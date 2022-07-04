using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Linq;

namespace ProLock
{
    public class Client
    {
        private const int BUFFER_SIZE = 1024;
        private const int PORT_NUMBER = 10086;
        public TcpClient client;
        public Stream stream;

        public ASCIIEncoding encoding = new ASCIIEncoding();

        public bool ConnectSocket()
        {
            try
            {
                client = new TcpClient();
                Log.Write("_____Connect to port 10086_____");
                client.Connect("127.0.0.1", PORT_NUMBER);
                stream = client.GetStream();
                string str = "LS";
                StreamWrite(str);
                //StreamRead();
            }
            catch (Exception ex)
            {
                Log.Write("ConnectSocket err: " + ex.ToString());
                return false;
            }

            return true;
        }

        public void CloseSocket()
        {
            try
            {
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }

        public bool CreateCard(string lockNo, DateTime endDate)
        {
            try
            {
                Log.Write("_____CreateCard_____");

                var data = "KR|RN" + lockNo + "|CT06|BN" + lockNo.Substring(0, 2) + "|CO" + endDate.ToString("yyMMddHHmm") + "|CN01|LS00";
                Log.Write("data: " + data);
                string result = "";
                result = StreamWrite(data);
                Log.Write("result: " + result);

                if (result.IndexOf("IssueCardOK") == -1)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Write("Socket err: " + ex.ToString());
                return false;
            }

            return true;
        }

        public string ReadCard()
        {
            string roomName = "";
            try
            {
                Log.Write("_____ReadCard_____");

                string result = "";
                result = StreamWrite("KI");

                if (result.IndexOf("ReadCardOK") == -1 || result.Length < 3)
                {
                    Log.Write("read fail: " + result);
                    return roomName;
                }

                var index = result.IndexOf("RN");
                roomName = result.Substring(index + 2, 3);
                Log.Write("roomName: " + roomName);
            }

            catch (Exception ex)
            {
                Log.Write("Socket err: " + ex.ToString());
                roomName = "";
            }

            return roomName;
        }

        public bool DeleteCard()
        {
            try
            {
                Log.Write("_____DeleteCard_____");
                string result = "";
                result = StreamWrite("KD");
                Log.Write("result: " + result);

                if (result.IndexOf("CardEraseOK") == -1)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Write("Socket err: " + ex.ToString());
                return false;
            }

            return true;
        }

        public string StreamWrite(string str)
        {
            var result = "";
            try
            {
                Log.Write("data read: " + str);
                byte[] data = encoding.GetBytes(str);
                stream.Write(data, 0, data.Length);
                Log.Write("data read: " + str);
                data = new byte[BUFFER_SIZE];
                stream.Read(data, 0, BUFFER_SIZE);
                result = encoding.GetString(data);
                result = result.Trim();
                Log.Write("data write: " + result);
            }
            catch (Exception ex)
            {
                Log.Write("StreamWrite err: " + ex.ToString());
                return result;
            }

            return result;
        }



    }
}
