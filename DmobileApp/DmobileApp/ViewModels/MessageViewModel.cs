using System;
using System.Collections.Generic;
using System.Text;

namespace DmobileApp.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        //public string Text { get; set; }
        //public DateTime MessageDateTime { get; set; }
        //public bool IsIncoming { get; set; }

        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; RaisePropertyChanged(); }
        }

        private DateTime messageDateTime;

        public DateTime MessageDateTime
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

        private string attachementUrl;

        public string AttachementUrl
        {
            get { return attachementUrl; }
            set { attachementUrl = value; RaisePropertyChanged(); }
        }
    }
}
