using System;

namespace GGMSaveManager
{
    /// <summary>
    /// This object describes an entire save.bin file - it is made of 62 blocks of length 0x20000
    /// </summary>
    class GGMSaveBin
    {
        public const int totalSlots = 62;
        public const int slotLength = 0x20000;
        public const int hashLength = 0x8; // The xxHash64 ley is 8 bytes long
        public const UInt32 maxSRAMLength = slotLength - 0x14; // max addressable space
        public const int saveStateLength = slotLength - 0x10; // unknown at the moment - set to max possible
        public const int addressableGames = 6; // Augen's CFW allows 6 games per "firmware"
        public const int addressableSaveSlots = 4; // each of the 6 games has 1 SRAM slot, and 4 save state slots
        public const int addressableSaveIDs = (addressableGames * (addressableSaveSlots + 1)); // see GetGameAndSaveSlotNumber in SaveSlot.cs

        public SaveSlot[] saveSlots = new SaveSlot[totalSlots]; // an array of all the save slots in "save.bin"

        public UInt32[] latestSaves = new UInt32[addressableSaveIDs]; // store the highest version number for each game/save state slot

        /// <summary>
        /// Create empty GGMSaveBin object.
        /// </summary>
        public GGMSaveBin()
        {
            // create empty GGMSaveBin
            for (int s = 0; s < totalSlots; s++)
                saveSlots[s] = new SaveSlot(); // create blank slot

            // record latest saves for each ID
            for (int s = 0; s < addressableSaveIDs; s++)
                latestSaves[s] = 0;
        }

        /// <summary>
        /// Create new GGMSaveBin from all 62 save slots in save.bin
        /// </summary>
        public GGMSaveBin(byte[] saveFile)
        {
            for (int s = 0; s < totalSlots; s++)
            { 
                byte[] saveSlotData = new byte[slotLength];
                Array.Copy(saveFile, s * slotLength, saveSlotData, 0, slotLength);
                saveSlots[s] = new SaveSlot(saveSlotData);
            }
            RecordLatestSaves();
        }

        /// <summary>
        /// Record latest saves for each ID
        /// </summary>
        public void RecordLatestSaves()
        { 
            for (int s = 0; s < addressableSaveIDs; s++)
                latestSaves[s] = 0;

            for (int s = 0; s < totalSlots; s++)
            {
                UInt32 id = saveSlots[s].saveID;
                if (saveSlots[s].versionNumber > latestSaves[id])
                    latestSaves[id] = saveSlots[s].versionNumber;
            }
        }

        /// <summary>
        /// Build a complete save.bin file (in RAM) from the passed GGMSaveBin
        /// </summary>
        public static byte[] FileData(GGMSaveBin saveFile)
        {
            byte[] data = new byte[totalSlots * slotLength];

            for (int s = 0; s < totalSlots; s++)
            {
                byte[] slotData = SaveSlot.CreateSlotData(saveFile.saveSlots[s]); // retrieve built save slot file data
                Array.Copy(slotData, 0, data, s * slotLength, slotLength); // concat save slot
            }

            return data;
        }
    }
}
