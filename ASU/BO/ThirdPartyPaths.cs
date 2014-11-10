using System;
using System.Collections.Generic;
using System.Text;

namespace ASU.BO
{
    class ThirdPartyPaths
    {
        public static string GetThirdPartyConversionToolFullPath()
        {
            string path = String.Empty;

            path = System.Configuration.ConfigurationManager.AppSettings["ThirdPartyImageConverter"];
            if (path.StartsWith("\\"))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + path;
            }

            return path;
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
