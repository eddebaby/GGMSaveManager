using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

public enum AdvancedFunctions { ChangeVersionNumber, ChangeSaveStateID, ChangeGameID, ChangeSRAMSize, RecalculateHash};

namespace GGMSaveManager
{
    /// <summary>
    /// A collection of functions for handling form related functionality in GGMSaveManager.
    /// </summary>
    static class FormOperations
    {
        /// <summary>
        /// Prompt user to cancel exiting the program if either file has unsaved changes.
        /// </summary>
        public static bool AskCancelExit(bool dirty)
        {
            if (dirty)
            {
                const string message = "You have made changes to the Game Gear Micro save data." + "\n" + "Are you sure you want to quit?";
                const string caption = "Exit with Unsaved changes";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                return (result == DialogResult.No);
            }
            return false;
        }

        /// <summary>
        /// Multiplies two 32-bit values and then divides the 64-bit result by a 
        /// third 32-bit value. The final result is rounded to the nearest integer.
        /// </summary>
        public static int MulDiv(int nNumber, int nNumerator, int nDenominator)
        {
            return (int)Math.Round((float)nNumber * nNumerator / nDenominator);
        }

        /// <summary>
        /// Multiplies two 32-bit values and then divides the 64-bit result by a 
        /// third 32-bit value. The final result is rounded to the nearest integer.
        /// </summary>
        public static int MulDiv(float number, float numerator, float denominator)
        {
            return MulDiv((int)number, (int)numerator, (int)denominator);
        }
        
        /// <summary>
        /// Mulitiplies width and height by a factor.
        /// </summary>
        public static Size ScaleSize(Size size, float width, float height)
        {
            return new Size(size.Width * (int)width, size.Height * (int)height);
        }

