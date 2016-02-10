/*
Cedric Dos Reis

Fonctionnalité :
	Creation du fichier 'BN-V2-2016-01-02-14-20-50.bnav'
	Verrouillage/deverrouillage du fichier avec l'ajout d'un fichier '.lock'
	vérification de verrouillage du fichier

*/
using System;
using System.IO;

namespace CreationFichier
{
    public class FileManager
    {
        string path;
        string fileName;
        string fileExtension;
        string fullFileName;
        #region
        #endregion


        
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
    }
}
