using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMSaveManager
{
    class GGMSaveBin
    {
        // this is for the entire save.bin - it is made of 62 blocks of length 0x20000

        public const int totalSlots = 62;
        public const int slotLength = 0x20000;
        public const int hashLength = 0x8;
        public const UInt32 maxSRAMLength = slotLength - 0x14; // max addressable space
        public const int saveStateLength = slotLength - 0x10; // unknown at the moment - set to max possible
        public const int addressableGames = 6;
        public const int addressableSaveSlots = 4;
        public const int addressableSaveIDs = (addressableGames * (addressableSaveSlots + 1)); // see GetGameAndSaveSlotNumber in SaveSlot.cs

        public SaveSlot[] saveSlots = new SaveSlot[totalSlots];

        public UInt32[] latestSaves = new UInt32[addressableSaveIDs]; // store the highest version number for each game/save state slot

        public GGMSaveBin()
        {
            // create empty GGM save.bin file (unsaved)
            for (int s = 0; s < totalSlots; s++)
                saveSlots[s] = new SaveSlot(); // create blank slot

            // record latest saves for each ID
            for (int s = 0; s < addressableSaveIDs; s++)
                latestSaves[s] = 0;
        }

        public GGMSaveBin(byte[] saveFile)
        {
            // create all 62 save slots in save.bin
            for (int s = 0; s < totalSlots; s++)
            { 
                byte[] saveSlotData = new byte[slotLength];
                Array.Copy(saveFile, s * slotLength, saveSlotData, 0, slotLength);
                saveSlots[s] = new SaveSlot(saveSlotData);
            }

            RecordLatestSaves();
        }

        public void RecordLatestSaves()
        {
            // record latest saves for each ID
            for (int s = 0; s < addressableSaveIDs; s++)
                latestSaves[s] = 0;

            for (int s = 0; s < totalSlots; s++)
            {
                UInt32 id = saveSlots[s].saveID;
                if (saveSlots[s].versionNumber > latestSaves[id])
                    latestSaves[id] = saveSlots[s].versionNumber;
            }
        }

        public static byte[] FileData(GGMSaveBin saveFile)
        {
            byte[] data = new byte[totalSlots * slotLength];

            for (int s = 0; s < totalSlots; s++)
            {
                byte [] slotData = SaveSlot.CreateSlotData(saveFile.saveSlots[s]);
                Array.Copy(slotData, 0, data, s * slotLength, slotLength); // concat save slot
            }

            return data;
        }
    }
}
