using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CSharpDemo
{
    public class Language
    { 
        static string g_szLanguagePath = "";
        static string g_szCurPath = "";

        /// <summary>
        /// ȡ��������Դ�ļ���·��
        /// </summary>
        public static void GetLanguagePath_Ex()
        {
            g_szCurPath = Application.StartupPath; 
            //Ini ini = new Ini(g_szCurPath + "\\Config.ini");
            //string sLan = ini.ReadValue("System", "Language");
            //if (sLan == "")
            //{
            //    sLan = "Chinese";
            //}
            g_szLanguagePath = g_szCurPath + "\\ToolsLanguage.ini";
        }

        /// <summary>
        /// ���ݱ�ʶszID��ѡ���������ļ��м����ַ���
        /// </summary>
        /// <param name="szID"></param>
        /// <returns></returns>
        public static string g_LoadString_Ex(string szID)
        {
            string szValue = "";
            if (g_szLanguagePath == "")
                GetLanguagePath_Ex();
            Ini ini = new Ini(g_szLanguagePath);
            szValue = ini.ReadValue("String", szID);
            if (szValue == "")
            {
                szValue = "Not found";
            }
            else
            {
                szValue.Replace("\\n", "\n");//�滻�ػ��з���
            }
            return szValue;
        }

        /// <summary>
        /// ���Ի�������ʱ��ȡ�����пɵõ����ַ����������浽�����ļ���
        ///	ÿ���ؼ��öԻ���ID�Ϳؼ�IDΨһ��ʶ
        /// </summary>
        /// <param name="frm"></param>
        public static void g_SetFormStrings_Ex(Form frm)
        {
            string szSection = "LockSDKDemo";
            string szKey = "", szText = "";
            bool bSetText = true;//true,���ļ��ж������ô��ڣ�false:�ӶԻ�������浽�ļ�

            if (g_szLanguagePath == "")
                GetLanguagePath_Ex();
            Ini ini = new Ini(g_szLanguagePath);
            if (bSetText)//���ļ��ж������öԻ���
            {
                string szDefault = "";

                //�����ڱ���
                szKey = frm.Name + "_Title";

                szDefault = ini.ReadValue(szSection, szKey);
                if (szDefault == "")
                {
                    szDefault = "Not found";
                }
                else
                {
                    szDefault.Replace("\\n", "\n");//�滻�ػ��з���
                }
                frm.Text = szDefault;

                //д������ֿؼ�����
                foreach (Control c1 in frm.Controls)
                {
                    szKey = c1.Name;// frm.Name + "_" + c1.Name;
                    szText = ini.ReadValue(szSection, szKey);
                    c1.Text = szText;
                }
            }
            else//�Ӵ��ڱ��浽�ļ�
            {
                //д�봰�ڱ���
                szKey = frm.Name + "_Title";
                szText = frm.Text;
                ini.Writue(szSection, szKey, szText);

                //д������ӿؼ��ı�������
                foreach (Control c2 in frm.Controls)
                {
                    szKey = frm.Name + "_" + c2.Name;
                    szText = c2.Text;
                    ini.Writue(szSection, szKey, szText);
                }
            }
        }
    }
    public class Ini
    {
        // ����INI�ļ���д�������� WritePrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // ����INI�ļ��Ķ��������� GetPrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);


        private string sPath = null;
        public Ini(string path)
        {
            this.sPath = path;
        }

        public void Writue(string section, string key, string value)
        {

            // section=���ýڣ�key=������value=��ֵ��path=·��

            WritePrivateProfileString(section, key, value, sPath);

        }
        public string ReadValue(string section, string key)
        {

            // ÿ�δ�ini�ж�ȡ�����ֽ�

            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

            // section=���ýڣ�key=������temp=���棬path=·��

            GetPrivateProfileString(section, key, "", temp, 255, sPath);

            return temp.ToString();

        }
    }
}
