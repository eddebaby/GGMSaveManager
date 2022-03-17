using System.Windows.Forms;

namespace GGMSaveManager
{
    class FileData
    {
        public int index;
        public GGMSaveBin saveBin;
        public string saveFile;
        public string previousFile = "", previousPath = "";
        public bool fileDirty = false;

        public SaveFileDialog saveFileDialog;
        public OpenFileDialog openFileDialog;

        public SlotFormControls slotFormControls;

        public FormData parentFormData;

        /// <summary>
        /// Initialise data for File number 1 or 2; used by various functions in GGMSaveManager.
        /// </summary>
        public FileData(int index, SaveFileDialog saveFileDialog, OpenFileDialog openFileDialog, SlotFormControls slotFormControls, FormData formData)
        {
            this.index = index;
            this.saveFileDialog = saveFileDialog;
            this.openFileDialog = openFileDialog;
            this.slotFormControls = slotFormControls;
            parentFormData = formData;
        }
    }
}
