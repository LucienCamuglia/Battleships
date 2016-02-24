using System;

public class FileManager
{
    String FilePath;

	public FileManager()
	{
	}

    /// <summary>
    /// Create the BNAV file for the game
    /// </summary>
    /// <param name="myPath"></param>
    private void CreateFiles(string myPath)
    {
        //Get the actual time
        DateTime date = DateTime.Now;

        fileExtension = ".bnav";
        path = myPath;

        //Init the name of the file with a formated date
        fileName = "BN-V02-" + date.ToString("yyyy-dd-MM-HH-mm-ss");

        //Create the file
        fullFileName = fileName + fileExtension;
        FileStream myFileStream;
        myFileStream = File.Create(path + fullFileName);
        myFileStream.Close();
    }

     /// <summary>
        /// lock or unlock the bnav file 
        /// The lock work with a copy of the original file with '.lock' at the end. - ex 'BN-V02-2016-01-02-14-20-50.bnav.lock'
        /// To unlock the original file the '.lock' file is deleted
        /// </summary>
        /// <param name="?"></param>
        private void ChangeFileStatus(bool _lock)
        {
            //ChangeFileStatus(true); -> verrouille le fichier
            //ChangeFileStatus(false); -> deverrouille le fichier
            if (fileName != null)
            {
                string fullFileNameLocked;
                fullFileNameLocked = fullFileName + ".lock";

                // Lock the file
                if (_lock)
                {
                    // Create the file and close it immediatly to avoid processus acces error
                    FileStream myFileStream;
                    myFileStream = File.Create(path + fullFileNameLocked); //The lock file is created 'BN-.......bnav.lock'
                    myFileStream.Close();
                }

                //Unlock the file
                if (!_lock)
                {
                    File.Delete(path + fullFileNameLocked);
                }
            }
        }

        /// <summary>
        /// Check if the file is locked
        /// </summary>
        /// <returns>Return true if '.lock' file exist and false if '.lock' file don't exist</returns> 
        private bool fileIsLocked()
        {
            bool result = false;
            if (File.Exists(fullFileName + ".lock"))
            {
                result = true;
            }
            return result;
        }

        public void writePseudo(string player, string pseudo)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.Filename);
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("JOUEURS/" + player);
            node.InnerText = pseudo;
            xmlDoc.Save(this.Filename);
        }

        //read a board and return it
        public int[,] readBoard(string boardType, string player)
        {
            int[,] board = new int[10, 10];
            int i=0, j=0;
            string regexpLine = @"(\d)";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.Filename);
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode(boardType + player);

            Match m = Regex.Match(node.InnerText, regexpLine);

            while (m.Success)
            {
                board[i, j] = System.Convert.ToInt16(m.Value);
                if (j < 9)
                {
                    j++;
                }
                else
                {
                    j = 0;
                    i++;
                }
                m = m.NextMatch();

            }

            return board;
        }

        //write a board in the file
        public void writeBoard(string boardType, string player, int x, int y, int value)
        {
            int[,] board = new int[10, 10];
            int i = 0, j = 0;

            board = readBoard(boardType, player);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.Filename);
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode(boardType + player);

            //write value
            board[x, y] = value;

            //write the array in the file
            node.InnerText = "" + Environment.NewLine;
            for (i = 0; i < 9; ++i)
            {
                for (j = 0; j < 9; ++j)
                {
                    node.InnerText += board[i, j].ToString() + ";";
                }
                node.InnerText += Environment.NewLine;
            }

            xmlDoc.Save(this.Filename);
        }

        public string[] ReadPlayers()
        {
            string[] Players = new string[2];             
            string player;
            XmlNode node;
            node = doc.DocumentElement.SelectSingleNode("/NAV/JOUEURS/P1");
            player = node.InnerText;
            Players[0] = player;
            node = doc.DocumentElement.SelectSingleNode("/NAV/JOUEURS/P2");
            player = node.InnerText;
            Players[1] = player;
            return Players;
        }

        public void WriteBattleBoard(int x, int y, int value)
        {
            writeBoard("PLATEAU-", "P1", x, y, value);
        }



}
