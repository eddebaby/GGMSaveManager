using System;

namespace GGMSaveManager
{
    /// <summary>
    /// save.bin is made of 62 save slots. Each SaveSlot is 0x20000 long (including header).
    /// </summary>
    class SaveSlot
    {
        public UInt64 hash = 0; // xxHash64 (hash of the save slot from 0x8 - 0x20000)
        public UInt32 saveID = 0; // can range from 0x00 to 0x1D (+0 for SRAM, +1 for each save slot, +5 per game)
        public UInt32 versionNumber = 0; // incremented each time a new save is created on the GGM
        public UInt32 sRAMDataLength = 0; // length of SRAM data (only used for SRAM, not used for save states)
        public SaveData saveData;

        public int gameNumber = 1; // from saveID - range: 1-6
        public bool isSRAM = false; // from saveID
        public int saveSlotNumber = 0; // from saveID, only used when isSRAM == false - range: 1-4 (4 save state slots per game)
        public bool isEmpty = false; // an empty slot contains no header or save data and a hash = 0
        public bool isCorrupt = false; // data is corrupt if stored hash does not match calculated hash

        /// <summary>
        /// Create a blank SaveSlot object.
        /// </summary>
        public SaveSlot()
        {
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

        /// <summary>
        /// Create a SaveSlot object from a byte array containing raw data
        /// from a save.bin file.
        /// </summary>
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
        
        /// <summary>
        /// Create a new SaveSlot object by copying properties an existing SaveSlot.
        /// </summary>
        public SaveSlot(SaveSlot sourceSaveSlot)
        {
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

        /// <summary>
        /// Get the Game number and Save State Slot number of this SaveSlot
        /// from a given saveID.
        /// </summary>
        private void GetGameAndSaveSlotNumber(UInt32 saveID)
        {
            gameNumber = ((int)saveID / 5) + 1;
            saveSlotNumber = (int)saveID % 5;
            if (saveSlotNumber == 0) isSRAM = true;
            else isSRAM = false;
        }

        /// <summary>
        /// Returns the Save ID of this SaveSlot from a given Game number and Save State Slot number.
        /// </summary>
        private static UInt32 SetSaveID(int gameNumber, int saveSlotNumber)
        {
            UInt32 newSaveID = 0;
            newSaveID += ((UInt32)gameNumber - 1) * 5;
            newSaveID += (UInt32)saveSlotNumber;
            return newSaveID;
        }

        /// <summary>
        /// Returns a byte array of zeroed-out data of a given length.
        /// Used to pad out data when saving to file.
        /// </summary>
        public static byte[] ZeroRemainingData(int dataLength, byte value = 0)
        {
            byte[] data = new byte[dataLength];
            for (int d = 0; d < dataLength; d++)
            {
                data[d] = value;
            }
            return data;
        }

        /// <summary>
        /// Returns a complete "save.bin slot" byte array for saving to a file.
        /// </summary>
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

        /// <summary>
        /// Recalculate hash for a given SaveSlot.
        /// </summary>
        public static void ReCalculateHash(ref SaveSlot saveSlot)
        {
            byte[] saveSlotData = CreateSlotData(saveSlot);
            UInt64 newHash = CalculateHash(ref saveSlotData);
            saveSlot.hash = newHash;
            saveSlot.isCorrupt = false;
        }
        
        /// <summary>
        /// Recalculate hash for this SaveSlot.
        /// </summary>
        public void ReCalculateHash()
        {
            byte[] saveSlotData = CreateSlotData(this);
            UInt64 newHash = CalculateHash(ref saveSlotData);
            hash = newHash;
            isCorrupt = false;
        }

        /// <summary>
        /// Calculate the hash for the given byte array of data.
        /// </summary>
        private static UInt64 CalculateHash(ref byte[] inputData)
        {
            // Uses xxHash64 algorithm - https://cyan4973.github.io/xxHash/
            return XXHash.xxHash64(ref inputData, GGMSaveBin.hashLength);
        }

        /// <summary>
        /// Import the given byte array as SRAM data for this SaveSlot.
        /// </summary>
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

        /// <summary>
        /// Change the Save State Slot number of the Save ID for this SaveSlot.
        /// Note: setting the Save State Slot number to 0 will convert this
        /// SaveSlot to SRAM (will it??).
        /// </summary>
        public void ChangeSaveStateSlot(int slot)
        {
            if (isSRAM || isEmpty) return;

            saveSlotNumber = Math.Max(Math.Min(slot, GGMSaveBin.addressableSaveSlots), 1);
            
            saveID = SetSaveID(gameNumber, saveSlotNumber);
            ReCalculateHash();
        }
        
        /// <summary>
        /// Change the version number of the Save ID for this SaveSlot.
        /// </summary>
        public void ChangeVersionNumber(UInt32 version)
        {
            if (isEmpty) return;

            versionNumber = Math.Max(Math.Min(version, UInt32.MaxValue), 0);
            ReCalculateHash();
        }

        /// <summary>
        /// Change the game number of the Save ID for this SaveSlot.
        /// </summary>
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

        /// <summary>
        /// Change the stored length of SRAM data for this SaveSlot.
        /// </summary>
        public void ChangeSRAMLength(UInt32 length)
        {
            if (!isSRAM || isEmpty) return;

            sRAMDataLength = Math.Max(Math.Min(length, GGMSaveBin.maxSRAMLength), 0);
            saveID = SetSaveID(gameNumber, saveSlotNumber);
            ReCalculateHash();
        }
    }
}