        /// <summary>
        /// Prompt user to enter a value in a message box.
        /// </summary>
        public static bool InputQuery(String caption, String prompt, ref String value)
        {
            // Returns the String as a ref parameter, returning true if they hit OK, or false if they hit Cancel
            Form form;
            form = new Form();
            form.AutoScaleMode = AutoScaleMode.Font;
            form.Font = SystemFonts.IconTitleFont;

            SizeF dialogUnits;
            dialogUnits = form.AutoScaleDimensions;

            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Text = caption;

            form.ClientSize = new Size(
                        MulDiv(180, dialogUnits.Width, 4),
                        MulDiv(63, dialogUnits.Height, 8));

            form.StartPosition = FormStartPosition.CenterScreen;

            System.Windows.Forms.Label lblPrompt;
            lblPrompt = new System.Windows.Forms.Label();
            lblPrompt.Parent = form;
            lblPrompt.AutoSize = true;
            lblPrompt.Left = MulDiv(8, dialogUnits.Width, 4);
            lblPrompt.Top = MulDiv(8, dialogUnits.Height, 8);
            lblPrompt.Text = prompt;

            System.Windows.Forms.TextBox edInput;
            edInput = new System.Windows.Forms.TextBox();
            edInput.Parent = form;
            edInput.Left = lblPrompt.Left;
            edInput.Top = MulDiv(19, dialogUnits.Height, 8);
            edInput.Width = MulDiv(164, dialogUnits.Width, 4);
            edInput.Text = value;
            edInput.SelectAll();


            int buttonTop = MulDiv(41, dialogUnits.Height, 8);
            //Command buttons should be 50x14 dlus
            Size buttonSize = ScaleSize(new Size(72, 27), dialogUnits.Width / 4, dialogUnits.Height / 8);

            System.Windows.Forms.Button bbOk = new System.Windows.Forms.Button();
            bbOk.Parent = form;
            bbOk.Text = "OK";
            bbOk.DialogResult = DialogResult.OK;
            form.AcceptButton = bbOk;
            bbOk.Location = new Point(MulDiv(38, dialogUnits.Width, 4), buttonTop);
            bbOk.Size = buttonSize;

            System.Windows.Forms.Button bbCancel = new System.Windows.Forms.Button();
            bbCancel.Parent = form;
            bbCancel.Text = "Cancel";
            bbCancel.DialogResult = DialogResult.Cancel;
            form.CancelButton = bbCancel;
            bbCancel.Location = new Point(MulDiv(92, (int)dialogUnits.Width, 4), buttonTop);
            bbCancel.Size = buttonSize;

            if (form.ShowDialog() == DialogResult.OK)
            {
                value = edInput.Text;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Use correct formatting for showing slots in a list box
        /// </summary>
        public static void DrawSlotsListBox(object sender, DrawItemEventArgs e, GGMSaveBin saveBin)
        {
            Font f = e.Font;
            Color c = e.ForeColor;
            if (SlotOperations.IsLatestSaveData(e.Index, saveBin))
                f = new Font(e.Font, FontStyle.Bold);
            if (saveBin.saveSlots[e.Index].isCorrupt)
                c = Color.Red;
            e.DrawBackground();
            e.Graphics.DrawString(((ListBox)(sender)).Items[e.Index].ToString(), f, new SolidBrush(c), e.Bounds);
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Populate a given list box with the contents of all of SaveSlots in a given GGMSaveBin.
        /// If restoreSelection = true, then the last selected slot in the listbox will be restored after the function is complete.
        /// </summary>
        public static void PopulateSaveSlots(ListBox listBox, GGMSaveBin saveBin, ImportedGameNames gameNames, bool restoreSelection = false)
        {
            int index = listBox.SelectedIndex;
            int topIndex = listBox.TopIndex;

            listBox.Items.Clear();
            for (int s = 0; s < GGMSaveBin.totalSlots; s++)
            {
                if (saveBin.saveSlots[s].isEmpty)
                {
                    listBox.Items.Add("Slot " + (s + 1).ToString("00") + ": Empty");
                }
                else
                {
                    listBox.Items.Add("Slot " + (s + 1).ToString("00") + ": " + (saveBin.saveSlots[s].isSRAM ? "SRAM [" + (saveBin.saveSlots[s].sRAMDataLength / 1024) + " KB]" : "State " + saveBin.saveSlots[s].saveSlotNumber) + " (v" + (saveBin.saveSlots[s].versionNumber + 1) + ") - " + gameNames.nameList[saveBin.saveSlots[s].gameNumber - 1]);
                }
            }
            if (restoreSelection)
            {
                listBox.TopIndex = topIndex;
                listBox.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Ask user if they want to discard unsaved changes.
        /// </summary>
        public static bool UserWantsToDiscardDirtyFile()
        {
            const string message = "You have made changes to the Game Gear Micro save data." + "\n" + "Are you sure you want to lose all unsaved changes?";
            const string caption = "Lose all unsaved changes";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
                return true;

            return false;
        }
        
        /// <summary>
        /// Ask user to enter a new numerical value with a message box.
        /// </summary>
        public static bool ChooseNumberDialog(ref int theNumber, bool minusOne = false)
        {
            int minusOneAmount = (minusOne ? -1 : 0);
            const string message = "Enter the new number.";
            const string caption = "Choose Number";
            string returnValue = (theNumber - minusOneAmount).ToString();
            bool result = false;

            if (InputQuery(caption, message, ref returnValue))
            {
                if (int.TryParse(returnValue, out int theNum))
                {
                    theNumber = theNum + minusOneAmount;
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Give the user a prompt to open a file. Return whether to open the file or not.
        /// </summary>
        public static bool AskOpenFile(string title, ref string file, ref string previousFile, ref string previousPath, OpenFileDialog openFileDialog, Timer t, bool dirty = false)
        {
            if (dirty)
                if (!FormOperations.UserWantsToDiscardDirtyFile())
                    return false;

            if (previousFile != "")
            {
                openFileDialog.FileName = previousFile;
                openFileDialog.InitialDirectory = previousPath;
            }

            openFileDialog.Title = title;

            t.Start(); // workaround for bug: filename offset to the left
            bool success = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog.FileName; // input file with GGM save data
                previousFile = Path.GetFileName(file);
                previousPath = Path.GetDirectoryName(file);


                if (file != "")
                {
                    success = true;
                }
            }
            return success;
        }

        /// <summary>
        /// Give the user a prompt to save a file. Return whether to save the file or not.
        /// </summary>
        public static bool AskSaveFile(string title, ref string file, ref string previousFile, ref string previousPath, SaveFileDialog saveFileDialog, Timer t, bool dirty = false, bool saveAs = false)
        {
            bool success = false;
            if (file == "" || saveAs)
            {
                if (previousFile != "")
                {
                    saveFileDialog.FileName = previousFile;
                    saveFileDialog.InitialDirectory = previousPath;
                }

                saveFileDialog.Title = title;

                t.Start(); // workaround for bug: filename offset to the left
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (saveFileDialog.FileName != "")
                    {
                        file = saveFileDialog.FileName;

                        previousFile = Path.GetFileName(file);
                        previousPath = Path.GetDirectoryName(file);
                    }
                    
                }
            }
            
            if (file != "")
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Give the user a prompt to open a save.bin file, then open it. Return whether the file was opened.
        /// </summary>
        public static bool AskOpenBinFile(FormData formData, int fileIndex)
        {
            FileData fileData = formData.fileData[fileIndex];
            bool success = false;
            if (FormOperations.AskOpenFile("Open Game Gear Micro save data", ref fileData.saveFile, ref fileData.previousFile, ref fileData.previousPath, fileData.openFileDialog, formData.t, fileData.fileDirty))
            {
                if (fileData.saveFile != "")
                    success = FileOperations.OpenBinFile(fileData.saveFile, ref fileData.saveBin);
            }

            return success;
        }
        
        /// <summary>
        /// Optionally, give the user a prompt to save the given GGMSaveBin as a file on their local file system. Then save the file.
        /// </summary>
        public static bool AskSaveBinFile(FormData formData, int fileIndex, bool saveAs = false)
        {
            FileData fileData = formData.fileData[fileIndex];
            bool success = false;
            if (FormOperations.AskSaveFile("Save Game Gear Micro save data", ref fileData.saveFile, ref fileData.previousFile, ref fileData.previousPath, fileData.saveFileDialog, formData.t, fileData.fileDirty, saveAs))
            {
                if (fileData.saveFile != "")
                    success = FileOperations.SaveBinFile(fileData.saveFile, fileData.saveBin);
            }

            return success;
        }

        /// <summary>
        /// Give the user a prompt to import the given SRAM data file to the chosen slot. Then import the data.
        /// </summary>
        private static bool AskImportSRAMData(FormData formData, int fileIndex, int slotIndex)
        {
            // Import SRAM data to passed save slot
            FileData fileData = formData.fileData[fileIndex];
            GGMSaveBin saveBin = fileData.saveBin;
            SaveSlot saveSlot = saveBin.saveSlots[slotIndex];
            
            bool success = false;
            formData.sRAMFile = formData.gameNames.nameList[saveSlot.gameNumber - 1];

            if (FormOperations.AskOpenFile("Import Game Gear SRAM file", ref formData.sRAMFile, ref formData.previousSRAMFile, ref formData.previousSRAMFilePath, formData.sramFileOFD, formData.t))
            {
                if (formData.sRAMFile != "")
                    success = FileOperations.ImportSRAMData(formData.sRAMFile, formData, fileIndex, slotIndex);
            }

            return success;
        }

        /// <summary>
        /// Give the user a prompt to export the given SRAM data file from the chosen slot. Then export the data.
        /// </summary>
        private static bool AskExportSRAMData(FormData formData, int fileIndex, int slotIndex)
        {
            FileData fileData = formData.fileData[fileIndex];
            GGMSaveBin saveBin = fileData.saveBin;
            SaveSlot saveSlot = saveBin.saveSlots[slotIndex];

            formData.sRAMFile = formData.gameNames.nameList[saveSlot.gameNumber - 1];

            bool success = false;

            if (FormOperations.AskSaveFile("Export Game Gear SRAM file", ref formData.sRAMFile, ref formData.previousSRAMFile, ref formData.previousSRAMFilePath, formData.sramFileSFD, formData.t, false, true))
            {
                if (formData.sRAMFile != "")
                    success = FileOperations.ExportSRAMData(formData.sRAMFile, saveSlot.saveData.data);
            }

            return success;           
        }

        /// <summary>
        /// Give the user a prompt to open a romlist.txt file, then open it. Return whether the file was opened.
        /// Expects a GG Game names file (romlist.txt from GGM CFW tool by Augen).
        /// </summary>
        public static bool AskOpenRomList(FormData formData)
        {
            
            bool success = false;
            if (FormOperations.AskOpenFile("Open List of 6 Game Gear games", ref formData.nameFile, ref formData.previousNameFile, ref formData.previousNameFilePath, formData.nameFileOFD, formData.t))
            {
                if (formData.nameFile != "")
                    success = FileOperations.OpenRomList(formData.nameFile, ref formData.gameNames);
            }
            if (success)
            {
                if (formData.fileData[0].saveFile != null) FormOperations.PopulateSaveSlots(formData.fileData[0].slotFormControls.slotList, formData.fileData[0].saveBin, formData.gameNames, true);
                if (formData.fileData[1].saveFile != null) FormOperations.PopulateSaveSlots(formData.fileData[1].slotFormControls.slotList, formData.fileData[1].saveBin, formData.gameNames, true);
            }
            return success;
        }

        /// <summary>
        /// Toggle the availability of the advanced features.
        /// </summary>
        public static void ToggleAdvancedFeatures(FormData formData, bool newValue)
        {
            formData.advancedFeatures = newValue;
            if (formData.fileData[0].saveBin != null) FormOperations.UpdateFileUI(formData.fileData[0], true);
            if (formData.fileData[1].saveBin != null) FormOperations.UpdateFileUI(formData.fileData[1], true);
        }

        /// <summary>
        /// Update UI after loading a file.
        /// </summary>
        public static void UpdateFileUI(FileData fileData, bool restoreSelection = false, bool keepDirty = false)
        {
            if (!keepDirty) fileData.fileDirty = false;
            SlotFormControls slotFormControls = fileData.slotFormControls;
            PopulateSaveSlots(slotFormControls.slotList, fileData.saveBin, fileData.parentFormData.gameNames, restoreSelection);
            slotFormControls.fileName.Text = fileData.saveFile;
            slotFormControls.fileSave.Enabled = slotFormControls.fileSaveAs.Enabled = true;
            CheckFileSlotUI(fileData);
        }

        /// <summary>
        /// Update UI after selecting a slot in a file.
        /// </summary>
        public static void CheckFileSlotUI(FileData fileData)
        {
            SlotFormControls slotFormControls = fileData.slotFormControls;
            int selectedIndex = slotFormControls.slotList.SelectedIndex;

            slotFormControls.fileSave.Enabled = fileData.fileDirty;
            if (selectedIndex != -1)
            {
                GGMSaveBin saveBin = fileData.saveBin;
                slotFormControls.slotCopy.Enabled = !saveBin.saveSlots[selectedIndex].isEmpty;
                slotFormControls.slotPaste.Enabled = fileData.parentFormData.dirtyClipboard;
                slotFormControls.slotClear.Enabled = !saveBin.saveSlots[selectedIndex].isEmpty;
                slotFormControls.slotExportSRAM.Enabled = saveBin.saveSlots[selectedIndex].isSRAM && !saveBin.saveSlots[selectedIndex].isEmpty;
                slotFormControls.slotImportSRAM.Enabled = saveBin.saveSlots[selectedIndex].isSRAM && !saveBin.saveSlots[selectedIndex].isEmpty;

                bool advancedFeatures = fileData.parentFormData.advancedFeatures;

                slotFormControls.labelSet.Enabled = advancedFeatures;

                if (advancedFeatures) slotFormControls.slotGameID.Enabled = true;
                else slotFormControls.slotGameID.Enabled = false;
                if (advancedFeatures) slotFormControls.slotSaveStateID.Enabled = !saveBin.saveSlots[selectedIndex].isSRAM && !saveBin.saveSlots[selectedIndex].isEmpty;
                else slotFormControls.slotSaveStateID.Enabled = false;
                if (advancedFeatures) slotFormControls.slotVersion.Enabled = !saveBin.saveSlots[selectedIndex].isEmpty;
                else slotFormControls.slotVersion.Enabled = false;
                if (advancedFeatures) slotFormControls.slotSRAMSize.Enabled = saveBin.saveSlots[selectedIndex].isSRAM && !saveBin.saveSlots[selectedIndex].isEmpty;
                else slotFormControls.slotSRAMSize.Enabled = false;
                if (advancedFeatures && saveBin.saveSlots[selectedIndex].isCorrupt) slotFormControls.slotHash.Enabled = !saveBin.saveSlots[selectedIndex].isEmpty;
                else slotFormControls.slotHash.Enabled = false;

                slotFormControls.labelSlot.Text = "Slot " + (selectedIndex + 1);
            }
            else
            {
                slotFormControls.slotCopy.Enabled = false;
                slotFormControls.slotPaste.Enabled = false;
                slotFormControls.slotClear.Enabled = false;
                slotFormControls.slotExportSRAM.Enabled = false;
                slotFormControls.slotImportSRAM.Enabled = false;
                slotFormControls.slotGameID.Enabled = false;
                slotFormControls.slotSaveStateID.Enabled = false;
                slotFormControls.slotVersion.Enabled = false;
                slotFormControls.slotSRAMSize.Enabled = false;
                slotFormControls.slotHash.Enabled = false;
                slotFormControls.labelSlot.Text = "Slot";
            }
            slotFormControls.slotList.Focus();
        }

        /// <summary>
        /// Check a slot is selected, then clear it.
        /// </summary>
        public static void CheckClearSlot(FileData fileData)
        {
            SlotFormControls slotFormControls = fileData.slotFormControls;
            int selectedIndex = slotFormControls.slotList.SelectedIndex;

            if (selectedIndex == -1)
            {
                slotFormControls.slotClear.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            // Clear currently selected slot
            SlotOperations.ClearSlot(ref fileData.saveBin.saveSlots[selectedIndex]);
            PopulateSaveSlots(slotFormControls.slotList, fileData.saveBin, fileData.parentFormData.gameNames, true);
            fileData.fileDirty = true;
            CheckFileSlotUI(fileData);
        }

        /// <summary>
        /// Check a slot is selected, then copy it to the clipboard.
        /// </summary>
        public static void CheckCopySlot(FileData fileData)
        {
            SlotFormControls slotFormControls = fileData.slotFormControls;
            int selectedIndex = slotFormControls.slotList.SelectedIndex;

            if (selectedIndex == -1)
            {
                slotFormControls.slotCopy.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            fileData.parentFormData.copiedSaveSlot = SlotOperations.CopySlot(fileData.saveBin.saveSlots[selectedIndex]);
            fileData.parentFormData.dirtyClipboard = slotFormControls.slotPaste.Enabled = true;
        }

        /// <summary>
        /// Check a slot is selected, then paste the clipboard to it.
        /// </summary>
        public static void CheckPasteSlot(FileData fileData)
        {
            SlotFormControls slotFormControls = fileData.slotFormControls;
            int selectedIndex = slotFormControls.slotList.SelectedIndex;

            if (selectedIndex == -1)
            {
                slotFormControls.slotPaste.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            fileData.saveBin.saveSlots[selectedIndex] = SlotOperations.CopySlot(new SaveSlot(fileData.parentFormData.copiedSaveSlot));
            fileData.parentFormData.dirtyClipboard = slotFormControls.slotPaste.Enabled = false;
            PopulateSaveSlots(fileData.slotFormControls.slotList, fileData.saveBin, fileData.parentFormData.gameNames, true);
            fileData.fileDirty = true;
            CheckFileSlotUI(fileData);
        }

        /// <summary>
        /// Check a slot is selected and contains SRAM data, then export the SRAM data.
        /// </summary>
        public static void CheckExportSRAMData(FormData formData, int fileIndex)
        {
            FileData fileData = formData.fileData[fileIndex];
            GGMSaveBin saveBin = fileData.saveBin;
            SlotFormControls slotFormControls = fileData.slotFormControls;
            int selectedIndex = slotFormControls.slotList.SelectedIndex;

            if (selectedIndex == -1)
            {
                slotFormControls.slotExportSRAM.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveBin.saveSlots[selectedIndex].isSRAM)
            {
                slotFormControls.slotExportSRAM.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            AskExportSRAMData(formData, fileIndex, selectedIndex);
        }

        /// <summary>
        /// Check a slot is selected and contains SRAM data, then import the given SRAM data.
        /// </summary>
        public static void CheckImportSRAMData(FormData formData, int fileIndex)
        {
            FileData fileData = formData.fileData[fileIndex];
            GGMSaveBin saveBin = fileData.saveBin;
            SlotFormControls slotFormControls = fileData.slotFormControls;
            int selectedIndex = slotFormControls.slotList.SelectedIndex;

            if (selectedIndex == -1)
            {
                slotFormControls.slotImportSRAM.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveBin.saveSlots[selectedIndex].isSRAM)
            {
                slotFormControls.slotImportSRAM.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            if (AskImportSRAMData(formData, fileIndex, selectedIndex))
            {
                fileData.fileDirty = true;
                UpdateFileUI(fileData, true, true);
            }
        }

        /// <summary>
        /// Get the button associated with the given advanced function.
        /// </summary>
        private static Button GetAdvancedFunctionButton(AdvancedFunctions selectedFunction, SlotFormControls slotFormControls)
        {
            Button button;

            switch (selectedFunction)
            {
                case AdvancedFunctions.ChangeGameID:
                    button = slotFormControls.slotGameID;
                    break;
                case AdvancedFunctions.ChangeSaveStateID:
                    button = slotFormControls.slotSaveStateID;
                    break;
                case AdvancedFunctions.ChangeVersionNumber:
                    button = slotFormControls.slotVersion;
                    break;
                case AdvancedFunctions.ChangeSRAMSize:
                    button = slotFormControls.slotSRAMSize;
                    break;
                case AdvancedFunctions.RecalculateHash:
                    button = slotFormControls.slotHash;
                    break;
                default:
                    button = slotFormControls.slotGameID;
                    break;
            }

            return button;
        }
        
        /// <summary>
        /// Get the number associated with the given advanced function.
        /// </summary>
        private static int GetAdvancedFunctionNumber(AdvancedFunctions selectedFunction, SaveSlot saveSlot, ref bool minusOne)
        {
            int num;

            switch (selectedFunction)
            {
                case AdvancedFunctions.ChangeGameID:
                    num = saveSlot.gameNumber;
                    minusOne = false;
                    break;
                case AdvancedFunctions.ChangeSaveStateID:
                    num = saveSlot.saveSlotNumber;
                    minusOne = false;
                    break;
                case AdvancedFunctions.ChangeVersionNumber:
                    num = (int)saveSlot.versionNumber;
                    minusOne = true;
                    break;
                case AdvancedFunctions.ChangeSRAMSize:
                    num = (int)saveSlot.sRAMDataLength / 1024;
                    minusOne = false;
                    break;
                default:
                    num = saveSlot.gameNumber;
                    minusOne = false;
                    break;
            }

            return num;
        }
        
        /// <summary>
        /// Run the given advanced function.
        /// </summary>
        private static void RunAdvancedFunction(AdvancedFunctions selectedFunction, SaveSlot saveSlot, int newNum)
        {
            switch (selectedFunction)
            {
                case AdvancedFunctions.ChangeGameID:
                    saveSlot.ChangeGameNumber(newNum);
                    break;
                case AdvancedFunctions.ChangeSaveStateID:
                    saveSlot.ChangeSaveStateSlot(newNum);
                    break;
                case AdvancedFunctions.ChangeVersionNumber:
                    saveSlot.ChangeVersionNumber((UInt32)newNum);
                    break;
                case AdvancedFunctions.ChangeSRAMSize:
                    saveSlot.ChangeSRAMLength((UInt32)newNum * 1024);
                    break;
                case AdvancedFunctions.RecalculateHash:
                    saveSlot.ReCalculateHash();
                    break;
                default:
                    saveSlot.ChangeGameNumber(newNum);
                    break;
            }
        }

        /// <summary>
        /// Check a slot is selected, do any other requested checks, then perform advanced function.
        /// </summary>
        public static void CheckAdvancedFunction(FormData formData, int fileIndex, AdvancedFunctions selectedFunction)
        {
            FileData fileData = formData.fileData[fileIndex];
            GGMSaveBin saveBin = fileData.saveBin;
            SlotFormControls slotFormControls = fileData.slotFormControls;
            int selectedIndex = slotFormControls.slotList.SelectedIndex;
            Button button = GetAdvancedFunctionButton(selectedFunction, slotFormControls);

            if (selectedIndex == -1)
            {
                button.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            SaveSlot saveSlot = saveBin.saveSlots[selectedIndex];
            if (saveSlot.isEmpty && selectedFunction != AdvancedFunctions.ChangeGameID)
            {
                button.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveSlot.isSRAM && selectedFunction == AdvancedFunctions.ChangeSRAMSize)
            {
                button.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            bool result = formData.advancedFeatures;
            int newNum = 0;
            bool minusOne = false;
            if (result && selectedFunction != AdvancedFunctions.RecalculateHash)
            {
                newNum = GetAdvancedFunctionNumber(selectedFunction, saveSlot, ref minusOne);
                result = ChooseNumberDialog(ref newNum, minusOne);
            }
                
            if (result)
            {
                RunAdvancedFunction(selectedFunction, saveSlot, newNum);
                saveBin.RecordLatestSaves();
                PopulateSaveSlots(slotFormControls.slotList, saveBin, formData.gameNames, true);
                fileData.fileDirty = true;
                CheckFileSlotUI(fileData);
            }
        }
    }
}
