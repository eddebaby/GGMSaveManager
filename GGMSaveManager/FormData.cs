using System.Windows.Forms;

namespace GGMSaveManager
{
    /// <summary>
    /// A collection of shared data used by various functions in GGMSaveManager.
    /// </summary>
    class FormData
    {
        public FileData[] fileData = new FileData[2]; // this is for File #1 and File #2
        public ImportedGameNames gameNames; // a list of 6 games from a romlist.txt file
        public string nameFile, sRAMFile;
        public string previousNameFile = "", previousSRAMFile = "", previousNameFilePath = "", previousSRAMFilePath = "";

        public SaveSlot copiedSaveSlot;
        public bool dirtyClipboard = false;
        public bool advancedFeatures = false;

        public OpenFileDialog sramFileOFD, nameFileOFD;
        public SaveFileDialog sramFileSFD;

        public Timer t = new Timer();

        /// <summary>
        /// Initialise shared data; used by various functions in GGMSaveManager.
        /// </summary>
        public FormData()
        {
            // Timer t is to resolve a bug in the OpenFileDialog (filename text is missing to left of box)
            t.Interval = 100;
            t.Tick += (s, e) =>
            {
                SendKeys.Send("{HOME}");
                t.Stop();
            };

            gameNames = new ImportedGameNames();
        }
    }
}
