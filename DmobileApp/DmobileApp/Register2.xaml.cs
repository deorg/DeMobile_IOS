using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DmobileApp.Model;
using DmobileApp.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DmobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register2 : ContentPage
    {
        private string _deviceID, _simSerial, _version;
        public Register2(string deviceId = "", string simSerial = "", string version = "1", string phone_no = "")
        {
            InitializeComponent();
            _deviceID = deviceId;
            _simSerial = simSerial;
            _version = version;
            Padding = new Thickness(0, 0, 0, 60);
            if (Device.RuntimePlatform == Device.iOS)
            {
                BackgroundColor = Color.WhiteSmoke;
                loginCard.BackgroundColor = Color.Transparent;
                loginForm.BackgroundColor = Color.FromHex("#0A2239");
                loginForm.BorderColor = Color.Transparent;
            }
        }

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                await DisplayAlert("ไม่สามารถลงทะเบียนได้", "กรุณากรอกหมายเลขโทรศัพท์ของท่าน", "ตกลง");
                txtPhone.Focus();
                return;
            }
            var version = double.Parse(_version);
            m_register data = new m_register();
            //data.citizen_no = txtCitizen.Text;
            data.phone_no = txtPhone.Text;
            //data.pin = txtPin.Text;
            data.device_id = _deviceID;
            data.app_version = version;
            if (Device.RuntimePlatform == Device.iOS)
            {
                data.serial_sim = "1111111111";
                data.platform = "IOS";
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
                data.serial_sim = "2222222222";
                data.platform = "ANDROID";
            }
            data.brand = DeviceInfo.Manufacturer;
            data.model = DeviceInfo.Model;
            data.api_version = DeviceInfo.VersionString;

            var result = User.preIdentify(data.phone_no);
            if (result != null)
            {
                if (result.code == 200)
                {
                    if (result.data.Count > 1)
                    {
                        List<string> menu = new List<string>();
                        foreach (var n in result.data)
                        {
                            menu.Add("คุณ" + n.CUST_NAME + "\n" + n.CITIZEN_NO);
                        }
                        var action = await DisplayActionSheet("D-Mobile App ยินดีต้อนรับ\n(กรุณาตรวจการชำระเงินทุกวัน)\nกรูณากดเลือก", null, "ไม่พบชื่อของท่าน", menu.ToArray());
                        Debug.WriteLine($"pipe => The action is {action}");
                        if (!string.IsNullOrEmpty(action))
                        {
                            if (action != "ไม่พบชื่อของท่าน")
                            {
                                var index = menu.IndexOf(action);
                                data.cust_no = result.data[index].CUST_NO;
                                register(data);
                            }
                            else if (action == "ไม่พบชื่อของท่าน")
                            {
                                txtPhone.Focus();
                            }
                        }
                    }
                    else
                    {
                        var action = await DisplayAlert("D-Mobile App ยินดีต้อนรับคุณ\n(กรุณาตรวจการชำระเงินทุกวัน)", $"คุณ {result.data.First().CUST_NAME}", "ใช่", "ไม่ใช่");
                        Debug.WriteLine($"pipe => The action is {action}");
                        if (action)
                        {
                            data.cust_no = result.data.First().CUST_NO;
                            register(data);
                        }
                        else
                            txtPhone.Focus();
                    }
                }
                else
                {
                    await DisplayAlert("ไม่สามารถลงทะเบียนได้", result.message, "ตกลง");
                }
            }
        }
        private void register(m_register data)
        {
            var result = User.register(data);
            if (result != null)
            {
                if (result.code == 200)
                {
                    if (Device.RuntimePlatform == Device.Android)
                        DependencyService.Get<IMessage>().longAlert("ลงทะเบียนสำเร็จ");


                    Navigation.PushAsync(new Mainpage(result.data, _deviceID, _simSerial, _version));
                    Navigation.RemovePage(this);

                }
                else
                {
                    DisplayAlert("ไม่สามารถลงทะเบียนได้", result.message, "ตกลง");
                    //if(Device.RuntimePlatform == Device.Android)
                    //DependencyService.Get<IMessage>().longAlert(result.message);
                }
            }
            else
            {
                DisplayAlert("ไม่สามารถลงทะเบียนได้", "พบข้อผิดพลาดจากเซิฟเวอร์ กรุณาลงทะเบียนใหม่ในภายหลัง", "ตกลง");
                txtPhone.Text = "";
                //if(Device.RuntimePlatform == Device.Android)
                //DependencyService.Get<IMessage>().longAlert("พบข้อผิดพลาดจากเซิฟเวอร์ ไม่สามารถลงทะเบียนได้");
            }
        }
    }
}
