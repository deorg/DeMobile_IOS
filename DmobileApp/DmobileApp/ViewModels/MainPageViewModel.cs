﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using DmobileApp.Model;
using Xamarin.Forms;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;

namespace DmobileApp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ObservableCollection<MessageViewModel> messagesList;
        private INavigation _navigation;
        private string _cust_name;

        public ObservableCollection<MessageViewModel> Messages
        {
            get { return messagesList; }
            set { messagesList = value; RaisePropertyChanged(); }
        }
        private int _cust_no;
        private string outgoingText;

        private int _skip = 0;
        private int _take = 10;
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
        private async Task refreshAsync()
        {
            if (_cust_no != 0)
            {

                var items = await Services.User.getSmsOffsetAsync(_cust_no, _skip += 10, _take);
                if (items.code == 200)
                {

                    if (items.data.Count != 0)
                    {
                        foreach (var msg in items.data)
                        {
                            Messages.Insert(0, new MessageViewModel(this._navigation, this._cust_name)
                            {
                                Text = msg.sms_note,
                                IsIncoming = msg.sender_type == "SYSTEM" ? true : false,
                                MessageDateTime = msg.sms_time.ToString("dd/MM/yyyy HH:mm"),
                                AttachementUrl = msg.msg_type == "IMAGE" ? msg.sms_note : null
                                //MessageDateTime = msg.sms_time
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
        public ICommand ViewImgCommand { get; set; }


        public MainPageViewModel(int cust_no, INavigation navigation, string cust_name)
        {
            // Initialize with default values
            try
            {
                _cust_no = cust_no;
                if (cust_no != 0)
                {
                    Messages = new ObservableCollection<MessageViewModel>();
                    var items = Services.User.getSmsOffsetAsync(cust_no, _skip, _take);
                    //var items = Services.User.getSmsOffset2(this._smsreq);
                    if (items.Result.code == 200)
                    {
                        //if (Device.RuntimePlatform == Device.Android)
                            //DependencyService.Get<IMessage>().longAlert("ดึงข้อมูลสำเร็จ");
                        if (items.Result.data.Count != 0)
                        {
                            foreach (var msg in items.Result.data)
                            {
                                Messages.Add(new MessageViewModel(navigation, cust_name)
                                {
                                    Text = msg.sms_note,
                                    IsIncoming = msg.sender_type == "SYSTEM" ? true : false,
                                    MessageDateTime = msg.sms_time.ToString("dd/MM/yyyy HH:mm"),
                                    AttachementUrl = msg.msg_type == "IMAGE" ? msg.sms_note : null
                                    //MessageDateTime = msg.sms_time
                                });
                            }
                            //Messages.Add(new MessageViewModel(navigation, cust_name)
                            //{
                            //    AttachementUrl = "http://35.197.153.92/Images/banks/bk.png",
                            //    IsIncoming = true,
                            //    MessageDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                            //});
                            //Messages.Add(new MessageViewModel(navigation, cust_name)
                            //{
                            //    AttachementUrl = "http://35.197.153.92/Images/banks/kb.png",
                            //    IsIncoming = true,
                            //    MessageDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
                            //});
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
                        Messages.Add(new MessageViewModel(navigation, cust_name) { Text = OutGoingText, IsIncoming = false, MessageDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm") });
                        OutGoingText = null;                   
                    }
                });
                ViewImgCommand = new Command(() =>
                {
                    Debug.WriteLine("PIPE => view image command");
                });
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                if (e.Message == "One or more errors occurred. (One or more errors occurred. (Connection reset))" || e.Message == "One or more errors occurred. (One or more errors occurred. (unexpected end of stream on com.android.okhttp.Address@ae191fe7))")
                {
                    if (Device.RuntimePlatform == Device.Android)
                        DependencyService.Get<IMessage>().longAlert("ขาดการเชื่อมต่อจากอินเทอร์เน็ต!");
                    //if (cust_no != 0)
                    //{
                    //    Messages = new ObservableCollection<MessageViewModel>();
                    //    var items = Services.User.getSmsOffsetAsync(cust_no, _skip, _take);
                    //    if (items.Result.code == 200)
                    //    {
                    //        //if (Device.RuntimePlatform == Device.Android)
                    //        //DependencyService.Get<IMessage>().longAlert("ดึงข้อมูลสำเร็จ");
                    //        if (items.Result.data.Count != 0)
                    //        {
                    //            foreach (var msg in items.Result.data)
                    //            {
                    //                Messages.Add(new MessageViewModel
                    //                {
                    //                    Text = msg.sms_note.Replace("\r", "\n"),
                    //                    IsIncoming = msg.sender_type == "SYSTEM" ? true : false,
                    //                    //MessageDateTime = DateTime.Now
                    //                    MessageDateTime = msg.sms_time
                    //                });
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (Device.RuntimePlatform == Device.Android)
                    //            DependencyService.Get<IMessage>().longAlert(items.Result.message);
                    //    }
                    //}
                    //else
                        //DependencyService.Get<IMessage>().longAlert("ไม่พบข้อมูลของคุณในระบบ SMS");
                }
                else
                {
                    if (Device.RuntimePlatform == Device.Android)
                        DependencyService.Get<IMessage>().longAlert(e.Message);
                }
            }   
        }
        // public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
    }
}
