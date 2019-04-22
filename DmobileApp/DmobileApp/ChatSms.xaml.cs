using DmobileApp.Model;
using DmobileApp.Services;
using DmobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DmobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChatSms : ContentPage
	{
        private int _cust_no;
        private double app_version, current_version;
        private List<string> notiMessage = new List<string>();
        private double _notiWidth;

        public ChatSms(profile_data profile, string version)
		{
			InitializeComponent ();
            Title = profile.CUST_NAME;
            app_version = profile.APP_VERSION;
            app_version = 2.9;
            current_version = double.Parse(version);
            notiMessage.Add("ยินดีต้อนรับคุณณัฏฐพัชร ชัยประพันธ์");
            notiMessage.Add("ข้อความแจ้งเตือนจะถูกอัพเดทในเวลาประมาณ 8.30น.ของทุกวัน");
            notiMessage.Add("หากมีข้อสงสัยกรุณาติดต่อ 034762422");
            notiTxt.Text = notiMessage.FirstOrDefault();
            //notiTxt.TranslationX = 300;
            //notiTxt.Text = notiMessage.FirstOrDefault();


            //if (2.8 > current_version)
            //{
            //    var toolBarItem = new ToolbarItem("update", "update.png", () =>
            //    {
            //        Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.Domestic.DmobileApp"));
            //    }, 0, 0);

            //    ToolbarItems.Add(toolBarItem);
            //}
            ToolbarItems.Add(new ToolbarItem() { Text = "เวอร์ชั่น "+version });
            if(app_version > current_version)
            {
                var tooBarItem = new ToolbarItem("กดอัดเดท", "update.png", () =>
                {
                    Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.Domestic.DmobileApp"));
                },0,0);
                ToolbarItems.Add(tooBarItem);
            }
            //Debug.WriteLine("-------------- app version => " + profile.APP_VERSION);
            //Debug.WriteLine("-------------- current version => " + version);

            _cust_no = profile.CUST_NO;
            if (profile.CHAT == "ON")
                chatBox.IsVisible = true;
            //BindingContext = new MainPageViewModel(profile.CUST_NO);
           
            //var last = MessagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
            //MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
        }

        protected override void OnAppearing()
        {
            var messages = new MainPageViewModel(_cust_no);
            //BindingContext = new MainPageViewModel(_cust_no);
            if (messages.Messages.Count > 0)
            {
                BindingContext = messages;
                //BindingContext = new MainPageViewModel(_cust_no);
                var last = MessagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
                MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
            }
            startNotiMessage();
            base.OnAppearing();


            // อย่าลืมดัก สำหรับ ios เท่านั้น
            //MessagingCenter.Subscribe<App>(this, "refreshSmsOnResume", app => {
            //    BindingContext = new MainPageViewModel(_cust_no);
            //    var last = MessagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
            //    MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
            //});
            //base.OnAppearing();

            //BindingContext = new MainPageViewModel(_cust_no);
            //var last = MessagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
            //MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
        }
        private void MyListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var message = MessagesListView.SelectedItem as MessageViewModel;
            if (message != null)
            {
                if (message.Text.Contains("อัพเดทเวอร์ชั่น"))
                {
                    if (app_version > current_version)
                        Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.Domestic.DmobileApp"));
                    else
                        DisplayAlert("", "D-MobileApp บนเครื่องของคุณเป็นเวอร์ชั่นล่าสุดแล้ว!", "ตกลง");
                }
            }

            MessagesListView.SelectedItem = null;

            //var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            //Debug.WriteLine("Display size =====> " + mainDisplayInfo.Width);
            //Debug.WriteLine("Text size ========> " + notiTxt.Width);
            //notiTxt.TranslationX = 335;
            // notiTxt.TranslateTo(500, 0, 5000, Easing.Linear);
            //notiTxt.Text = "ข้อความเปลี่ยน";
            //txtMessage.Unfocus();
        }

        private async void startNotiMessage()
        {
            //_notiWidth = notiTxt.Width;
            //notiTxt.TranslationX = 340;
            //Debug.WriteLine("noti widht =======> " + _notiWidth);
            foreach (var msg in notiMessage)
            {
                notiTxt.Text = msg;
                _notiWidth = notiTxt.TranslationX = notiTxt.Width;
                Debug.WriteLine("noti widht =======> " + _notiWidth);
                await notiTxt.TranslateTo(-_notiWidth, 0, 18000, Easing.SinIn);
                notiTxt.TranslationX = _notiWidth;

                //await notiTxt.FadeTo(0, 2000, Easing.SinInOut);
                //await Task.Delay(2000);
                //notiTxt.Text = msg;
                //await notiTxt.FadeTo(1, 2000, Easing.SinInOut);
                //await Task.Delay(2000);
            }
        }
        //void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        //{
        //    var item = e.Item as MessageViewModel;
        //    var index = MessagesListView.ItemsSource.Cast<MessageViewModel>().First();
        //    if (item != null && item == index)
        //    {
        //        Debug.WriteLine("item scroll ========> " + item.Text);
        //        Debug.WriteLine("item first =========> " + index.Text);
        //    }
        //}


        private void MyListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
            //txtMessage.Unfocus();
        }
        private void Send_Clicked(object sender, EventArgs e)
        {
            var last = MessagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
            MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
            txtMessage.Focus();
            if (!string.IsNullOrEmpty(last.Text))
            {
                var message = new m_custMessage
                {
                    cust_no = _cust_no,
                    message = last.Text
                };
                var result = User.sendSms(message);
                if(result.code != 200)
                {
                    DependencyService.Get<IMessage>().longAlert("ไม่สามารถส่งข้อความได้");
                }
            }
        }
    }
}