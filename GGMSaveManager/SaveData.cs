using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GGMSaveManager
{
    class SaveData
    {
        public int dataLength = GGMSaveBin.slotLength;
        public byte[] data;

        public SaveData()
        {
            dataLength = 0;
            data = SaveSlot.ZeroRemainingData(1);
        }

        public SaveData(byte[] data)
        {  
            dataLength = data.Length;
            this.data = data;
        }
    }
}
