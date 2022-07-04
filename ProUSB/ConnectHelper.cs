using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ProLock
{
    public class ConnectHelper
    {
        public string _connectString { get; set; }
        public string _locationPath { get; set; }

        public static bool Connect(string locationPath)
        {
            Log.Write("_____Connect_____");
            string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + locationPath + "CardLock.mdb;Jet OLEDB:Database Password=pradlock;";
            var query = "select * from roominfo";
            using (OleDbConnection connection = new OleDbConnection(connectString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Log.Write("err: " + ex.ToString()); ;
                    return false;
                }
            }

            return true;
        }

        public static string GetLockNo(string roomName, string locationPath)
        {
            Log.Write("_____GetLockNo_____");
            string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + locationPath + "CardLock.mdb;Jet OLEDB:Database Password=pradlock;";
            var lockNo = "";
            var query = "select bldno, flrno, romid from roominfo where roomno = '" + roomName.Trim().TrimStart('0') + "'";
            Log.Write("query: " + query);
            using (OleDbConnection connection = new OleDbConnection(connectString))
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    OleDbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var bld = reader[0].ToString().Trim();
                        var flrno = reader[1].ToString().Trim();
                        var roomid = reader[2].ToString().Trim();
                        lockNo = "0" + bld + "0" + flrno + (roomid.Length == 1 ? ("0" + roomid) : roomid);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Log.Write("err: " + ex.ToString()); ;
                    return "";
                }
            }

            Log.Write("lockNo: " + lockNo);
            return lockNo;
        }
    }
}
