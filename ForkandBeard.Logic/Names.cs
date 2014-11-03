using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace ForkandBeard.Logic
{
    public class Names
    {
        public static string GetApplicationName()
        {
            return Process.GetCurrentProcess().ProcessName;
        }

        public static string GetApplicationMajorVersion()
        {
            return FileVersionInfo.GetVersionInfo(Process.GetCurrentProcess().MainModule.FileName).ProductVersion;
        }

        public static string GetApplicationNameAndMajorVersion()
        {
            return String.Format("{0} ver.{1}", GetApplicationName(), GetApplicationMajorVersion());
        }
    }
}
