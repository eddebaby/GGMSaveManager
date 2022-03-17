using System.Windows.Forms;

namespace GGMSaveManager
{
    /// <summary>
    /// An object containing all of the controls for a specific File in the UI 
    /// </summary>
    class SlotFormControls
    {
        public Button fileOpen;
        public Button fileSave;
        public Button fileSaveAs;
        public Button fileNew;
        public Button loadGameNames;

        public TextBox fileName;

        public Label labelSlot;
        public Button slotCopy;
        public Button slotPaste;
        public Button slotClear;
        public Button slotImportSRAM;
        public Button slotExportSRAM;

        public Label labelSet;
        public CheckBox advancedSettings;

        public Button slotVersion;
        public Button slotSaveStateID;
        public Button slotGameID;
        public Button slotSRAMSize;
        public Button slotHash;

        public ListBox slotList;

        /// <summary>
        /// Import buttons used for a particular File in the UI into an easy to interpret object
        /// </summary>
        public SlotFormControls(Button fileOpen, Button fileSave, Button fileSaveAs, Button fileNew, Button loadGameNames, TextBox fileName, 
            Label labelSlot, Button slotCopy, Button slotPaste, Button slotClear, Button slotImportSRAM, Button slotExportSRAM, 
            Label labelSet, CheckBox advancedSettings, Button slotVersion, Button slotSaveStateID, Button slotGameID, Button slotSRAMSize, 
            Button slotHash, ListBox slotList)
        {
            this.fileOpen = fileOpen;
            this.fileSave = fileSave;
            this.fileSaveAs = fileSaveAs;
            this.fileNew = fileNew;
            this.loadGameNames = loadGameNames;

            this.fileName = fileName;

            this.labelSlot = labelSlot;
            this.slotCopy = slotCopy;
            this.slotPaste = slotPaste;
            this.slotClear = slotClear;
            this.slotImportSRAM = slotImportSRAM;
            this.slotExportSRAM = slotExportSRAM;

            this.labelSet = labelSet;
            this.advancedSettings = advancedSettings;

            this.slotVersion = slotVersion;
            this.slotSaveStateID = slotSaveStateID;
            this.slotGameID = slotGameID;
            this.slotSRAMSize = slotSRAMSize;
            this.slotHash = slotHash;

            this.slotList = slotList;
        }
    }
}
