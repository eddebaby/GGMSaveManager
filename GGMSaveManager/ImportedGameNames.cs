using System;
using System.Text.RegularExpressions;

namespace GGMSaveManager
{
    class ImportedGameNames
    {
        // adapts a romlist.txt file to an array of 6 game names

        public string[] nameList = new string[GGMSaveBin.addressableGames];

        public ImportedGameNames()
        {
            InitialiseGameNames();
        }

        private void InitialiseGameNames()
        {
            for (int n = 0; n < GGMSaveBin.addressableGames; n++)
            {
                nameList[n] = "Game " + (n + 1);
            }
        }

        public void ImportGameNames(string[] gameList)
        {
            for (int n = 0; n < GGMSaveBin.addressableGames; n++)
            {
                if (gameList[n] != null) nameList[n] = gameList[n];
                nameList[n] = Regex.Replace(nameList[n], "\\.gg$", String.Empty); // remove gg extension (if present)
                nameList[n] = Regex.Replace(nameList[n], "\\.sms$", String.Empty); // remove sms extension (if present)
            }
        }
    }
}
