using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ProLock.Service
{
    internal class CommunicationClass_V2019 : ICommunicationClass
    {
        [DllImport("lib/proRFL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 GetDLLVersion([In, Out] char[] sDllVer);
        private string _version = "";
        public int ProGetDLLVersion()
        {
            int result = 0;
            var sDllVer = new char[200];
            string versionDLL = "";
            int fUSB = 1;
            result = GetDLLVersion(sDllVer);

            if (result == 0)
            {
                versionDLL = CharToString(sDllVer);
                //MessageBox.Show(versionDLL);
                _version = versionDLL;
            }

            if (versionDLL.ToUpper().IndexOf("PROUSB") >= 0) fUSB = 1;
            else fUSB = 0;

            return fUSB;
        }

        
        public bool checkVersionIsCorrect(string version)
        {
            if (_version == version)
            {
                return true;
            }
            return false;
        }
        public void showVersion()
        {
            MessageBox.Show(_version);
        }
        public bool WriteCard()
        {
            MessageBox.Show("Write card success");
            return true;
        }
        public static string CharToString(char[] arr)
        {
            string str = new string(arr);
            int pos = str.IndexOf('\0');
            if (pos >= 0)
                str = str.Substring(0, pos);
            return str;
        }
    }
}
