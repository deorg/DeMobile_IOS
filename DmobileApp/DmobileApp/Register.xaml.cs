using DmobileApp.Model;
using DmobileApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DmobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private string _deviceID, _simSerial, _version;
        public Register(string deviceId = "", string simSerial = "", string version = "1", string phone_no = "")
        {
            InitializeComponent();
            _deviceID = deviceId;
            _simSerial = simSerial;
            _version = version;


            //if (Device.RuntimePlatform == Device.Android)
            //{
            //    if (!string.IsNullOrEmpty(phone_no))
            //    {
            //        try
            //        {
            //            _phone_no = phone_no.Replace("+66", "0");
            //            txtPhone.Text = phone_no.Replace("+66", "0");
            //            DependencyService.Get<IMessage>().longAlert("ตรวจพบหมายเลขโทรศัพท์บนเครื่อง");
            //            //var sendClick = btnRegister as IButtonController;
            //            //sendClick.SendClicked();
            //        }
            //        catch
            //        {
            //            _phone_no = phone_no.Replace("+66", "0");
            //            txtPhone.Text = phone_no.Replace("+66", "0");
            //        }
            //    }
            //}
        }
        //protected override async void OnAppearing()
        //{
        //    if (!string.IsNullOrEmpty(_phone_no))
        //    {
        //        var resCheckPhone = User.preIdentify(_phone_no);
        //        if (resCheckPhone != null)
        //        {
        //            if (resCheckPhone.code == 200)
        //            {
        //                if (resCheckPhone.data.Count == 1)
        //                {
        //                    var answer = await DisplayAlert("ยืนยันตัวตน", $"คุณต้องการเข้าใช้งานด้วยชื่อ {resCheckPhone.data[0].CUST_NAME} ใช่หรือไม่", "ใช่", "ไม่ใช่");
        //                    if (answer)
        //                    {
        //                        Console.WriteLine("answer");
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("not answer");
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    base.OnAppearing();
        //    //MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
        //}
        private async void btnRegister_Clicked(object sender, EventArgs e)
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
            if(Device.RuntimePlatform == Device.iOS)
            {
                data.serial_sim = "1111111111";
                data.platform = "IOS";
            }
            else if(Device.RuntimePlatform == Device.Android)
            {
                data.serial_sim = "2222222222";
                data.platform = "ANDROID";
            }
            data.brand = DeviceInfo.Manufacturer;
            data.model = DeviceInfo.Model;
            data.api_version = DeviceInfo.VersionString;
            var result = Services.User.preIdentify(data.phone_no);
            if(result != null)
            {
                if(result.code == 200)
                {
                    if(result.data.Count > 1)
                    {
                        List<string> menu = new List<string>();
                        foreach(var n in result.data)
                        {
                            menu.Add("คุณ"+n.CUST_NAME+"\n"+n.CITIZEN_NO);
                        }
                        var action = await DisplayActionSheet("D-Mobile App ยินดีต้อนรับ\nกรูณากดเลือก", null, "ไม่พบชื่อของท่าน", menu.ToArray());
                        Debug.WriteLine($"pipe => The action is {action}");
                    }
                    else
                    {
                        var action = await DisplayAlert("D-Mobile App ยินดีต้อนรับ\nกรุณาตรวจสอบการชำระเงินทุกวัน", $"คุณ {result.data.First().CUST_NAME}", "ใช่", "ไม่ใช่");
                        Debug.WriteLine($"pipe => The action is {action}");
                        if (action)
                        {
                            register(data);
                        }
                    }
                }
                else
                {
                    await DisplayAlert("", result.message, "ตกลง");
                }
            }
            //var result = Services.User.register(data);
            //if(result != null)
            //{
            //    if(result.code == 200)
            //    {
            //        if(Device.RuntimePlatform == Device.Android)
            //            DependencyService.Get<IMessage>().longAlert("ลงทะเบียนสำเร็จ");
                       

            //        Navigation.PushAsync(new Mainpage(result.data, _deviceID, _simSerial, _version));
            //        Navigation.RemovePage(this);

            //        //if (result.data.PERMIT == "BOTH")
            //        //{
            //        //    Navigation.PushAsync(new Mainpage(result.data));
            //        //    Navigation.RemovePage(this);
            //        //}
            //        //else if(result.data.PERMIT == "SMS")
            //        //{
            //        //    Navigation.PushAsync(new ChatSms(result.data));
            //        //    Navigation.RemovePage(this);
            //        //}
            //        //else if(result.data.PERMIT == "PAYMENT")
            //        //{
            //        //    Navigation.PushAsync(new LoanPage(result.data));
            //        //    Navigation.RemovePage(this);                       
            //        //}
            //    }
            //    else
            //    {
            //        DisplayAlert("ไม่สามารถลงทะเบียนได้", result.message, "ตกลง");
            //        //if(Device.RuntimePlatform == Device.Android)
            //        //DependencyService.Get<IMessage>().longAlert(result.message);
            //    }
            //}
            //else
            //{
            //    DisplayAlert("ไม่สามารถลงทะเบียนได้", "พบข้อผิดพลาดจากเซิฟเวอร์ กรุณาลงทะเบียนใหม่ในภายหลัง", "ตกลง");
            //    txtPhone.Text = "";
            //    //if(Device.RuntimePlatform == Device.Android)
            //        //DependencyService.Get<IMessage>().longAlert("พบข้อผิดพลาดจากเซิฟเวอร์ ไม่สามารถลงทะเบียนได้");
            //}
        }
        private void register(m_register data)
        {
            var result = Services.User.register(data);
            if(result != null)
            {
                if(result.code == 200)
                {
                    if(Device.RuntimePlatform == Device.Android)
                        DependencyService.Get<IMessage>().longAlert("ลงทะเบียนสำเร็จ");


                    Navigation.PushAsync(new Mainpage(result.data, _deviceID, _simSerial, _version));
                    Navigation.RemovePage(this);

                    //if (result.data.PERMIT == "BOTH")
                    //{
                    //    Navigation.PushAsync(new Mainpage(result.data));
                    //    Navigation.RemovePage(this);
                    //}
                    //else if(result.data.PERMIT == "SMS")
                    //{
                    //    Navigation.PushAsync(new ChatSms(result.data));
                    //    Navigation.RemovePage(this);
                    //}
                    //else if(result.data.PERMIT == "PAYMENT")
                    //{
                    //    Navigation.PushAsync(new LoanPage(result.data));
                    //    Navigation.RemovePage(this);                       
                    //}
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