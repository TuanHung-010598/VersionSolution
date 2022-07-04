using ProLock.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;



namespace ProLock
{

    class CommunicationClass
    {
        public static string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Program Files (x86)\\ORBITA\\LockingSystem5.0\\db\\database.mdb;Jet OLEDB:Database Password =orbita91.wxy-;";
        
        public static char[] CharFromString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return new char[1];
            return ASCIIEncoding.ASCII.GetChars(ASCIIEncoding.ASCII.GetBytes(str));
        }
        public static string CharToString(char[] arr)
        {
            string str = new string(arr);
            int pos = str.IndexOf('\0');
            if (pos >= 0)
                str = str.Substring(0, pos);
            return str;
        }


        [DllImport("lib/proRFL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 GetDLLVersion([In, Out] char[] sDllVer);

        [DllImport("lib/proRFL.dll",CharSet = CharSet.Ansi,CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 initializeUSB([In, Out] int fUSB);

        [DllImport("proRFL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 Buzzer([In, Out] int fUSB, [In, Out] int t);

        [DllImport("proRFL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 ReadCard([In, Out] int fUSB, [In, Out] char[] Buffer);

        [DllImport("proRFL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 GetGuestLockNoByCardDataStr( int dlsCoID, [In, Out] char[] CardDataStr, [In, Out] char[] LockNo);

       
        [DllImport("proRFL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 GuestCard(int fUSB,
           Int32 dlsCoID, int CardNo, int dai, int llock,
            int pdoors, string BTime, string ETime, string LockNo, [In, Out] char[] CardHexStr
           );

        [DllImport("proRFL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 CardErase([In, Out] int fUSB, int dlsCoID , [In, Out] char[] CardHexStr);

        [DllImport("proRFL.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private static extern Int16 GetGuestETimeByCardDataStr(int dlsCoID, [In, Out] char[] CardDataStr, [In, Out] char[] LockNo);
        

        public static CardInfo CreateCard(string _room, DateTime _indate, DateTime _outdate)
        {
           

            var result = 1;

            // Deletete Card before Create Card
            var resultdel=deleteCard();


            Random rnd = new Random();
            int fUSB = ProGetDLLVersion();
            Int32 dlsCoID = getHotelId();
            int cardNo = 1;//  rnd.Next(0, 15);
            int dai = 0;// rnd.Next(0, 255);
            string BTime =  DateTime.Now.ToString("yyMMddHHmm");
            string ETime = _outdate.ToString("yyMMddHHmm");
            string LockNo = _room;
            int llock = 1;
            int pdoors = 0;
            var cardHexStr = new char[128];
     

            if (dlsCoID==0)
            {
                var retval1 = new CardInfo { result = 1 };
                return retval1;
            }
            result = GuestCard(fUSB, dlsCoID, cardNo, dai, llock, pdoors, BTime, ETime, LockNo, cardHexStr);


            int r=ReadCard(ProGetDLLVersion(), cardHexStr);


            string scardInformation = CharToString(cardHexStr);
            string cardNumber = ""; ;
            if (scardInformation.Length > 30)
            {
                if (scardInformation.Substring(24, 8) == "FFFFFFFF")
                {
                    cardNumber = ""; 
                    return new CardInfo { result = 1 };
                }
                else
                {
                    cardNumber = scardInformation.Substring(24, 8);
                }
            }
            var retval = new CardInfo { result = result };
            if (result == 0)
            {
                retval.arrivalDate = _indate;
                retval.departureDate = _outdate;
                retval.cardNo = cardNumber;
                retval.room = cardNumber;
                retval.result = result;
            }
            else
            {
                retval.result = 1;
            }
            return retval;

        }
        public static string getCheckoutTime()
        {
            int dlsCoID = getHotelId();
            string CheckoutTime = "";
            var LockNoChar = new char[128];
          
            var cardInformation = new char[128];
           
            int result = 0;
            result = ReadCard(ProGetDLLVersion(), cardInformation);
            if (result == 0)
            {
                

                result = GetGuestETimeByCardDataStr(dlsCoID, cardInformation, LockNoChar);
                if (result == 0)
                {
                    CheckoutTime = CharToString(LockNoChar);
                }
                   
                
            }

            return CheckoutTime;
        }
        public static string getLockNo()
        {
            int dlsCoID = getHotelId();
            string LockNo = "";
            var LockNoChar = new char[128];
           
            var cardInformation = new char[128];
           
            int result = 0;
            result = ReadCard(ProGetDLLVersion(), cardInformation);
            if (result == 0)
            {

                //CardInfoService2.WriteLog("function ReadCard cardInformation:" + cardInformation);

                //cardInformation = CharFromString(cardNo);
                result = GetGuestLockNoByCardDataStr(dlsCoID, cardInformation, LockNoChar);
                if (result==0)
                {
                           
                    LockNo = CharToString(LockNoChar);
                }
                    
                
            }

            return LockNo;
        }
        public static CardInfo ReadCard()
        {


            string cardNo = "";
           
            var cardInformation = new char[128];
            string scardInformation = "";
            int result = 0;
            result = ReadCard(ProGetDLLVersion(), cardInformation);
            if (result == 0)
            {
                scardInformation = CharToString(cardInformation);
                if (scardInformation.Length > 30)
                {
                    if (scardInformation.Substring(24, 8) == "FFFFFFFF")
                    {
                        cardNo = "";
                        result = 1;
                    }
                    else
                    {
                        cardNo = scardInformation.Substring(24, 8);
                    }
                }
            }
            
            
            var retval = new CardInfo { result = result };
            if (result == 0)
            {  
                retval.arrivalDate = DateTime.Now;
                retval.departureDate = DateTime.Now;
                retval.cardNo = getCheckoutTime();
                retval.room = getLockNo();
            }
            return retval;
        }
        public static int deleteCard()
        {
            int result = 0;
            var cardInformation = new char[200];
            result = ReadCard(ProGetDLLVersion(), cardInformation);
            result = CardErase(ProGetDLLVersion(), getHotelId(), cardInformation);
            return result;
        }
        public static int ProInitializeUSB(int fUSB)
        {
            int result = 0;
            result = initializeUSB(fUSB);
            return result;
        }
        public static int Buzzer(int fUSB)
        {
            int result = 0;
            int t = 0;
            result = Buzzer(fUSB,t);
            return result;
        }

        public static int ProGetDLLVersion()
        {
            int result = 0;
            var sDllVer = new char[200];
            string versionDLL ="";
            int fUSB = 1;
            result = GetDLLVersion(sDllVer);
            
            if (result == 0)
            {
                versionDLL = CharToString(sDllVer);
            }

            if (versionDLL.ToUpper().IndexOf("PROUSB") >= 0) fUSB = 1;
            else fUSB = 0;

            return fUSB;
        }
  
        public static Int32 getHotelId()
        {
            
            Int32 hotelId = 0;
            hotelId = Int32.Parse(Settings.Default.HotelCode);
            return hotelId;
        }

        public static Int32 InnitHotelId()
        {
            Int32 hotelId = 0;
            var cardInformation = new char[128];
            string scardInformation = "";
            int result = 0;
            result = ReadCard(ProGetDLLVersion(), cardInformation);
            if (result == 0)
            {
                scardInformation = CharToString(cardInformation);
                //scardInformation = "551501C97C00E5E38382010061812AC48133800101643E94FF30313231303139FFFFFFD7";
                if (scardInformation.Length > 30)
                {
                    if (scardInformation.Substring(24, 8) == "FFFFFFFF")
                    {
                        hotelId = 0;
                    }
                    else
                    {
                        var s = scardInformation.Substring(10, 4);
                        var i = Convert.ToInt32(s, 16) % 16384;
                        s = scardInformation.Substring(8, 2);
                        i = i + (Convert.ToInt32(s, 16) * 65536);
                        hotelId = i;
                    }
                }

            }
            else
            {
                hotelId = 0;
            }
            return hotelId;
        }

    }
}
