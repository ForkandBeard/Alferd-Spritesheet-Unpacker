using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Logic.BAL
{
    public class Paths
    {
        private static string forkandBeardFolder = null;

        private static string GetUserDataPath()
        {           
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static string GetUserForkandBeardDataPath()
        {
            if (forkandBeardFolder == null)
            {
                forkandBeardFolder = System.IO.Path.Combine(GetUserDataPath(), "ForkandBeard");
            }
            System.IO.Directory.CreateDirectory(forkandBeardFolder);
            return forkandBeardFolder;
        }

        public static string GetUserForkandBeardDataSubFolderPath(Enums.Directory directory)
        {
            return GetUserForkandBeardDataSubFolderPath(directory.ToString());
        }

        public static string GetUserForkandBeardDataSubFolderPath(string subDirectoryPath)
        {
            string path;

            path = System.IO.Path.Combine(GetUserForkandBeardDataPath(), subDirectoryPath);
            System.IO.Directory.CreateDirectory(path);

            return path;
        }
    }
}
