using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace DmobileApp.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        //public string Text { get; set; }
        //public DateTime MessageDateTime { get; set; }
        //public bool IsIncoming { get; set; }
        public ICommand ViewImgCommand { get; set; }
        public INavigation navigation;
        public MessageViewModel(INavigation navigation, string cust_name)
        {
            this.navigation = navigation;
            ViewImgCommand = new Command(() =>
            {
                Debug.WriteLine("PIPE => view image command");
                Debug.WriteLine(attachementUrl);
                this.navigation.PushAsync(new ViewImage(attachementUrl, cust_name), true);
                //Navigation.PushAsync(new Payment(_profile, item, _deviceId, _serialSim, _version));
            });
        }


        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(); }
        }

        private string messageDateTime;

        public string MessageDateTime
        {
            get { return messageDateTime; }
            set { messageDateTime = value; RaisePropertyChanged(); }
        }

        private bool isIncoming;

        public bool IsIncoming
        {
            get { return isIncoming; }
            set { isIncoming = value; RaisePropertyChanged(); }
        }

        public bool HasAttachement => !string.IsNullOrEmpty(attachementUrl);
        public bool HasText => !string.IsNullOrEmpty(text);

        private string attachementUrl;

        public string AttachementUrl
        {
            get { return attachementUrl; }
            set { attachementUrl = value; RaisePropertyChanged(); }
        }
    }
}
