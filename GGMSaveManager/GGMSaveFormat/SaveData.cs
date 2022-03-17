namespace GGMSaveManager
{
    /// <summary>
    /// Each SaveSlot contains a single piece of SaveData
    /// </summary>
    class SaveData
    {
        public int dataLength = GGMSaveBin.slotLength;
        public byte[] data;

        /// <summary>
        /// Create a blank SaveData object
        /// </summary>
        public SaveData()
        {
            dataLength = 0;
            data = SaveSlot.ZeroRemainingData(1);
        }

        /// <summary>
        /// Create a SaveData object from a byte array containing raw data
        /// from a save.bin file
        /// </summary>
        public SaveData(byte[] data)
        {  
            dataLength = data.Length;
            this.data = data;
        }
    }
}
