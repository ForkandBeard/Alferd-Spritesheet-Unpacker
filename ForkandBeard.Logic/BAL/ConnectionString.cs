using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Logic.BAL
{
    public class ConnectionString
    {
        public static BO.ConnectionString LoadConnectionString(Guid id)
        {
            return SaverAndLoader.Load<BO.ConnectionString>(Enums.Directory.Connection_Strings.ToString(), id);
        }

        public static List<BO.ConnectionString> LoadAllConnectionStrings()
        {
            return SaverAndLoader.LoadAll<BO.ConnectionString>(Enums.Directory.Connection_Strings.ToString());
        }

        public static void SaveConnectionString(BO.ConnectionString connectionString)
        {
            SaverAndLoader.Save(connectionString);                       
        }
    }
}
