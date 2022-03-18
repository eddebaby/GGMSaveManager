using System;
using System.Windows.Forms;

namespace GGMSaveManager
{
    public partial class frmMain : Form
    {
        private FormData formData;

        /// <summary>
        /// The main form for GGM Save Manager. Allows the user to open and edit GGM save.bin files.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            // Setup all form controls
            formData = new FormData();

            SlotFormControls newSlotFormControls = new SlotFormControls(btnOpenFile1, btnSaveFile1, btnSaveAsFile1, btnNewFile1, btnLoadGameNames, 
                txtFile1, lblSlot1, btnCopySlotFile1, btnPasteSlotFile1, btnClearSlotFile1, btnImportSRAMSlotFile1, 
                btnExportSRAMSlotFile1, lblSet1, chkAdvancedFeatures, btnChangeVersionSlotFile1, btnChangeSaveStateIDSlotFile1,
                btnChangeGameIDSlotFile1, btnChangeSRAMSizeSlotFile1, btnRecalculateHashSlotFile1, listBoxFile1);

            formData.fileData[0] = new FileData(0, saveFileDialog2, openFileDialog1, newSlotFormControls, formData);

            newSlotFormControls = new SlotFormControls(btnOpenFile2, btnSaveFile2, btnSaveAsFile2, btnNewFile2, btnLoadGameNames,
                txtFile2, lblSlot2, btnCopySlotFile2, btnPasteSlotFile2, btnClearSlotFile2, btnImportSRAMSlotFile2,
                btnExportSRAMSlotFile2, lblSet2, chkAdvancedFeatures, btnChangeVersionSlotFile2, btnChangeSaveStateIDSlotFile2,
                btnChangeGameIDSlotFile2, btnChangeSRAMSizeSlotFile2, btnRecalculateHashSlotFile2, listBoxFile2);

            formData.fileData[1] = new FileData(1, saveFileDialog2, openFileDialog2, newSlotFormControls, formData);

