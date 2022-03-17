using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMSaveManager
{
    /// <summary>
    /// A collection of functions for handling save slot related functions in GGMSaveManager.
    /// </summary>
    static class SlotOperations
    {
        /// <summary>
        /// Check if the given SaveSlot (of a given index of a given GGMSaveBin) is the latest version.
        /// </summary>
        public static bool IsLatestSaveData(int index, GGMSaveBin saveBin)
        {
            if (!saveBin.saveSlots[index].isEmpty && saveBin.saveSlots[index].versionNumber == saveBin.latestSaves[saveBin.saveSlots[index].saveID])
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if the given SaveSlot is the latest version.
        /// </summary>
        public static bool IsLatestSaveData(SaveSlot currentSaveSlot, GGMSaveBin saveBin)
        {
            if (!currentSaveSlot.isEmpty && currentSaveSlot.versionNumber == saveBin.latestSaves[currentSaveSlot.saveID])
            {
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Clear the given slot.
        /// </summary>
        public static void ClearSlot(ref SaveSlot saveSlot)
        {
            // Clear currently selected slot
            saveSlot = new SaveSlot();
        }

        /// <summary>
        /// Copy the given slot.
        /// </summary>
        public static SaveSlot CopySlot(SaveSlot saveSlot)
        {
            return saveSlot;
        }
    }
}
