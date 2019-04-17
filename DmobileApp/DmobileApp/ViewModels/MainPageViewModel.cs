using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using DmobileApp.Model;
using Xamarin.Forms;
using System.Linq;

namespace DmobileApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<MessageViewModel> messagesList;

        public ObservableCollection<MessageViewModel> Messages
        {
            get { return messagesList; }
            set { messagesList = value; RaisePropertyChanged(); }
        }
        private int _cust_no;
        private string outgoingText;

        private int _skip = 0;
        private int _take = 5;
        public string OutGoingText
        {
            get { return outgoingText; }
            set { outgoingText = value; RaisePropertyChanged(); }
        }
        private bool _isRefreshing = false;
        public bool IsRefreshing {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged("IsRefreshing");
            }
        }
        private async System.Threading.Tasks.Task refreshAsync()
        {
            if (_cust_no != 0)
            {
                //Messages = new ObservableCollection<MessageViewModel>();
                var items = await Services.User.getSmsOffsetAsync(_cust_no, _skip += 5, _take);
                //if (items.data.Count > 0)
                //items.data = items.data.OrderBy(p => p.sms010_pk).ToList();
                if (items.code == 200)
                {
                    //if (Device.RuntimePlatform == Device.Android)
                    //DependencyService.Get<IMessage>().longAlert("ดึงข้อมูลสำเร็จ");
                    if (items.data.Count != 0)
                    {
                        foreach (var msg in items.data)
                        {
                            Messages.Insert(0, new MessageViewModel
                            {
                                Text = msg.sms_note,
                                IsIncoming = msg.sender_type == "SYSTEM" ? true : false,
                                //MessageDateTime = DateTime.Now
                                MessageDateTime = msg.sms_time
                            });
                        }
                    }
                }
                else
                {
                    if (Device.RuntimePlatform == Device.Android)
                        DependencyService.Get<IMessage>().longAlert(items.message);
                }
            }
            else
                DependencyService.Get<IMessage>().longAlert("ไม่พบข้อมูลของคุณในระบบ SMS");
        }
        public ICommand RefreshSms
        {
            get
            {
                return new Command(async () => 
                {
                    IsRefreshing = true;
                    await refreshAsync();
                    IsRefreshing = false;
                });
            }
        }
        public ICommand SendCommand { get; set; }


        public MainPageViewModel(int cust_no)
        {
            // Initialize with default values
            try
            {
                _cust_no = cust_no;
                if (cust_no != 0)
                {
                    Messages = new ObservableCollection<MessageViewModel>();                   
                    var items = Services.User.getSmsOffsetAsync(cust_no, _skip, _take);
                    if (items.Result.code == 200)
                    {
                        //if (Device.RuntimePlatform == Device.Android)
                            //DependencyService.Get<IMessage>().longAlert("ดึงข้อมูลสำเร็จ");
                        if (items.Result.data.Count != 0)
                        {
                            foreach (var msg in items.Result.data)
                            {
                                Messages.Add(new MessageViewModel
                                {
                                    Text = msg.sms_note,
                                    IsIncoming = msg.sender_type == "SYSTEM" ? true : false,
                                    //MessageDateTime = DateTime.Now
                                    MessageDateTime = msg.sms_time
                                });
                            }
                        }
                    }
                    else
                    {
                        if(Device.RuntimePlatform == Device.Android)
                            DependencyService.Get<IMessage>().longAlert(items.Result.message);
                    }
                }
                else
                    DependencyService.Get<IMessage>().longAlert("ไม่พบข้อมูลของคุณในระบบ SMS");
                //Messages = new ObservableCollection<MessageViewModel>
                //{
                //    new MessageViewModel { Text = "Hi Squirrelj;iajiojo;oiiiiiiiiiiiiiiiiiiiiiigsegejo! \uD83D\uDE0A", IsIncoming = true, MessageDateTime = DateTime.Now },
                //    //new MessageViewModel { Text = "Hi Baboon, How are you? \uD83D\uDE0A", IsIncoming = false, MessagDateTime = DateTime.Now.AddMinutes(-24)},
                //    //new MessageViewModel { Text = "We've a party at Mandrill's. Would you like to join? We would love to have you there! \uD83D\uDE01", IsIncoming = true, MessagDateTime = DateTime.Now.AddMinutes(-23)},
                //    //new MessageViewModel { Text = "You will love it. Don't miss.", IsIncoming = true, MessagDateTime = DateTime.Now.AddMinutes(-23)},
                //    //new MessageViewModel { Text = "Sounds like a plan. \uD83D\uDE0E", IsIncoming = false, MessagDateTime = DateTime.Now.AddMinutes(-23)},
                //    //new MessageViewModel { Text = "\uD83D\uDE48 \uD83D\uDE49 \uD83D\uDE49", IsIncoming = false, MessagDateTime = DateTime.Now.AddMinutes(-23)},
                //};
                OutGoingText = null;
                SendCommand = new Command(() =>
                {
                    if (!string.IsNullOrEmpty(OutGoingText))
                    {
                        Messages.Add(new MessageViewModel { Text = OutGoingText, IsIncoming = false, MessageDateTime = DateTime.Now });
                        OutGoingText = null;                   
                    }
                });
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                if(Device.RuntimePlatform == Device.Android)
                    DependencyService.Get<IMessage>().longAlert(e.Message);
            }   
        }
        // public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
    }
}