            formData.sramFileOFD = openFileDialog4;
            formData.sramFileSFD = saveFileDialog1;
            formData.nameFileOFD = openFileDialog3;
    }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = FormOperations.AskCancelExit(formData.fileData[0].fileDirty | formData.fileData[1].fileDirty);
        }

        private void listBoxFile1_DrawItem(object sender, DrawItemEventArgs e)
        {
            FormOperations.DrawSlotsListBox(sender, e, formData.fileData[0].saveBin);
        }

        private void listBoxFile2_DrawItem(object sender, DrawItemEventArgs e)
        {
            FormOperations.DrawSlotsListBox(sender, e, formData.fileData[1].saveBin);
        }

        private void listBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            // Handle the MeasureItem event for an owner-drawn ListBox, adjust item height to match font size (for DPI).
            e.ItemHeight = DPI.MultiplyByDPIRatio(((ListBox)sender).Font.Height);
        }

        private void listBoxFile1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormOperations.CheckFileSlotUI(formData.fileData[0]);
        }

        private void listBoxFile2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormOperations.CheckFileSlotUI(formData.fileData[1]);
        }

        private void chkAdvancedFeatures_CheckedChanged(object sender, EventArgs e)
        {
            FormOperations.ToggleAdvancedFeatures(formData, chkAdvancedFeatures.Checked);
        }

        private void btnOpenFile1_Click(object sender, EventArgs e)
        {
            // Open file #1
            if (FormOperations.AskOpenBinFile(formData, 0))
            {
                FormOperations.UpdateFileUI(formData.fileData[0]);
            }
        }

        private void btnOpenFile2_Click(object sender, EventArgs e)
        {
            // Open File #2
            if (FormOperations.AskOpenBinFile(formData, 1))
            {
                FormOperations.UpdateFileUI(formData.fileData[1]);
            }
        }

        private void btnSaveFile1_Click(object sender, EventArgs e)
        {
            // Save File #1
            if (FormOperations.AskSaveBinFile(formData, 0))
            {
                FormOperations.UpdateFileUI(formData.fileData[0], true);
            }  
        }

        private void btnSaveFile2_Click(object sender, EventArgs e)
        {
            // Save File #2
            if (FormOperations.AskSaveBinFile(formData, 1))
            {
                FormOperations.UpdateFileUI(formData.fileData[1], true);
            }
        }

        private void btnSaveAsFile1_Click(object sender, EventArgs e)
        {
            // Save As for File #1
            if (FormOperations.AskSaveBinFile(formData, 0, true))
            {
                FormOperations.UpdateFileUI(formData.fileData[0], true);
            }
        }

        private void btnSaveAsFile2_Click(object sender, EventArgs e)
        {
            // Save As for File #2
            if (FormOperations.AskSaveBinFile(formData, 1, true))
            {
                FormOperations.UpdateFileUI(formData.fileData[1], true);
            }
        }

        private void btnNewFile1_Click(object sender, EventArgs e)
        {
            // create a new (unsaved) file in File #1
            if (FileOperations.NewBinFile(ref formData.fileData[0].saveBin, ref formData.fileData[0].saveFile, ref formData.fileData[0].fileDirty))
            {
                FormOperations.UpdateFileUI(formData.fileData[0], false, true);
            }
        }

        private void btnNewFile2_Click(object sender, EventArgs e)
        {
            // create a new (unsaved) file in File #2
            if (FileOperations.NewBinFile(ref formData.fileData[1].saveBin, ref formData.fileData[1].saveFile, ref formData.fileData[1].fileDirty))
            {
                FormOperations.UpdateFileUI(formData.fileData[1], false, true);
            }
        }

        private void btnLoadGameNames_Click(object sender, EventArgs e)
        {
            // open GG Game names file (romlist.txt from GGM CFW tool by Augen)
            if (FormOperations.AskOpenRomList(formData))
            {

            }
        }
        
        private void btnClearSlotFile1_Click(object sender, EventArgs e)
        {
            // clear a slot in 1st save file
            FormOperations.CheckClearSlot(formData.fileData[0]);
        }

        private void btnClearSlotFile2_Click(object sender, EventArgs e)
        {
            // clear a slot in 2nd save file
            FormOperations.CheckClearSlot(formData.fileData[1]);
        }

        private void btnCopySlotFile1_Click(object sender, EventArgs e)
        {
            // copy slot in 1st save file
            FormOperations.CheckCopySlot(formData.fileData[0]);
            FormOperations.CheckFileSlotUI(formData.fileData[1]); // update UI for other file
        }

        private void btnCopySlotFile2_Click(object sender, EventArgs e)
        {
            // copy slot in 2nd save file
            FormOperations.CheckCopySlot(formData.fileData[1]);
            FormOperations.CheckFileSlotUI(formData.fileData[0]); // update UI for other file
        }

        private void btnPasteSlotFile1_Click(object sender, EventArgs e)
        {
            // paste to a slot in 1st save file
            FormOperations.CheckPasteSlot(formData.fileData[0]);
            FormOperations.CheckFileSlotUI(formData.fileData[1]); // update UI for other file
        }

        private void btnPasteSlotFile2_Click(object sender, EventArgs e)
        {
            // paste to a slot in 2nd save file
            FormOperations.CheckPasteSlot(formData.fileData[1]);
            FormOperations.CheckFileSlotUI(formData.fileData[0]); // update UI for other file
        }

        private void btnExportSRAMSlotFile1_Click(object sender, EventArgs e)
        {
            // export SRAM data from slot in 1st save file
            FormOperations.CheckExportSRAMData(formData, 0);
        }

        private void btnExportSRAMSlotFile2_Click(object sender, EventArgs e)
        {
            // export SRAM data from slot in 2nd save file
            FormOperations.CheckExportSRAMData(formData, 1);
        }

        private void btnImportSRAMSlotFile1_Click(object sender, EventArgs e)
        {
            // import SRAM data to slot in 1st save file
            FormOperations.CheckImportSRAMData(formData, 0);
        }

        private void btnImportSRAMSlotFile2_Click(object sender, EventArgs e)
        {
            // import SRAM data to slot in 2nd save file
            FormOperations.CheckImportSRAMData(formData, 1);
        }

        private void btnChangeGameIDSlotFile1_Click(object sender, EventArgs e)
        {
            // change game number for slot in 1st save file
            FormOperations.CheckAdvancedFunction(formData, 0, AdvancedFunctions.ChangeGameID);
        }

        private void btnChangeGameIDSlotFile2_Click(object sender, EventArgs e)
        {
            // change game number for slot in 2nd save file
            FormOperations.CheckAdvancedFunction(formData, 1, AdvancedFunctions.ChangeGameID);
        }

        private void btnChangeSaveStateIDSlotFile1_Click(object sender, EventArgs e)
        {
            // change save slot number for slot in 1st save file
            FormOperations.CheckAdvancedFunction(formData, 0, AdvancedFunctions.ChangeSaveStateID);
        }

        private void btnChangeSaveStateIDSlotFile2_Click(object sender, EventArgs e)
        {
            // change save slot number for slot in 2nd save file
            FormOperations.CheckAdvancedFunction(formData, 1, AdvancedFunctions.ChangeSaveStateID);
        }

        private void btnChangeVersionSlotFile1_Click(object sender, EventArgs e)
        {
            // change version number for slot in 1st save file
            FormOperations.CheckAdvancedFunction(formData, 0, AdvancedFunctions.ChangeVersionNumber);
        }

        private void btnChangeVersionSlotFile2_Click(object sender, EventArgs e)
        {
            // change version number for slot in 2nd save file
            FormOperations.CheckAdvancedFunction(formData, 1, AdvancedFunctions.ChangeVersionNumber);
        }

        private void btnChangeSRAMSizeSlotFile1_Click(object sender, EventArgs e)
        {
            // change SRAM size for slot in 1st save file
            FormOperations.CheckAdvancedFunction(formData, 0, AdvancedFunctions.ChangeSRAMSize);
        }

        private void btnChangeSRAMSizeSlotFile2_Click(object sender, EventArgs e)
        {
            // change SRAM size for slot in 2nd save file
            FormOperations.CheckAdvancedFunction(formData, 1, AdvancedFunctions.ChangeSRAMSize);
        }

        private void btnRecalculateHashSlotFile1_Click(object sender, EventArgs e)
        {
            // recalculate hash for slot in 1st save file
            FormOperations.CheckAdvancedFunction(formData, 0, AdvancedFunctions.RecalculateHash);
        }

        private void btnRecalculateHashSlotFile2_Click(object sender, EventArgs e)
        {
            // recalculate hash for slot in 2nd save file
            FormOperations.CheckAdvancedFunction(formData, 1, AdvancedFunctions.RecalculateHash);
        }
    }
}
