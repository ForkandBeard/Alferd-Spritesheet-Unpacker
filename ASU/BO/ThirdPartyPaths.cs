using System;
using System.Collections.Generic;
using System.Text;

namespace ASU.BO
{
    class ThirdPartyPaths
    {
        public static string GetThirdPartyConversionToolFullPath()
        {
            string strReturn = null;

            strReturn = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverter"];
            if (strReturn.StartsWith("\\"))
            {
                strReturn = AppDomain.CurrentDomain.BaseDirectory + strReturn;
            }

            return strReturn;
        }

        public static string GetThirdPartyConversionToolExecutableName()
        {
            return System.IO.Path.GetFileName(GetThirdPartyConversionToolFullPath());
        }

        public static string GetThirdPartyConversionToolDirectory()
        {
            return System.IO.Path.GetDirectoryName(GetThirdPartyConversionToolFullPath());
        }
    }
}
