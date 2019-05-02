using System.Diagnostics;
using System.Threading.Tasks;
using DmobileApp.Model;
using DmobileApp.Services;
using Microsoft.AspNet.SignalR.Client;
using Plugin.Badge;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DmobileApp
{
    public partial class App : Application
    {
        private m_profile resIdentify;
        private string _deviceId = "";
        private string _simSerial = "";
        private string _version = "1";
        private string _phone_number = "";
        private HubConnection connection;
        private IHubProxy notifyHub;
        private string p_connection;

        public App(string deviceId="", string simSerial="", string version = "1.0", string phone_number = "")
        {
            InitializeComponent();
            _deviceId = deviceId;
            _simSerial = simSerial;
            _version = version;
            _phone_number = phone_number;
            resIdentify = User.identify(deviceId, simSerial, version);
            if (resIdentify != null)
            {
                if (resIdentify.code == 200)
                {
                    MainPage = new NavigationPage(new Mainpage(resIdentify.data, deviceId, simSerial, version));
                        //connection = new HubConnection("http://33092ab1.ngrok.io/signalr");
                        //notifyHub = connection.CreateHubProxy("NotificationHub");
                        //Debug.WriteLine("pipe => In App contructor");
                        //if (Application.Current.Properties.ContainsKey("conn_ws"))
                        //{
                        //    p_connection = Application.Current.Properties["conn_ws"].ToString();
                        //    //Application.Current.Properties["conn_ws"] = "disconnected";
                        //    Debug.WriteLine($"pipe => found key [conn_ws] = {p_connection}");
                        //    Debug.WriteLine($"pipe => connection state {connection.State}");
                        //    if (connection.State == ConnectionState.Disconnected)
                        //        connection.Start();
                        //}
                        //else
                        //{
                        //    Debug.WriteLine("pipe => not found key[conn_ws]");
                        //    Debug.WriteLine($"pipe => connection state {connection.State}");
                        //    connection.Start();
                        //}

                        //connection.Closed += async () => await Connection_ClosedAsync();
                        //connection.Reconnected += async () => await Connection_ReconnectedAsync();
                        //connection.Reconnecting += Connection_Reconnecting;

                        //notifyHub.On("connect", async (string message) => await RegisterContext(message, deviceId));
                        //notifyHub.On("notify", (string message) => OnNotify(message));



                    //MainPage = new NavigationPage(new Mainpage(resIdentify.data, deviceId) { Icon =  });
                    //if (resIdentify.data.PERMIT == "SMS")
                    //    MainPage = new NavigationPage(new ChatSms(resIdentify.data));
                    //else if (resIdentify.data.PERMIT == "PAYMENT")
                    //    MainPage = new NavigationPage(new LoanPage(resIdentify.data));
                    //else
                    //MainPage = new NavigationPage(new Mainpage(resIdentify.data));
                }
                else if(resIdentify.code == 402)
                {
                    MainPage = new NavigationPage(new Register(deviceId, simSerial, version, phone_number));

                    //MainPage = new NavigationPage(new Register(deviceId, simSerial, version, phone_number));
                }
                else 
                {
                    MainPage = new NavigationPage(new Register(deviceId, simSerial, version, phone_number));
                }
                //else
                //MainPage = new NavigationPage(new Register(deviceId, simSerial, version));
            }
            else
            {
                MainPage = new NavigationPage(new Register(deviceId, simSerial, version, phone_number));
                //MainPage.DisplayAlert("ไม่สามารถเข้าสู่ระบบได้", "พบข้อผิดพลาดจากเซิฟเวอร์ กรุณาลองเข้าระบบใหม่ในภายหลัง!", "ตกลง");
                //DependencyService.Get<IMessage>().longAlert("พบข้อผิดพลาดจากเซิฟเวอร์ กรุณาลองเข้าระบบใหม่ในภายหลัง");
            }

        }

        public async Task ConnectWSAsync()
        {
            await connection.Start();
        }
        public async Task RegisterContext(string message, string deviceId) {
            Debug.WriteLine(message);
            Debug.WriteLine(message);
            Debug.WriteLine(message);
            Debug.WriteLine($"pipe => connection state {connection.State}");
            await notifyHub.Invoke("registerContext", deviceId);
            Application.Current.Properties["conn_ws"] = "connected";
            await Application.Current.SavePropertiesAsync();
        }
        public void OnNotify(string message)
        {
            Debug.WriteLine($"pipe => New notification is {message}");
            Debug.WriteLine($"pipe => connection state {connection.State}");
            CrossLocalNotifications.Current.Show("D-Mobile", "มีข้อความใหม่ที่ยังไม่ได้อ่าน");
            //CrossBadge.Current.SetBadge(3);
            //DependencyService.Get<IMessage>().longAlert(message);
        }

        public async Task Connection_ClosedAsync()
        {
            Debug.WriteLine("pipe => Connection closed......");
            Debug.WriteLine($"pipe => connection state {connection.State}");
            Application.Current.Properties["conn_ws"] = "disconnected";
            await Application.Current.SavePropertiesAsync();
        }

        public async Task Connection_ReconnectedAsync()
        {
            Debug.WriteLine("pipe => Connection reconnected.......");
            Debug.WriteLine($"pipe => connection state {connection.State}");
            Application.Current.Properties["conn_ws"] = "connected";
            await Application.Current.SavePropertiesAsync();
        }

        void Connection_Reconnecting()
        {
            Debug.WriteLine("pipe => Connection reconnecting.......");
            Debug.WriteLine($"pipe => connection state {connection.State}");
        }


        protected override void OnStart()
        {
            // Handle when your app starts
            Debug.WriteLine("pipe => application OnStart");
            //Debug.WriteLine($"pipe => connection state {connection.State}");
            if (Application.Current.Properties.ContainsKey("conn_ws"))
            {
                p_connection = Application.Current.Properties["conn_ws"].ToString();
                Debug.WriteLine($"pipe => found key [conn_ws] = {p_connection}");
            }
            if (resIdentify == null)
            {
                MainPage.DisplayAlert("ไม่สามารถเข้าสู่ระบบได้", "ไม่สามารถเชื่อมต่อบริการได้ กรุณาลองเข้าระบบใหม่ในภายหลัง!", "ตกลง");
            }
            else
            {
                if(resIdentify.code == 402)
                {
                    MainPage.DisplayAlert("ไม่สามารถเข้าสู่ระบบได้", resIdentify.message, "ตกลง");
                }
            }
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
            Debug.WriteLine("pipe => application OnSleep");
           //Debug.WriteLine($"pipe => connection state {connection.State}");
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            Debug.WriteLine("pipe => application OnResume");
            //Debug.WriteLine($"pipe => connection state {connection.State}");
            MessagingCenter.Send(this, "refreshSmsOnResume");
            MessagingCenter.Send(this, "refreshContract");
        }
    }
}
