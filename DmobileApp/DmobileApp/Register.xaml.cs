using DmobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DmobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private string _deviceID, _simSerial;
        public Register(string deviceId, string simSerial)
        {
            InitializeComponent();
            _deviceID = deviceId;
            _simSerial = simSerial;
        }
        private void btnRegister_Clicked(object sender, EventArgs e)
        {
            m_register data = new m_register();
            data.citizen_no = txtCitizen.Text;
            data.phone_no = txtPhone.Text;
            data.pin = txtPin.Text;
            data.device_id = _deviceID;
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
            var result = Services.User.register(data);
            if(result != null)
            {
                if(result.code == 200)
                {
                    if(Device.RuntimePlatform == Device.Android)
                        DependencyService.Get<IMessage>().longAlert("ลงทะเบียนสำเร็จ");

                    if (result.data.PERMIT == "BOTH")
                    {
                        Navigation.PushAsync(new Mainpage(result.data));
                        Navigation.RemovePage(this);
                    }
                    else if(result.data.PERMIT == "SMS")
                    {
                        Navigation.PushAsync(new ChatSms(result.data));
                        Navigation.RemovePage(this);
                    }
                    else if(result.data.PERMIT == "PAYMENT")
                    {
                        Navigation.PushAsync(new LoanPage(result.data));
                        Navigation.RemovePage(this);
                        
                    }
                }
                else
                {
                    if(Device.RuntimePlatform == Device.Android)
                        DependencyService.Get<IMessage>().longAlert(result.message);
                }
            }
            else
            {
                if(Device.RuntimePlatform == Device.Android)
                    DependencyService.Get<IMessage>().longAlert("พบข้อผิดพลาดจากเซิฟเวอร์ ไม่สามารถลงทะเบียนได้");
            }
        }
    }
}