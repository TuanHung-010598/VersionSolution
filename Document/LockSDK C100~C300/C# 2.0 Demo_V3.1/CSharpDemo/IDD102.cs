using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace CSharpDemo
{
    public partial class IDD102 : Form
    {
        public IDD102()
        {
            InitializeComponent();
        }
        int st = 0;

        #region ���ö�̬�⺯��
        [DllImport("LockSDK.dll", EntryPoint = "TP_Configuration")]
        public static extern int TP_Configuration(Int16 LockType);

        [DllImport("LockSDK.dll", EntryPoint = "TP_MakeGuestCard")]
        public static extern int TP_MakeGuestCard(StringBuilder card_snr,string room_no,string checkin_time,string checkout_time,Int16 iflags);

        [DllImport("LockSDK.dll", EntryPoint = "TP_ReadGuestCard")]
        public static extern int TP_ReadGuestCard([In, Out] char[] card_snr,[In, Out] char[] room_no,[In, Out] char[] checkin_time,[In, Out] char[] checkout_time);

        [DllImport("LockSDK.dll", EntryPoint = "TP_CancelCard")]
        public static extern int TP_CancelCard(StringBuilder card_snr);

        [DllImport("LockSDK.dll", EntryPoint = "TP_GetCardSnr")]
        public static extern int TP_GetCardSnr(StringBuilder card_snr);
        #endregion

        private void LockSDK_Demo_Load(object sender, EventArgs e)
        {
            
            Language.g_SetFormStrings_Ex(this);
            this.txtRoomNo.Text = "001.002.00028";
            this.txtInTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtOutTime.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " 12:00:00";
        }

        #region ��������
        private void CheckErr(int iret)
        {
            switch(iret)
            {
                case 1:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_SUCCESS"), Language.g_LoadString_Ex("IDS_STRING_MSG"),MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -1:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR_NOCARD"), Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -2:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR_NOREADE"), Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -3:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR_INVALIDCARD"), Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -4:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR_CARDTYPE"), Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -5:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR_READCARD"), Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break; 
                case -8:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR_INPUT"), Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case -29:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR_REG"), Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR"), Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }
        #endregion

        #region ��ť 

        private void IDD102_1011_Click(object sender, EventArgs e)
        {
            this.txtInTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        } 
        private void IDD102_1002_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("OK");
            //����SDK
            Int16 locktype = 0;
            if(IDD102_1001.Checked)
            {
                locktype = 5;
            }
            if(IDD102_1000.Checked)
            {
                locktype = 4;
            }

            st = TP_Configuration(locktype);
            //MessageBox.Show("St:" + st.ToString());
            if (st == 1)
            {
                this.IDD102_1011.Enabled = true;
                this.IDD102_1003.Enabled = true;
                this.IDD102_1005.Enabled = true;
                this.IDD102_1009.Enabled = true;
            }
            CheckErr(st);
        }

        private void IDD102_1003_Click(object sender, EventArgs e)
        {
            //�ƿ�
            StringBuilder card_snr = new StringBuilder();
            string roomno=txtRoomNo.Text;
            string intime=txtInTime.Text;
            String outtime=txtOutTime.Text;

            st = TP_MakeGuestCard(card_snr, roomno, intime, outtime, 0);
            //if (iret == 1)
            //    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_SUCCESS"));
            //else
            //    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR") + Language.g_LoadString_Ex("IDS_STRING_ERROR_CODE") + iret.ToString());
            CheckErr(st);

        }

        private void IDD102_1005_Click(object sender, EventArgs e)
        {
            //����
            StringBuilder card_snr = new StringBuilder();
            st = TP_CancelCard(card_snr);
            //if (iret == 1)
            //    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_SUCCESS"));
            //else
            //    MessageBox.Show(Language.g_LoadString_Ex("IDS_STRING_ERROR") + Language.g_LoadString_Ex("IDS_STRING_ERROR_CODE") + iret.ToString());
            CheckErr(st);
        }

        private void IDD102_1009_Click(object sender, EventArgs e)
        {
            //����
            char[] card_snr = new char[20];
            char[] roomno = new char[20];
            char[] intime = new char[20];
            char[] outtime = new char[20];
            string strMsg = "";
            st = TP_ReadGuestCard(card_snr, roomno, intime, outtime);
            if (st == 1)
            {
                strMsg = Language.g_LoadString_Ex("IDS_STRING_CARDNO") + new string( card_snr) + "\n";
                 strMsg += Language.g_LoadString_Ex("IDS_STRING_LOCKNO") + new string(roomno) + "\n";
                 strMsg += Language.g_LoadString_Ex("IDS_STRING_INTIME") + new string(intime) + "\n";
                 strMsg += Language.g_LoadString_Ex("IDS_STRING_OUTTIME") + new string(outtime);
                 MessageBox.Show(strMsg, Language.g_LoadString_Ex("IDS_STRING_MSG"), MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                CheckErr(st);
                //strMsg = Language.g_LoadString_Ex("IDS_STRING_ERROR") + Language.g_LoadString_Ex("IDS_STRING_ERROR_CODE") + iret.ToString();
            }
           
        }
        #endregion 
    }
}