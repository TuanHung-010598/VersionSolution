using ProLock.Properties;
using ServiceStack.ServiceInterface;
using ServiceStack.WebHost.Endpoints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using Newtonsoft.Json;
using ProLock.Service;

namespace ProLock
{
    public partial class frmMain : Form
    {
        public static bool isValid = false;
        bool newlySelected = false;
        public static bool reloadRoomList = false;
        public static int COMPort;
        public static Client client;

        string listeningOn = "http://*:2000/";

        public frmMain()
        {
            InitializeComponent();
            client = new Client();
        }

        void SelectInstallationFolder()
        {
            try
            {
                txtLocation.Text = Settings.Default.LockFolder;

                if (!txtLocation.Text.EndsWith("\\"))
                    txtLocation.Text += "\\";

                var result = ConnectHelper.Connect(txtLocation.Text);

                if (result)
                {
                    button2.ForeColor = Color.Blue;
                    this.lblStatus1.Text = "Chọn thư mục thành công";
                    lblStatus1.ForeColor = Color.Blue;
                    button2.Enabled = false;
                    Settings.Default.LockFolder = txtLocation.Text;
                    Settings.Default.Save();
                }
                else
                {
                    button2.ForeColor = Color.Red;
                    this.lblStatus1.Text = "Thư mục khóa chưa đúng. Mời bạn chọn";
                    lblStatus1.ForeColor = Color.Red;
                    button2.Enabled = true;
                    MessageBox.Show("Thư mục khóa chưa đúng");
                    return;
                }

                result = client.ConnectSocket();

                if (result)
                {
                    button2.ForeColor = Color.Blue;
                    this.lblStatus2.Text = "Kết nối thành công";
                    lblStatus2.ForeColor = Color.Blue;
                }
                else
                {
                    this.lblStatus2.Text = "Vui lòng bật và đăng nhập phần mềm khóa JY01";
                    lblStatus2.ForeColor = Color.Red;
                    MessageBox.Show("Vui lòng bật và đăng nhập phần mềm khóa JY01");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thư mục khóa chưa đúng. Mời bạn chọn");
                button1.ForeColor = Color.Red;
                Log.Write("err: " + ex.ToString()); ;
                return;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                var appHost = new AppHost();
                appHost.Init();
                appHost.Start(listeningOn);
            }
            catch
            {
                MessageBox.Show("Phần mềm đã được bật, vui lòng kiểm tra lại");
                Application.Exit();
            }

            SelectInstallationFolder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var folder = folderBrowserDialog1.ShowDialog();
            if (folder == DialogResult.OK)
            {
                newlySelected = true;
                Settings.Default.SystemCode = "";
                Settings.Default.LockFolder = folderBrowserDialog1.SelectedPath;
                Settings.Default.Save();
                SelectInstallationFolder();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            foreach (Type t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ICommunicationClass).IsAssignableFrom(type)))
            {
                if (t.Name != typeof(ICommunicationClass).Name)
                {
                    comboBox1.Items.Add(t.Name);
                }
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (Type t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ICommunicationClass).IsAssignableFrom(type)))
            {
                if (t.Name == comboBox1.Text)
                {
                    var s = Activator.CreateInstance(t) as ICommunicationClass;
                    s.ProGetDLLVersion();
                    s.showVersion();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Type t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(ICommunicationClass).IsAssignableFrom(type)))
            {
                
                // lay class da luu torng file config
                //if(t.Name == ConfigurationManager.AppSettings["version"])
                //{

                //}
                var s = Activator.CreateInstance(t) as ICommunicationClass;
                s.ProGetDLLVersion();
                if (s.checkVersionIsCorrect("ProUsb-20190530"))
                {
                    MessageBox.Show($"Correct class is : {t.Name}");

                    // ghi vao file config
                    ConfigurationManager.AppSettings["version"] = t.Name;
                    return;
                }
            }

            MessageBox.Show($"No class is correct");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CommunicationWrapper.WriteCard();
        }
    }

    public class CardInfo2
    {
        public string RoomName { get; set; }
        public string TravellerName { get; set; }
        public string TravellerId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool OverrideOldCards { get; set; }
    }

    public class CardInfoResponse2
    {
        public int Result { get; set; } //0:success;1:fail
        public string RoomName { get; set; }
        public string TravellerName { get; set; }
        public string TravellerId { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
        public int cardNumber { get; set; }

        public string ErrorMesage { get; set; }
    }

    public class CardInfoService2 : ServiceBase<CardInfo2>
    {
        public CardInfoService2()
        {
            if (frmMain.client == null)
            {
                frmMain.client = new Client();
                frmMain.client.ConnectSocket();
            }
        }

        public string MapRoomName(string roomName)
        {
            var text = File.ReadAllText("room.json");
            var mydictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
            var roomAddress = "";
            mydictionary.TryGetValue(roomName, out roomAddress);
            if (roomAddress != "")
            {
                return roomAddress;
            }
            else
            {
                MessageBox.Show("Room address is null!");
            }
            return roomName;
        }

        public string GetLockNo(string roomName)
        {
            return roomName;
        }

        protected override object Run(CardInfo2 request)
        {
            var response = new CardInfoResponse2 { Result = 1 }; //0: OK
            var isMapRoomName = bool.Parse(ConfigurationSettings.AppSettings["isMapRoomName"]);
            var roomName = "";
            try
            {
                switch (this.Request.PathInfo)
                {
                    case "/readcard"://read card info   
                        Log.Write("_____ReadCard_____");
                        roomName = frmMain.client.ReadCard();

                        if (roomName != "")
                        {
                            response.Result = 0;
                            response.TravellerId = Log.GetReservationRoomId(roomName);
                        }

                        break;
                    case "/writecard":
                        Log.Write("_____WriteCard_____");
                        int travellerId = int.Parse(request.TravellerId);
                        roomName = request.RoomName.Trim();
                        string lockNo = "";

                        if (!isMapRoomName)
                        {
                            //roomName = ConnectHelper.GetLockNo(roomName);
                        }

                        lockNo = ConnectHelper.GetLockNo(roomName, Settings.Default.LockFolder);

                        if (lockNo == "")
                        {
                            response.Result = 1;
                            response.ErrorMesage = "Lock lock no is null";
                            return response;
                        }

                        var result = frmMain.client.CreateCard(lockNo, request.DepartureDate);
                        response.Result = result ? 0 : 1;

                        if (response.Result == 0)
                        {
                            Log.SetReservationRoomId(roomName, request.TravellerId);
                        }

                        break;
                    case "/deletecard":
                        Log.Write("_____DeteleCard_____");
                        var dresult = frmMain.client.DeleteCard();
                        response.Result = dresult ? 0 : 1;

                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                response.Result = 1;
                response.ErrorMesage = ex.ToString();
                Log.Write("api err: " + ex.ToString());
            }

            return response;
        }
    }

    //Define the Web Services AppHost
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost()
        : base("HttpListener Self-Host Orbita", typeof(CardInfoService2).Assembly)
        { }

        public override void Configure(Funq.Container container)
        {
            base.SetConfig(new EndpointHostConfig
            {
                GlobalResponseHeaders = {
                    { "Access-Control-Allow-Origin", "*" },
                    { "Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS" },
                    { "Access-Control-Allow-Headers", "Content-Type, Access-Control-Allow-Headers, Authorization, X-Requested-With" },
                },
            });

            Routes
            .Add<CardInfo2>("/readcard")
            .Add<CardInfo2>("/deletecard")
            .Add<CardInfo2>("/writecard");
        }
    }
}
