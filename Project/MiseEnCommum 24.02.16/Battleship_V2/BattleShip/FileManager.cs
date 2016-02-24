using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace BattleShip
{
    public class FileManager
    {
        #region fields
        private string fileExtension, path, fileName, fullFileName;
        private string currentplayer;
        private XmlDocument doc;
        #endregion

        #region Properties
        public string Currentplayer
        {
            get { return currentplayer; }
            set { currentplayer = value; }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for creating new game
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pseudo"></param>
        public FileManager(string path, string pseudo)
        {
            CreateFiles(path);
            writePseudo("P1", pseudo);
        }

        /// <summary>
        /// Constructor for joining a game
        /// </summary>
        /// <param name="path"></param>
        /// <param name="pseudo"></param>
        /// <param name="filename"></param>
        public FileManager(string path, string pseudo, string filename)
        {
            JoinGame(path, filename, pseudo);
        }
        #endregion

        #region methods
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
            /* FileStream myFileStream;
             //myFileStream = File.Create(path + fullFileName);      
             myFileStream.Close();*/            

            System.IO.File.Copy(@"./DEFAULT.bnav", myPath+"\\" + fullFileName);
            fullFileName = myPath+"\\"+fullFileName;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fullFileName);

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
            if (File.Exists(path + fullFileName + ".lock"))
            {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Write the pseudo of the player in the file
        /// </summary>
        /// <param name="player">P1 or P2</param>
        /// <param name="pseudo"></param>
        private void writePseudo(string player, string pseudo)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.fullFileName);
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("JOUEURS/" + player);
            node.InnerText = pseudo;
            xmlDoc.Save(this.fullFileName);
        }

        //read a board and return it
        public int[,] readBoard(string boardType, string player)
        {
            int[,] board = new int[10, 10];
            int i = 0, j = 0;
            string regexpLine = @"(\d)";

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(this.fullFileName);
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
            xmlDoc.Load(this.fullFileName);
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

            xmlDoc.Save(this.fullFileName);
        }

        /// <summary>
        /// Read the both players
        /// </summary>
        /// <returns>the pseudo of the both players in a array of strings</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void WriteBattleBoard(int x, int y, int value)
        {
            writeBoard("PLATEAU-", Currentplayer, x, y, value);
        }


        public void SwitchCurrentPlayer()
        {
            this.currentplayer = ReadCurrentPlayer();
            XmlNode nodeCurrentPlayer = doc.DocumentElement.SelectSingleNode("/NAV/CURRENT_PLAYER/PLAYER");
            switch (currentplayer)
            {
                case "P1": this.currentplayer = "P2"; break;
                case "P2": this.currentplayer = "P1"; break;

            }
            nodeCurrentPlayer.InnerText = this.currentplayer;

            // Ecrit la variable CurrentPlayer dans le fichier
            doc.Save(fullFileName);

        }

        public string ReadCurrentPlayer()
        {
            // Selectionne le current player
            XmlNode nodejoueur1 = doc.DocumentElement.SelectSingleNode("/NAV/CURRENT_PLAYER/PLAYER");
            this.currentplayer = nodejoueur1.LastChild.Value;
            return this.currentplayer;
        }

        /// <summary>
        /// Join a  curent game
        /// </summary>
        /// <param name="path">path where is the file</param>
        /// <param name="filename">filename with extension</param>
        /// <param name="pseudo">pseudo of joining player</param>
        /// <returns>true if the game is joined else return false</returns>
        private bool JoinGame(string path, string filename, string pseudo)
        {
            this.path = path;
            this.fullFileName = filename;
            bool returnVal;
            returnVal = true;
            if (fileIsLocked())
            {
                returnVal = false;
            }
            else
            {
                ChangeFileStatus(true);
                string[] players = ReadPlayers();
                if (players[1] != "")
                {
                    writePseudo("P2", pseudo);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(this.fullFileName);
                }
                else
                {
                    returnVal = false;
                }
                ChangeFileStatus(false);
            }


            return returnVal;
        }
        #endregion

    }
}
