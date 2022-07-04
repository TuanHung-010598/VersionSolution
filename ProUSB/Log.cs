using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ProLock
{
    public class Log
    {
        public static void Write(string strLog)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = "";
            logFilePath = logFilePath + "Log-" + System.DateTime.Today.ToString("yyyy-MM-dd") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);

            if (!logDirInfo.Exists)
            {
                logDirInfo.Create();
            }

            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }

            log = new StreamWriter(fileStream);
            strLog += "\r\n";
            log.WriteLine(strLog);
            log.Close();
        }

        public static string GetReservationRoomId(string roomName)
        {
            Write("_____GetReservationRoomId_____");
            roomName.TrimStart('0').Trim();
            string reservaionRoomId = "0";
            string[] ReservationRoomLines = System.IO.File.ReadAllLines(ConfigurationManager.AppSettings["reservationPath"]);

            foreach (string line in ReservationRoomLines)
            {
                string[] words = line.Split('_');
                if (words.Length == 2)
                {
                    if (roomName == words[0].Trim())
                    {
                        reservaionRoomId = words[1].Trim();
                        break;
                    }
                }
            }

            return reservaionRoomId;
        }

        public static void SetReservationRoomId(string roomName, string reservationRoomId)
        {
            Write("_____SetReservationRoomId_____");
            string logFilePath = ConfigurationManager.AppSettings["reservationPath"];
            FileInfo logFileInfo;
            logFileInfo = new FileInfo(logFilePath);

            if (!logFileInfo.Exists)
            {
                logFileInfo.Create();
            }

            string[] ReservationRoomLines = System.IO.File.ReadAllLines(logFilePath);
            string ReservationRoomValue = "";
            foreach (string line in ReservationRoomLines)
            {
                if (line == "")
                {
                    continue;
                }

                string[] words = line.Split('_');
                if (words.Length == 2)
                {
                    if (words[0] != roomName)
                    {
                        ReservationRoomValue += "\r\n" + line;
                    }

                    if (words[0] == roomName && words[1] == reservationRoomId)
                    {
                        return;
                    }
                }
            }

            ReservationRoomValue += "\r\n" + roomName + "_" + reservationRoomId;
            System.IO.File.WriteAllText(logFilePath, ReservationRoomValue);
        }
    }
}
