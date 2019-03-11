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
            data.serial_sim = _simSerial;
            var result = Services.User.register(data);
            if(result != null)
            {
                if(result.code == 200)
                {
                    DependencyService.Get<IMessage>().longAlert("ลงทะเบียนสำเร็จ");                  
                    Navigation.PushAsync(new ListSms(result.data.CUST_NO));
                    Navigation.RemovePage(this);
                }
            }
            
        }
    }
}