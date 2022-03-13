using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMSaveManager
{
    class SaveSlot
    {
        // each slot is 0x20000 long (including header)
        public UInt64 hash = 0; // xxHash64 (hash of the save slot from 0x8 - 0x20000)
        public UInt32 saveID = 0; // can range from 0x00 to 0x1D (+0 for SRAM, +1 for each save slot, +5 per game)
        public UInt32 versionNumber = 0; // incremented each time a new save is created on the GGM
        public UInt32 sRAMDataLength = 0; // length of SRAM data (only used for SRAM, not used for save states)
        public SaveData saveData;

        public int gameNumber = 1; // from saveID - range: 1-6
        public bool isSRAM = false; // from saveID
        public int saveSlotNumber = 0; // from saveID, only used when isSRAM == false - range: 1-4 (4 save state slots per game)
        public bool isEmpty = false;
        public bool isCorrupt = false;

        public SaveSlot()
        {
            // create blank save slot
            isEmpty = true;
            hash = 0;
            saveID = 0;
            versionNumber = 0;
            sRAMDataLength = 0;
            saveData = new SaveData();

            gameNumber = 0;
            saveSlotNumber = 0;
            isSRAM = false;
            isCorrupt = false;
        }

        public SaveSlot(byte[] saveSlotData)
        {
            hash = BitConverter.ToUInt64(saveSlotData, 0);
            saveID = BitConverter.ToUInt32(saveSlotData, 8);
            versionNumber = BitConverter.ToUInt32(saveSlotData, 12);
            GetGameAndSaveSlotNumber(saveID);

            byte[] rawSaveData;
            if (isSRAM)
            {
                sRAMDataLength = BitConverter.ToUInt32(saveSlotData, 16);
                rawSaveData = new byte[sRAMDataLength];
                Array.Copy(saveSlotData, 20, rawSaveData, 0, sRAMDataLength);
            }
            else
            {
                sRAMDataLength = 0;
                rawSaveData = new byte[GGMSaveBin.saveStateLength];
                Array.Copy(saveSlotData, 16, rawSaveData, 0, GGMSaveBin.saveStateLength);
            }
            saveData = new SaveData(rawSaveData);

            isEmpty = isCorrupt = false;

            if (hash != CalculateHash(ref saveSlotData))
            {
                bool dataIsAllZeros = true;
                foreach (byte b in saveData.data)
                {
                    
                    if (b != 0)
                    {
                        dataIsAllZeros = false;
                        break;
                    }
                }
                if (hash == 0 && dataIsAllZeros && saveID == 0 && versionNumber == 0 && isSRAM && sRAMDataLength == 0) isEmpty = true;
                else isCorrupt = true;
            }
        }

        public SaveSlot(SaveSlot sourceSaveSlot)
        {
            // copy from source save slot to this one

            hash = sourceSaveSlot.hash;
            saveID = sourceSaveSlot.saveID;
            versionNumber = sourceSaveSlot.versionNumber;

            gameNumber = sourceSaveSlot.gameNumber;
            saveSlotNumber = sourceSaveSlot.saveSlotNumber;
            isSRAM = sourceSaveSlot.isSRAM;

            sRAMDataLength = sourceSaveSlot.sRAMDataLength;
            saveData = sourceSaveSlot.saveData;

            isEmpty = sourceSaveSlot.isEmpty;
            isCorrupt = sourceSaveSlot.isCorrupt;
        }

        private void GetGameAndSaveSlotNumber(UInt32 saveID)
        {
            gameNumber = ((int)saveID / 5) + 1;
            saveSlotNumber = (int)saveID % 5;
            if (saveSlotNumber == 0) isSRAM = true;
            else isSRAM = false;
        }

        private static UInt32 SetSaveID(int gameNumber, int saveSlotNumber)
        {
            UInt32 newSaveID = 0;
            newSaveID += ((UInt32)gameNumber - 1) * 5;
            newSaveID += (UInt32)saveSlotNumber;
            return newSaveID;
        }

        public static byte[] ZeroRemainingData(int dataLength, byte value = 0)
        {
            byte[] data = new byte[dataLength];
            for (int d = 0; d < dataLength; d++)
            {
                data[d] = value;
            }
            return data;
        }

        public static byte[] CreateSlotData(SaveSlot saveSlot)
        {
            byte[] data = new byte[GGMSaveBin.slotLength];

            if (saveSlot.isEmpty)
            {
                // create empty slot...
                Array.Copy(ZeroRemainingData(GGMSaveBin.slotLength), 0, data, 0, GGMSaveBin.slotLength);
                return data;
            }

            byte[] uIntData = BitConverter.GetBytes(saveSlot.hash);
            Array.Copy(uIntData, 0, data, 0, 8);

            saveSlot.saveID = SetSaveID(saveSlot.gameNumber, saveSlot.saveSlotNumber);

            uIntData = BitConverter.GetBytes(saveSlot.saveID);
            Array.Copy(uIntData, 0, data, 8, 4);

            uIntData = BitConverter.GetBytes(saveSlot.versionNumber);
            Array.Copy(uIntData, 0, data, 12, 4);

            int endOfData = 0;

            if (saveSlot.isSRAM)
            {
                uIntData = BitConverter.GetBytes(saveSlot.sRAMDataLength);
                Array.Copy(uIntData, 0, data, 16, 4);
                Array.Copy(saveSlot.saveData.data, 0, data, 20, saveSlot.saveData.dataLength);
                endOfData = 20 + saveSlot.saveData.dataLength;
            }
            else
            {
                Array.Copy(saveSlot.saveData.data, 0, data, 16, saveSlot.saveData.dataLength);
                endOfData = 16 + saveSlot.saveData.dataLength;
            }
            if (endOfData < GGMSaveBin.slotLength)
            {
                int remainingDataLength = GGMSaveBin.slotLength - endOfData;
                // zero out rest of slot...
                Array.Copy(ZeroRemainingData(remainingDataLength), 0, data, endOfData, remainingDataLength);
            }

            return data;
        }

        public static void ReCalculateHash(ref SaveSlot saveSlot)
        {
            byte[] saveSlotData = CreateSlotData(saveSlot);
            UInt64 newHash = CalculateHash(ref saveSlotData);
            saveSlot.hash = newHash;
            saveSlot.isCorrupt = false;
        }

        public void ReCalculateHash()
        {
            byte[] saveSlotData = CreateSlotData(this);
            UInt64 newHash = CalculateHash(ref saveSlotData);
            hash = newHash;
            isCorrupt = false;
        }

        private static UInt64 CalculateHash(ref byte[] inputData)
        {
            // Uses xxHash64 algorithm - https://cyan4973.github.io/xxHash/
            return XXHash.xxHash64(ref inputData, GGMSaveBin.hashLength);
        }

        public void ImportSRAMData(byte[] inputData)
        {
            if (!isSRAM || isEmpty) return;

            byte[] newSRAM = new byte[sRAMDataLength];
            Array.Copy(inputData, 0, newSRAM, 0, Math.Min(inputData.Length, sRAMDataLength));

            if (inputData.Length < sRAMDataLength)
            {
                int remainingDataLength = (int)sRAMDataLength - inputData.Length;
                // zero out rest of SRAM data (with 0xFFs)...
                Array.Copy(ZeroRemainingData(remainingDataLength, 0xFF), 0, newSRAM, inputData.Length, remainingDataLength);
            }
            saveData = new SaveData(newSRAM);
            ReCalculateHash();
        }

        public void ChangeSaveStateSlot(int slot)
        {
            if (isSRAM || isEmpty) return;

            saveSlotNumber = Math.Max(Math.Min(slot, GGMSaveBin.addressableSaveSlots), 0);
            
            saveID = SetSaveID(gameNumber, saveSlotNumber);
            ReCalculateHash();
        }

        public void ChangeVersionNumber(UInt32 version)
        {
            if (isEmpty) return;

            versionNumber = Math.Max(Math.Min(version, UInt32.MaxValue), 0);
            ReCalculateHash();
        }

        public void ChangeGameNumber(int game)
        {
            //if (isEmpty) return;

            gameNumber = Math.Max(Math.Min(game, GGMSaveBin.addressableGames), 1);
            saveID = SetSaveID(gameNumber, saveSlotNumber);
            if (isEmpty)
            {
                isSRAM = true;
                isEmpty = false;
            }
            
            ReCalculateHash();
        }

        public void ChangeSRAMLength(UInt32 length)
        {
            if (!isSRAM || isEmpty) return;

            sRAMDataLength = Math.Max(Math.Min(length, GGMSaveBin.maxSRAMLength), 0);
            saveID = SetSaveID(gameNumber, saveSlotNumber);
            ReCalculateHash();
        }
    }
}
