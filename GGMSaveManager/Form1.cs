using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GGMSaveManager
{
    public partial class Form1 : Form
    {
        GGMSaveBin saveBin1, saveBin2;
        ImportedGameNames gameNames;
        string saveFile1, saveFile2, nameFile, sRAMFile;
        SaveSlot copiedSaveSlot;
        bool dirtyClipboard = false;
        string previousFile1 = "", previousFile2 = "", previousNameFile = "", previousSRAMFile = "", previousPath1 = "", previousPath2 = "", previousNameFilePath = "", previousSRAMFilePath = "";
        Timer t = new Timer();
        public bool file1Dirty = false, file2Dirty = false;
        private bool ignoreLastAction = false;

        bool advancedFeatures = false;

        public static String InputBox(String caption, String prompt, String defaultText)
        {
            // Returns the string the user entered; empty string if they hit Cancel
            String localInputText = defaultText;
            if (InputQuery(caption, prompt, ref localInputText))
            {
                return localInputText;
            }
            else
            {
                return "";
            }
        }

        public static Boolean InputQuery(String caption, String prompt, ref String value)
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
                        Toolkit.MulDiv(180, dialogUnits.Width, 4),
                        Toolkit.MulDiv(63, dialogUnits.Height, 8));

            form.StartPosition = FormStartPosition.CenterScreen;

            System.Windows.Forms.Label lblPrompt;
            lblPrompt = new System.Windows.Forms.Label();
            lblPrompt.Parent = form;
            lblPrompt.AutoSize = true;
            lblPrompt.Left = Toolkit.MulDiv(8, dialogUnits.Width, 4);
            lblPrompt.Top = Toolkit.MulDiv(8, dialogUnits.Height, 8);
            lblPrompt.Text = prompt;

            System.Windows.Forms.TextBox edInput;
            edInput = new System.Windows.Forms.TextBox();
            edInput.Parent = form;
            edInput.Left = lblPrompt.Left;
            edInput.Top = Toolkit.MulDiv(19, dialogUnits.Height, 8);
            edInput.Width = Toolkit.MulDiv(164, dialogUnits.Width, 4);
            edInput.Text = value;
            edInput.SelectAll();


            int buttonTop = Toolkit.MulDiv(41, dialogUnits.Height, 8);
            //Command buttons should be 50x14 dlus
            Size buttonSize = Toolkit.ScaleSize(new Size(72, 27), dialogUnits.Width / 4, dialogUnits.Height / 8);

            System.Windows.Forms.Button bbOk = new System.Windows.Forms.Button();
            bbOk.Parent = form;
            bbOk.Text = "OK";
            bbOk.DialogResult = DialogResult.OK;
            form.AcceptButton = bbOk;
            bbOk.Location = new Point(Toolkit.MulDiv(38, dialogUnits.Width, 4), buttonTop);
            bbOk.Size = buttonSize;

            System.Windows.Forms.Button bbCancel = new System.Windows.Forms.Button();
            bbCancel.Parent = form;
            bbCancel.Text = "Cancel";
            bbCancel.DialogResult = DialogResult.Cancel;
            form.CancelButton = bbCancel;
            bbCancel.Location = new Point(Toolkit.MulDiv(92, (int)dialogUnits.Width, 4), buttonTop);
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

        protected virtual bool DoesFileExist(FileInfo file)
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

        protected virtual bool IsFileLocked(FileInfo file, bool isSave = true)
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
                    string message = "Can't "+ saveLoad + " File. Make sure it isn't locked by another program." + "\n\n" + "The requested operation cannot be performed on a file with a user-mapped section open.";
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

        public Form1()
        {
            InitializeComponent();
            t.Interval = 100;
            t.Tick += (s, e) =>
            {
                SendKeys.Send("{HOME}");
                t.Stop();
            };
            gameNames = new ImportedGameNames();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font f = e.Font;
            Color c= e.ForeColor;
            if (IsLatestSaveData(e.Index, saveBin1))
                f = new Font(e.Font, FontStyle.Bold);
            if (saveBin1.saveSlots[e.Index].isCorrupt)
                c = Color.Red;
            e.DrawBackground();
            e.Graphics.DrawString(((ListBox)(sender)).Items[e.Index].ToString(), f, new SolidBrush(c), e.Bounds);
            e.DrawFocusRectangle();
        }

        private void listBox2_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font f = e.Font;
            Color c = e.ForeColor;
            if (IsLatestSaveData(e.Index, saveBin2))
                f = new Font(e.Font, FontStyle.Bold);
            if (saveBin2.saveSlots[e.Index].isCorrupt)
                c = Color.Red;
            e.DrawBackground();
            e.Graphics.DrawString(((ListBox)(sender)).Items[e.Index].ToString(), f, new SolidBrush(c), e.Bounds);
            e.DrawFocusRectangle();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (file1Dirty | file2Dirty)
            { 
                const string message = "You have made changes to the Game Gear Micro save data." + "\n" + "Are you sure you want to quit?";
                const string caption = "Exit with Unsaved changes";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

                e.Cancel = (result == DialogResult.No);
            }
        }

        private bool IsLatestSaveData(int index, GGMSaveBin saveBin)
        {
            if (!saveBin.saveSlots[index].isEmpty && saveBin.saveSlots[index].versionNumber == saveBin.latestSaves[saveBin.saveSlots[index].saveID])
            {
                return true;
            }
            return false;
        }

        private bool IsLatestSaveData(SaveSlot currentSaveSlot, GGMSaveBin saveBin)
        {
            if (!currentSaveSlot.isEmpty && currentSaveSlot.versionNumber == saveBin.latestSaves[currentSaveSlot.saveID])
            {
                return true;
            }
            return false;
        }

        private void PopulateSaveSlots(ListBox listBox, GGMSaveBin saveBin, bool restoreSelection = false)
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

        private void PrepareSRAMForExport(int fileNumber, int slotNumber)
        {
            GGMSaveBin selectedGGMSaveBin;
            if (fileNumber == 1) selectedGGMSaveBin = saveBin1;
            else selectedGGMSaveBin = saveBin2;
            
            SaveSlot currentSlot = selectedGGMSaveBin.saveSlots[slotNumber];
            string gameFileName = gameNames.nameList[currentSlot.gameNumber - 1];
            ExportSRAMData(gameFileName, currentSlot);
        }

        private void ExportSRAMData(string gameFileName, SaveSlot currentSaveSlot)
        {
            // Export passed save slot
            if (previousSRAMFile != "")
            {
                saveFileDialog1.FileName = previousSRAMFile;
                saveFileDialog1.InitialDirectory = previousSRAMFilePath;
            }

            saveFileDialog1.Title = "Export Game Gear SRAM file";
            saveFileDialog1.FileName = gameFileName;

            t.Start(); // workaround for bug: filename offset to the left
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sRAMFile = saveFileDialog1.FileName; // output file to export SRAM data to
  
                if (!IsFileLocked(new FileInfo(sRAMFile))) File.WriteAllBytes(sRAMFile, currentSaveSlot.saveData.data); // save the data!

                previousSRAMFile = Path.GetFileName(sRAMFile);
                previousSRAMFilePath = Path.GetDirectoryName(sRAMFile);
            }
        }

        private void PrepareSRAMForImport(int fileNumber, int slotNumber)
        {
            GGMSaveBin selectedGGMSaveBin;
            if (fileNumber == 1) selectedGGMSaveBin = saveBin1;
            else selectedGGMSaveBin = saveBin2;

            SaveSlot currentSlot = selectedGGMSaveBin.saveSlots[slotNumber];
            string gameFileName = gameNames.nameList[currentSlot.gameNumber - 1];
            ImportSRAMData(gameFileName, currentSlot, selectedGGMSaveBin);
        }

        private bool CheckSRAMData(byte[] inputData, string filename, SaveSlot currentSaveSlot, GGMSaveBin saveBin)
        {
            bool SRAMDataOK = false;

            string introMessage = "Current Slot: " + gameNames.nameList[currentSaveSlot.gameNumber - 1] + "\nSRAM File: "+ filename;
            // Check file size
            if (inputData.Length > currentSaveSlot.sRAMDataLength)
            {
                string message = introMessage + "\n\nThe SRAM file is larger than the existing SRAM data. \nIt is probably from a different game." + "\n\n" + "Do you want to continue importing this SRAM data anyway?";
                string caption = "Warning whilst loading " + filename;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Error);
                SRAMDataOK = (result == DialogResult.Yes);
            }
            else if (inputData.Length <= (currentSaveSlot.sRAMDataLength / 2))
            {
                string message = introMessage + "\n\nThe SRAM file is significantly smaller than the existing SRAM data. \nIt is probably from a different game." + "\n\n" + "Do you want to continue importing this SRAM data anyway?";
                string caption = "Warning whilst loading " + filename;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Warning);
                SRAMDataOK = (result == DialogResult.Yes);
            }
            else if (inputData.Length < currentSaveSlot.sRAMDataLength)
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
            if (SRAMDataOK && !IsLatestSaveData(currentSaveSlot, saveBin1))
            {
                string message = "The current save slot is not the latest for the game \'"+ gameNames.nameList[currentSaveSlot.gameNumber - 1] + "\'. \nSo this imported SRAM data won't be loaded by the GGM.  \n\nDo you want to change this slot to be the latest?\n\nYes: Change slot to latest version number\nNo: Keep current version number\nCancel: Cancel importing this SRAM data.";
                string caption = "Warning whilst loading " + filename;
                var result = MessageBox.Show(message, caption,
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    currentSaveSlot.ChangeVersionNumber(saveBin.latestSaves[currentSaveSlot.saveID] + 1);
                    saveBin.RecordLatestSaves();
                }
                else if(result == DialogResult.No)
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

        private void ImportSRAMData(string gameFileName, SaveSlot currentSaveSlot, GGMSaveBin saveBin)
        {
            // Import SRAM data to passed save slot
            if (previousSRAMFile != "")
            {
                openFileDialog4.FileName = previousSRAMFile;
                openFileDialog4.InitialDirectory = previousSRAMFilePath;
            }

            openFileDialog4.Title = "Import Game Gear SRAM file";
            openFileDialog4.FileName = gameFileName;

            ignoreLastAction = true;

            t.Start(); // workaround for bug: filename offset to the left
            if (openFileDialog4.ShowDialog() == DialogResult.OK)
            {
                sRAMFile = openFileDialog4.FileName; // input file to import SRAM data from

                // Import SRM data...
                byte[] data = File.ReadAllBytes(sRAMFile); // read input data
                if (CheckSRAMData(data, Path.GetFileName(sRAMFile), currentSaveSlot, saveBin)) // ask user about import
                {
                    currentSaveSlot.ImportSRAMData(data);

                    previousSRAMFile = Path.GetFileName(sRAMFile);
                    previousSRAMFilePath = Path.GetDirectoryName(sRAMFile);
                    ignoreLastAction = false;
                }
                
            }
        }

        private bool UserWantsToDiscardDirtyFile()
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

        private void UpdateFile1UI(bool restoreSelection = false, bool keepDirty = false)
        {
            if (!keepDirty) file1Dirty = false;
            PopulateSaveSlots(listBox1, saveBin1, restoreSelection);
            textBox1.Text = saveFile1;
            button3.Enabled = button28.Enabled = true;
            CheckFile1SlotUI();
        }

        private void CheckFile1SlotUI()
        {
            button3.Enabled = file1Dirty;
            if (listBox1.SelectedIndex != -1)
            {
                button4.Enabled = !saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty;
                button5.Enabled = dirtyClipboard;
                button6.Enabled = !saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty;
                button7.Enabled = saveBin1.saveSlots[listBox1.SelectedIndex].isSRAM && !saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty;
                button9.Enabled = saveBin1.saveSlots[listBox1.SelectedIndex].isSRAM && !saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty;

                label3.Enabled = advancedFeatures;

                if (advancedFeatures) button10.Enabled = true;
                else button10.Enabled = false;
                if (advancedFeatures) button11.Enabled = !saveBin1.saveSlots[listBox1.SelectedIndex].isSRAM && !saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty;
                else button11.Enabled = false;
                if (advancedFeatures) button12.Enabled = !saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty;
                else button12.Enabled = false;
                if (advancedFeatures) button17.Enabled = saveBin1.saveSlots[listBox1.SelectedIndex].isSRAM && !saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty;
                else button17.Enabled = false;
                if (advancedFeatures && saveBin1.saveSlots[listBox1.SelectedIndex].isCorrupt) button25.Enabled = !saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty;
                else button25.Enabled = false;

                label1.Text = "Slot " + (listBox1.SelectedIndex + 1);
            }
            else
            {
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                label1.Text = "Slot";
            }
            listBox1.Focus(); 
        }

        private void UpdateFile2UI(bool restoreSelection = false, bool keepDirty = false)
        {
            if (!keepDirty) file2Dirty = false;
            PopulateSaveSlots(listBox2, saveBin2, restoreSelection);
            textBox2.Text = saveFile2;
            button22.Enabled = button29.Enabled = true;
            CheckFile2SlotUI();
        }

        private void CheckFile2SlotUI()
        {
            button22.Enabled = file2Dirty;
            if (listBox2.SelectedIndex != -1)
            {
                button21.Enabled = !saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty;
                button20.Enabled = dirtyClipboard;
                button19.Enabled = !saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty;
                button18.Enabled = saveBin2.saveSlots[listBox2.SelectedIndex].isSRAM && !saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty;
                button16.Enabled = saveBin2.saveSlots[listBox2.SelectedIndex].isSRAM && !saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty;

                label4.Enabled = advancedFeatures;

                if (advancedFeatures) button15.Enabled = true;
                else button15.Enabled = false;
                if (advancedFeatures) button14.Enabled = !saveBin2.saveSlots[listBox2.SelectedIndex].isSRAM && !saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty;
                else button14.Enabled = false;
                if (advancedFeatures) button13.Enabled = !saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty;
                else button13.Enabled = false;
                if (advancedFeatures) button27.Enabled = saveBin2.saveSlots[listBox2.SelectedIndex].isSRAM && !saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty;
                else button27.Enabled = false;
                if (advancedFeatures && saveBin2.saveSlots[listBox2.SelectedIndex].isCorrupt) button26.Enabled = !saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty;
                else button26.Enabled = false;
                label2.Text = "Slot " + (listBox2.SelectedIndex + 1);
            }
            else
            {
                button21.Enabled = false;
                button20.Enabled = false;
                button19.Enabled = false;
                button18.Enabled = false;
                button16.Enabled = false;
                button15.Enabled = false;
                button14.Enabled = false;
                button13.Enabled = false;
                label2.Text = "Slot";
            }
            listBox2.Focus();
        }

        private bool SaveBinFile(GGMSaveBin saveBin, ref string saveFile, ref string previousFile, ref string previousPath, bool saveAs = false)
        {
            if (saveFile == "" || saveAs)
            {
                if (previousFile != "")
                {
                    saveFileDialog2.FileName = previousFile;
                    saveFileDialog2.InitialDirectory = previousPath;
                }

                saveFileDialog2.Title = "Save Game Gear Micro save data";

                t.Start(); // workaround for bug: filename offset to the left
                if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    saveFile = saveFileDialog2.FileName;

                    previousFile = Path.GetFileName(saveFile);
                    previousPath = Path.GetDirectoryName(saveFile);
                }
            }
            bool success = false;
            if (saveFile != "")
            {
                success = !IsFileLocked(new FileInfo(saveFile));
                if (success) File.WriteAllBytes(saveFile, GGMSaveBin.FileData(saveBin)); // save the data!
            }
            return success;
        }

        private int ChooseNumberDialog(int previousNumber, bool minusOne = false)
        {
            /*const string message = "Not yet implemented." + "\n\n" + "I need to know the checksum format to be able to save these changes.";
            const string caption = "Unimpemented feature";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);*/


            // todo: add number selection...
            int minusOneAmount = (minusOne ? -1 : 0);
            const string message = "Enter the new number.";
            const string caption = "Choose Number";
            string returnValue = (previousNumber - minusOneAmount).ToString();
            ignoreLastAction = true;
            
            if (InputQuery(caption, message, ref returnValue))
            {
                if (int.TryParse(returnValue, out int theNum))
                {
                    //if (previousNumber != theNum + minusOneAmount)
                    //{
                        previousNumber = theNum + minusOneAmount;
                        ignoreLastAction = false;
                    //}
                }      
            }

            return previousNumber;
        }

        private void chkAdvancedFeatures_CheckedChanged(object sender, EventArgs e)
        {
            advancedFeatures = chkAdvancedFeatures.Checked;
            if (saveBin1 != null) UpdateFile1UI(true);
            if (saveBin2 != null) UpdateFile2UI(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Open file #1

            if (file1Dirty)
                if (!UserWantsToDiscardDirtyFile())
                    return;

            if (previousFile1 != "")
            {
                openFileDialog1.FileName = previousFile1;
                openFileDialog1.InitialDirectory = previousPath1;
            }

            openFileDialog1.Title = "Open Game Gear Micro save data";

            t.Start(); // workaround for bug: filename offset to the left
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFile1 = openFileDialog1.FileName; // input file with GGM save data
                previousFile1 = Path.GetFileName(saveFile1);
                previousPath1 = Path.GetDirectoryName(saveFile1);

                bool success = false;
                if (saveFile1 != "")
                {
                    success = !IsFileLocked(new FileInfo(saveFile1), false);
                    if (success)
                    {
                        byte[] data = File.ReadAllBytes(saveFile1); // read input data
                        saveBin1 = new GGMSaveBin(data);
                        UpdateFile1UI();
                    }
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            // Open File #2

            if (file2Dirty)
                if (!UserWantsToDiscardDirtyFile())
                    return;

            if (previousFile2 != "")
            {
                openFileDialog2.FileName = previousFile2;
                openFileDialog2.InitialDirectory = previousPath2;
            }

            openFileDialog2.Title = "Open Game Gear Micro save data";

            t.Start(); // workaround for bug: filename offset to the left
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                saveFile2 = openFileDialog2.FileName; // input file with GGM save data
                byte[] data = File.ReadAllBytes(saveFile2); // read input data
                saveBin2 = new GGMSaveBin(data);
                previousFile2 = Path.GetFileName(saveFile2);
                previousPath2 = Path.GetDirectoryName(saveFile2);

                UpdateFile2UI();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Save File #1
            if (SaveBinFile(saveBin1, ref saveFile1, ref previousFile1, ref previousPath1))
            {
                UpdateFile1UI(true);
            }  
        }

        private void button22_Click(object sender, EventArgs e)
        {
            // Save File #2
            if (SaveBinFile(saveBin2, ref saveFile2, ref previousFile2, ref previousPath2))
            {
                UpdateFile2UI(true);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            // Save As for File #1
            if (SaveBinFile(saveBin1, ref saveFile1, ref previousFile1, ref previousPath1, true))
            {
                UpdateFile1UI(true);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            // Save As for File #2
            if (SaveBinFile(saveBin2, ref saveFile2, ref previousFile2, ref previousPath2, true))
            {
                UpdateFile2UI(true);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // create a new (unsaved) file in File #1

            if (file1Dirty)
                if (!UserWantsToDiscardDirtyFile())
                    return;

            saveFile1 = ""; // no file attached currently
            saveBin1 = new GGMSaveBin(); // create blank GGM save.bin
            file1Dirty = true;
            UpdateFile1UI(false, true);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            // create a new (unsaved) file in File #2

            if (file2Dirty)
                if (!UserWantsToDiscardDirtyFile())
                    return;

            saveFile2 = ""; // no file attached currently
            saveBin2 = new GGMSaveBin(); // create blank GGM save.bin
            file2Dirty = true;
            UpdateFile2UI(false, true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // open GG Game names file (romlist.txt from GGM CFW tool by Augen)
            if (previousNameFile != "")
            {
                openFileDialog3.FileName = previousNameFile;
                openFileDialog3.InitialDirectory = previousNameFilePath;
            }

            openFileDialog3.Title = "Open List of 6 Game Gear games";

            t.Start(); // workaround for bug: filename offset to the left
            if (openFileDialog3.ShowDialog() == DialogResult.OK)
            {
                nameFile = openFileDialog3.FileName; // input file with GG games list

                previousNameFile = Path.GetFileName(nameFile);
                previousNameFilePath = Path.GetDirectoryName(nameFile);

                gameNames.ImportGameNames(File.ReadAllLines(nameFile));
                if (saveFile1 != null) PopulateSaveSlots(listBox1, saveBin1, true);
                if (saveFile2 != null) PopulateSaveSlots(listBox2, saveBin2, true);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckFile1SlotUI();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckFile2SlotUI();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // clear a slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button6.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            // Clear currently selected slot
            saveBin1.saveSlots[listBox1.SelectedIndex] = new SaveSlot();
            PopulateSaveSlots(listBox1, saveBin1, true);
            file1Dirty = true;
            CheckFile1SlotUI();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            // clear a slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button19.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            // Clear currently selected slot
            saveBin2.saveSlots[listBox2.SelectedIndex] = new SaveSlot();
            PopulateSaveSlots(listBox2, saveBin2, true);
            file2Dirty = true;
            CheckFile2SlotUI();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // copy slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button4.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            //copiedSaveSlot = new SaveSlot();
            copiedSaveSlot = saveBin1.saveSlots[listBox1.SelectedIndex];
            dirtyClipboard = true;
            button5.Enabled = dirtyClipboard;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            // copy slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button21.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            //copiedSaveSlot = new SaveSlot();
            copiedSaveSlot = saveBin2.saveSlots[listBox2.SelectedIndex];
            dirtyClipboard = true;
            button20.Enabled = dirtyClipboard;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            // paste to a slot in 1st save file
            if (index == -1)
            {
                button5.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            saveBin1.saveSlots[index] = new SaveSlot(copiedSaveSlot);
            dirtyClipboard = false;
            button5.Enabled = dirtyClipboard;
            PopulateSaveSlots(listBox1, saveBin1, true);
            file1Dirty = true;
            CheckFile1SlotUI();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            int index = listBox2.SelectedIndex;
            // paste to a slot in 2nd save file
            if (index == -1)
            {
                button20.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            saveBin2.saveSlots[index] = new SaveSlot(copiedSaveSlot);
            dirtyClipboard = false;
            button20.Enabled = dirtyClipboard;
            PopulateSaveSlots(listBox2, saveBin2, true);
            file2Dirty = true;
            CheckFile2SlotUI();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // export SRAM data from slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button6.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveBin1.saveSlots[listBox1.SelectedIndex].isSRAM)
            {
                button6.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            PrepareSRAMForExport(1, listBox1.SelectedIndex);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // export SRAM data from slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button18.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveBin2.saveSlots[listBox2.SelectedIndex].isSRAM)
            {
                button18.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            PrepareSRAMForExport(2, listBox2.SelectedIndex);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // import SRAM data to slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button9.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveBin1.saveSlots[listBox1.SelectedIndex].isSRAM)
            {
                button9.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            PrepareSRAMForImport(1, listBox1.SelectedIndex);
            if (!ignoreLastAction)
            {
                file1Dirty = true;
                UpdateFile1UI(true, true);
            }
            else ignoreLastAction = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // import SRAM data to slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button16.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveBin2.saveSlots[listBox2.SelectedIndex].isSRAM)
            {
                button16.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            PrepareSRAMForImport(2, listBox2.SelectedIndex);
            if (!ignoreLastAction)
            {
                file2Dirty = true;
                UpdateFile2UI(true, true);
            }
            else ignoreLastAction = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // change game number for slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button10.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            int newNum = ChooseNumberDialog(saveBin1.saveSlots[listBox1.SelectedIndex].gameNumber);
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin1.saveSlots[listBox1.SelectedIndex].ChangeGameNumber(newNum);
                saveBin1.RecordLatestSaves();
                PopulateSaveSlots(listBox1, saveBin1, true);
                file1Dirty = true;
                CheckFile1SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // change game number for slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button10.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            int newNum = ChooseNumberDialog(saveBin2.saveSlots[listBox2.SelectedIndex].gameNumber);
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin2.saveSlots[listBox2.SelectedIndex].ChangeGameNumber(newNum);
                saveBin2.RecordLatestSaves();
                PopulateSaveSlots(listBox2, saveBin2, true);
                file2Dirty = true;
                CheckFile2SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // change save slot number for slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button11.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty)
            {
                button11.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            int newNum = ChooseNumberDialog(saveBin1.saveSlots[listBox1.SelectedIndex].saveSlotNumber);
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin1.saveSlots[listBox1.SelectedIndex].ChangeSaveStateSlot(newNum);
                saveBin1.RecordLatestSaves();
                PopulateSaveSlots(listBox1, saveBin1, true);
                file1Dirty = true;
                CheckFile1SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // change save slot number for slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button14.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty)
            {
                button14.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            int newNum = ChooseNumberDialog(saveBin2.saveSlots[listBox2.SelectedIndex].saveSlotNumber);
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin2.saveSlots[listBox2.SelectedIndex].ChangeSaveStateSlot(newNum);
                saveBin2.RecordLatestSaves();
                PopulateSaveSlots(listBox2, saveBin2, true);
                file2Dirty = true;
                CheckFile2SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // change version number for slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button12.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty)
            {
                button12.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            UInt32 newNum = (UInt32)ChooseNumberDialog((int)saveBin1.saveSlots[listBox1.SelectedIndex].versionNumber, true);
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin1.saveSlots[listBox1.SelectedIndex].ChangeVersionNumber(newNum);
                saveBin1.RecordLatestSaves();
                PopulateSaveSlots(listBox1, saveBin1, true);
                file1Dirty = true;
                CheckFile1SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // change version number for slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button13.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty)
            {
                button13.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }

            UInt32 newNum = (UInt32)ChooseNumberDialog((int)saveBin2.saveSlots[listBox2.SelectedIndex].versionNumber, true);
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin2.saveSlots[listBox2.SelectedIndex].ChangeVersionNumber(newNum);
                saveBin2.RecordLatestSaves();
                PopulateSaveSlots(listBox2, saveBin2, true);
                file2Dirty = true;
                CheckFile2SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // change SRAM size for slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button17.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty)
            {
                button17.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveBin1.saveSlots[listBox1.SelectedIndex].isSRAM)
            {
                button17.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            UInt32 newNum = (UInt32)ChooseNumberDialog((int)saveBin1.saveSlots[listBox1.SelectedIndex].sRAMDataLength / 1024);
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin1.saveSlots[listBox1.SelectedIndex].ChangeSRAMLength(newNum * 1024);
                saveBin1.RecordLatestSaves();
                PopulateSaveSlots(listBox1, saveBin1, true);
                file1Dirty = true;
                CheckFile1SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            // change SRAM size for slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button27.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty)
            {
                button27.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (!saveBin2.saveSlots[listBox2.SelectedIndex].isSRAM)
            {
                button27.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            UInt32 newNum = (UInt32)ChooseNumberDialog((int)saveBin2.saveSlots[listBox2.SelectedIndex].sRAMDataLength / 1024);
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin2.saveSlots[listBox2.SelectedIndex].ChangeSRAMLength(newNum * 1024);
                saveBin2.RecordLatestSaves();
                PopulateSaveSlots(listBox2, saveBin2, true);
                file2Dirty = true;
                CheckFile2SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            // recalculate hash for slot in 1st save file
            if (listBox1.SelectedIndex == -1)
            {
                button25.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (saveBin1.saveSlots[listBox1.SelectedIndex].isEmpty)
            {
                button25.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin1.saveSlots[listBox1.SelectedIndex].ReCalculateHash();
                saveBin1.RecordLatestSaves();
                PopulateSaveSlots(listBox1, saveBin1, true);
                file1Dirty = true;
                CheckFile1SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            // recalculate hash for slot in 2nd save file
            if (listBox2.SelectedIndex == -1)
            {
                button26.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            else if (saveBin2.saveSlots[listBox2.SelectedIndex].isEmpty)
            {
                button26.Enabled = false; // just in case, although this is a silent bugfix to user
                return;
            }
            if (advancedFeatures && !ignoreLastAction)
            {
                saveBin2.saveSlots[listBox2.SelectedIndex].ReCalculateHash();
                saveBin2.RecordLatestSaves();
                PopulateSaveSlots(listBox2, saveBin2, true);
                file2Dirty = true;
                CheckFile2SlotUI();
            }
            else if (ignoreLastAction) ignoreLastAction = false;
        }
    }
}
