using System.Windows.Forms;
using System.IO;

namespace GGMSaveManager
{
    /// <summary>
    /// A collection of functions for handling files in GGMSaveManager.
    /// </summary>
    static class FileOperations
    {
        /// <summary>
        /// Check if the given file exists in the user's file system.
        /// </summary>
        private static bool DoesFileExist(FileInfo file)
        {
            // unnneded, unused, and unfinished...

            if (!file.Exists)
            {
                //the file is unavailable because it does not exist

                const string message = "Can't Open File. Make sure it isn't open in another program." + "\n\n" + "The requested operation cannot be performed on a file with a user-mapped section open.";
                string caption = "Error opening " + file.Name;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);

                return false;
            }

            //file exists
            return true;
        }

        /// <summary>
        /// Check if the given file is locked by another process.
        /// </summary>
        private static bool IsFileLocked(FileInfo file, bool isSave = true)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)

                if (file.Exists)
                {
                    string saveLoad = (isSave ? "Save" : "Load");
                    string saveLoad2 = (isSave ? "sav" : "load");
                    string message = "Can't " + saveLoad + " File. Make sure it isn't locked by another program." + "\n\n" + "The requested operation cannot be performed on a file with a user-mapped section open.";
                    string caption = "Error " + saveLoad2 + "ing " + file.Name;
                    var result = MessageBox.Show(message, caption,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);

                    return true;
                }
            }

            //file is not locked
            return false;
        }

        /// <summary>
        /// Show message boxes to the user about any potential issues with the SRAM data to be imported. 
        /// Ask if they want to back out at all.
        /// </summary>
        private static bool CheckSRAMData(byte[] inputData, string filename, FormData formData, int fileIndex, int slotIndex)
        {
            bool SRAMDataOK = false;
            FileData fileData = formData.fileData[fileIndex];
            GGMSaveBin saveBin = fileData.saveBin;
            SaveSlot saveSlot = saveBin.saveSlots[slotIndex];
            ImportedGameNames gameNames = formData.gameNames;

            string introMessage = "Current Slot: " + formData.gameNames.nameList[saveSlot.gameNumber - 1] + "\nSRAM File: " + filename;
            // Check file size
            if (inputData.Length > saveSlot.sRAMDataLength)
            {
                string message = introMessage + "\n\nThe SRAM file is larger than the existing SRAM data. \nIt is probably from a different game." + "\n\n" + "Do you want to continue importing this SRAM data anyway?";
                string caption = "Warning whilst loading " + filename;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Error);
                SRAMDataOK = (result == DialogResult.Yes);
            }
            else if (inputData.Length <= (saveSlot.sRAMDataLength / 2))
            {
                string message = introMessage + "\n\nThe SRAM file is significantly smaller than the existing SRAM data. \nIt is probably from a different game." + "\n\n" + "Do you want to continue importing this SRAM data anyway?";
                string caption = "Warning whilst loading " + filename;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning);
                SRAMDataOK = (result == DialogResult.Yes);
            }
            else if (inputData.Length < saveSlot.sRAMDataLength)
            {
                string message = introMessage + "\n\nThe SRAM file is smaller than the existing SRAM data. \nIf you think you are importing the correct SRAM data, it is likely that the SRAM data file has just been trimmed. \nThis will not be an issue, as long as the game is correct." + "\n\n" + "Do you want to import the SRAM data?";
                string caption = "Warning whilst loading " + filename;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Information);
                SRAMDataOK = (result == DialogResult.Yes);
            }
            else
            {
                string message = introMessage + "\n\nThe SRAM file is the same size the existing SRAM data. \nIf you have chosen the correct game, this data should load correctly on the GGM." + "\n\n" + "Do you want to import the SRAM data?";
                string caption = "Warning whilst loading " + filename;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question);
                SRAMDataOK = (result == DialogResult.Yes);
            }

            // check version (ask if want to change to latest)
            if (SRAMDataOK && !SlotOperations.IsLatestSaveData(saveSlot, saveBin))
            {
                string message = "The current save slot is not the latest for the game \'" + gameNames.nameList[saveSlot.gameNumber - 1] + "\'. \nSo this imported SRAM data won't be loaded by the GGM.  \n\nDo you want to change this slot to be the latest?\n\nYes: Change slot to latest version number\nNo: Keep current version number\nCancel: Cancel importing this SRAM data.";
                string caption = "Warning whilst loading " + filename;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    saveSlot.ChangeVersionNumber(saveBin.latestSaves[saveSlot.saveID] + 1);
                    saveBin.RecordLatestSaves();
                }
                else if (result == DialogResult.No)
                {
                    // do nothing
                }
                else if (result == DialogResult.Cancel)
                {
                    SRAMDataOK = false;
                }
            }

            return SRAMDataOK;
        }

        /// <summary>
        /// Import SRAM data from a .srm file. Returns if the load was succesful
        /// </summary>
        public static bool ImportSRAMData(string gameFileName, FormData formData, int fileIndex, int slotIndex)
        {
            // Import SRAM data to passed save slot
            bool success = false;
            if (gameFileName != "")
            {
                // Import SRM data...
                byte[] data = File.ReadAllBytes(gameFileName); // read input data
                if (CheckSRAMData(data, Path.GetFileName(gameFileName), formData, fileIndex, slotIndex)) // ask user about import
                {
                    formData.fileData[fileIndex].saveBin.saveSlots[slotIndex].ImportSRAMData(data);
                    success = true;
                }
            }

            return success;
        }

        /// <summary>
        /// Give the user a prompt to save the given SRAM data as a file on their local file system. Then save the file.
        /// </summary>
        public static bool ExportSRAMData(string gameFileName, byte[] data)
        {
            // Export passed save slot
            bool success = false;
            if (gameFileName != "")
            {
                if (!IsFileLocked(new FileInfo(gameFileName)))
                {
                    File.WriteAllBytes(gameFileName, data); // save the data!
                    success = true;
                }
            }

            return success;
        }

        /// <summary>
        /// Save a GGMSaveBin object to a save.bin file. Returns if the save was succesful
        /// </summary>
        public static bool SaveBinFile(string saveFile, GGMSaveBin saveBin)
        {
            bool success = false;
            if (saveFile != "")
            {
                success = !IsFileLocked(new FileInfo(saveFile));
                if (success) File.WriteAllBytes(saveFile, GGMSaveBin.FileData(saveBin)); // save the data!
            }
            return success;
        }

        /// <summary>
        /// Open a save.bin file and import the data to a GGMSaveBin object. Returns if the load was succesful
        /// </summary>
        public static bool OpenBinFile(string saveFile, ref GGMSaveBin saveBin)
        {
            bool success = false;
            if (saveFile != "")
            {
                success = !IsFileLocked(new FileInfo(saveFile), false);
                if (success)
                {
                    byte[] data = File.ReadAllBytes(saveFile); // read input data
                    saveBin = new GGMSaveBin(data);
                }
            }

            return success;
        }

        /// <summary>
        /// Create a new (unsaved) save.bin file
        /// </summary>
        public static bool NewBinFile(ref GGMSaveBin saveBin, ref string saveFile, ref bool dirty)
        {
            if (dirty)
                if (!FormOperations.UserWantsToDiscardDirtyFile())
                    return false;

            saveFile = ""; // no file attached currently
            saveBin = new GGMSaveBin(); // create blank GGM save.bin
            dirty = true;
            return true;
        }

        /// <summary>
        /// Open a romlist.txt file and import the data to a ImportedGameNames object. Returns if the load was succesful
        /// </summary>
        public static bool OpenRomList(string nameFile, ref ImportedGameNames gameNames)
        {
            // open GG Game names file (romlist.txt from GGM CFW tool by Augen)

            bool success = false;
            if (nameFile != "")
            {
                success = !IsFileLocked(new FileInfo(nameFile), false);
                if (success)
                {
                    gameNames.ImportGameNames(File.ReadAllLines(nameFile));
                }
            }

            return success;
        }
    }
}
