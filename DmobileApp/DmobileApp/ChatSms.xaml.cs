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
        private string _cust_name;
        private double app_version, current_version;
        private List<string> notiMessage = new List<string>();
        private List<ColorItem> colors = new List<ColorItem>();
        //private int count = 0;
        //private double _notiWidth;

        public ChatSms(profile_data profile, string version)
		{
			InitializeComponent ();
            _cust_name = profile.CUST_NAME;
            Title = profile.CUST_NAME;
            app_version = profile.APP_VERSION;

            current_version = double.Parse(version);
            //getListColor();
            //notiBox.BackgroundColor = colors[count].Color;
            //notiTxt.Text = colors[count].Name;
            if (profile.BROADCAST != null)
            {
                notiBox.IsVisible = true;
                notiFrame.BackgroundColor = Color.GhostWhite;
                notiTxt.Text = profile.BROADCAST.NOTE;
            }

            //if (2.8 > current_version)
            //{
            //    var toolBarItem = new ToolbarItem("update", "update.png", () =>
            //    {
            //        Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.Domestic.DmobileApp"));
            //    }, 0, 0);

            //    ToolbarItems.Add(toolBarItem);
            //}

            if (app_version > current_version)
            {
                var toolBarItem = new ToolbarItem();
                toolBarItem.Text = "กดอัพเดท\nเวอร์ชั่นใหม่";
                toolBarItem.Clicked += (sender, e) => {
                    //if (count != 0)
                    //{
                    //    count--;
                    //    notiBox.BackgroundColor = colors[count].Color;
                    //    notiTxt.Text = colors[count].Name;
                    //}
                    if(Device.RuntimePlatform == Device.Android)
                        Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.Domestic.DmobileApp"));
                    else if(Device.RuntimePlatform == Device.iOS)
                        Device.OpenUri(new Uri("https://itunes.apple.com/us/app/d-mobileapp/id1457641979?ls=1&mt=8"));
                };
                ToolbarItems.Add(toolBarItem);
                //toolBarItem = new ToolbarItem("กดอัพเดท", "update.png", () =>
                //{
                //    Device.OpenUri(new Uri("https://play.google.com/store/apps/details?id=com.Domestic.DmobileApp"));
                //},0,0);
                //ToolbarItems.Add(toolBarItem);
            }
            else
            {
                if (Device.RuntimePlatform == Device.Android)
                    ToolbarItems.Add(new ToolbarItem() { Text = "เวอร์ชั่น\n" + version });
                else if (Device.RuntimePlatform == Device.iOS)
                    ToolbarItems.Add(new ToolbarItem() { Text = "เวอร์ชั่น" + version });
            }

            _cust_no = profile.CUST_NO;
            if (profile.CHAT == "ON")
                chatBox.IsVisible = true;
            //BindingContext = new MainPageViewModel(profile.CUST_NO);
           
            //var last = MessagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
            //MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
        }

        protected override void OnAppearing()
        {
            var messages = new MainPageViewModel(_cust_no, Navigation, _cust_name);
            if (messages.Messages.Count > 0)
            {
                BindingContext = messages;
                var last = MessagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
                MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
            }
            //startNotiMessage();
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
            if (message != null && message.HasAttachement == false)
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
            if (!string.IsNullOrEmpty(notiTxt.Text))
            {
                await notiTxt.FadeTo(0.5, 800, Easing.SinInOut);
                await notiTxt.FadeTo(1, 800, Easing.SinInOut);
                await notiTxt.FadeTo(0.5, 800, Easing.SinInOut);
                await notiTxt.FadeTo(1, 800, Easing.SinInOut);
                await notiTxt.FadeTo(0.5, 800, Easing.SinInOut);
                await notiTxt.FadeTo(1, 800, Easing.SinInOut);
                await notiTxt.FadeTo(0.5, 800, Easing.SinInOut);
                await notiTxt.FadeTo(1, 800, Easing.SinInOut);
                await notiTxt.FadeTo(0.5, 800, Easing.SinInOut);
                await notiTxt.FadeTo(1, 800, Easing.SinInOut);
            }
            //foreach (var msg in notiMessage)
            //{
            //    notiTxt.Text = msg;
            //    _notiWidth = notiTxt.TranslationX = notiTxt.Width;
            //    Debug.WriteLine("noti widht =======> " + _notiWidth);
            //    await notiTxt.TranslateTo(-_notiWidth, 0, 18000, Easing.SinIn);
            //    notiTxt.TranslationX = _notiWidth;

            //}
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
            //adsBox.IsVisible = false;
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

        private void getListColor()
        {
            colors.Add(new ColorItem() { Color = Color.Accent, Name = "Accent" });
            colors.Add(new ColorItem() { Color = Color.AliceBlue, Name = "AliceBlue" });
            colors.Add(new ColorItem() { Color = Color.MintCream, Name = "MintCream" });
            colors.Add(new ColorItem() { Color = Color.MistyRose, Name = "MistyRose" });
            colors.Add(new ColorItem() { Color = Color.Moccasin, Name = "Moccasin" });
            colors.Add(new ColorItem() { Color = Color.NavajoWhite, Name = "NavajoWhite" });
            colors.Add(new ColorItem() { Color = Color.Navy, Name = "Navy" });
            colors.Add(new ColorItem() { Color = Color.OldLace, Name = "OldLace" });
            colors.Add(new ColorItem() { Color = Color.MidnightBlue, Name = "MidnightBlue" });
            colors.Add(new ColorItem() { Color = Color.Olive, Name = "Olive" });
            colors.Add(new ColorItem() { Color = Color.Orange, Name = "Orange" });
            colors.Add(new ColorItem() { Color = Color.OrangeRed, Name = "OrangeRed" });
            colors.Add(new ColorItem() { Color = Color.Orchid, Name = "Orchid" });
            colors.Add(new ColorItem() { Color = Color.PaleGoldenrod, Name = "PaleGoldenrod" });
            colors.Add(new ColorItem() { Color = Color.PaleGreen, Name = "PaleGreen" });
            colors.Add(new ColorItem() { Color = Color.PaleTurquoise, Name = "PaleTurquoise" });
            colors.Add(new ColorItem() { Color = Color.OliveDrab, Name = "OliveDrab" });
            colors.Add(new ColorItem() { Color = Color.PaleVioletRed, Name = "PaleVioletRed" });
            colors.Add(new ColorItem() { Color = Color.MediumVioletRed, Name = "MediumVioletRed" });
            colors.Add(new ColorItem() { Color = Color.MediumSpringGreen, Name = "MediumSpringGreen" });
            colors.Add(new ColorItem() { Color = Color.LightSkyBlue, Name = "LightSkyBlue" });
            colors.Add(new ColorItem() { Color = Color.LightSlateGray, Name = "LightSlateGray" });
            colors.Add(new ColorItem() { Color = Color.LightSteelBlue, Name = "LightSteelBlue" });
            colors.Add(new ColorItem() { Color = Color.LightYellow, Name = "LightYellow" });
            colors.Add(new ColorItem() { Color = Color.Lime, Name = "Lime" });
            colors.Add(new ColorItem() { Color = Color.LimeGreen, Name = "LimeGreen" });
            colors.Add(new ColorItem() { Color = Color.MediumTurquoise, Name = "MediumTurquoise" });
            colors.Add(new ColorItem() { Color = Color.Linen, Name = "Linen" });
            colors.Add(new ColorItem() { Color = Color.Maroon, Name = "Maroon" });
            colors.Add(new ColorItem() { Color = Color.MediumAquamarine, Name = "MediumAquamarine" });
            colors.Add(new ColorItem() { Color = Color.MediumBlue, Name = "MediumBlue" });
            colors.Add(new ColorItem() { Color = Color.MediumOrchid, Name = "MediumOrchid" });
            colors.Add(new ColorItem() { Color = Color.MediumSeaGreen, Name = "MediumSeaGreen" });
            colors.Add(new ColorItem() { Color = Color.MediumSlateBlue, Name = "MediumSlateBlue" });
            colors.Add(new ColorItem() { Color = Color.Magenta, Name = "Magenta" });
            colors.Add(new ColorItem() { Color = Color.LightSeaGreen, Name = "LightSeaGreen" });
            colors.Add(new ColorItem() { Color = Color.PapayaWhip, Name = "PapayaWhip" });
            colors.Add(new ColorItem() { Color = Color.Peru, Name = "Peru" });
            colors.Add(new ColorItem() { Color = Color.SpringGreen, Name = "SpringGreen" });
            colors.Add(new ColorItem() { Color = Color.SteelBlue, Name = "SteelBlue" });
            colors.Add(new ColorItem() { Color = Color.Tan, Name = "Tan" });
            colors.Add(new ColorItem() { Color = Color.Teal, Name = "Teal" });
            colors.Add(new ColorItem() { Color = Color.Thistle, Name = "Thistle" });
            colors.Add(new ColorItem() { Color = Color.Tomato, Name = "Tomato" });
            colors.Add(new ColorItem() { Color = Color.Snow, Name = "Snow" });
            colors.Add(new ColorItem() { Color = Color.Transparent, Name = "Transparent" });
            colors.Add(new ColorItem() { Color = Color.Violet, Name = "Violet" });
            colors.Add(new ColorItem() { Color = Color.Wheat, Name = "Wheat" });
            colors.Add(new ColorItem() { Color = Color.White, Name = "White" });
            colors.Add(new ColorItem() { Color = Color.WhiteSmoke, Name = "WhiteSmoke" });
            colors.Add(new ColorItem() { Color = Color.Yellow, Name = "Yellow" });
            colors.Add(new ColorItem() { Color = Color.YellowGreen, Name = "YellowGreen" });
            colors.Add(new ColorItem() { Color = Color.Turquoise, Name = "Turquoise" });
            colors.Add(new ColorItem() { Color = Color.PeachPuff, Name = "PeachPuff" });
            colors.Add(new ColorItem() { Color = Color.SlateGray, Name = "SlateGray" });
            colors.Add(new ColorItem() { Color = Color.SkyBlue, Name = "SkyBlue" });
            colors.Add(new ColorItem() { Color = Color.Pink, Name = "Pink" });
            colors.Add(new ColorItem() { Color = Color.Plum, Name = "Plum" });
            colors.Add(new ColorItem() { Color = Color.PowderBlue, Name = "PowderBlue" });
            colors.Add(new ColorItem() { Color = Color.Purple, Name = "Purple" });
            colors.Add(new ColorItem() { Color = Color.Red, Name = "Red" });
            colors.Add(new ColorItem() { Color = Color.RosyBrown, Name = "RosyBrown" });
            colors.Add(new ColorItem() { Color = Color.SlateBlue, Name = "SlateBlue" });
            colors.Add(new ColorItem() { Color = Color.RoyalBlue, Name = "RoyalBlue" });
            colors.Add(new ColorItem() { Color = Color.Salmon, Name = "Salmon" });
            colors.Add(new ColorItem() { Color = Color.SandyBrown, Name = "SandyBrown" });
            colors.Add(new ColorItem() { Color = Color.SeaGreen, Name = "SeaGreen" });
            colors.Add(new ColorItem() { Color = Color.SeaShell, Name = "SeaShell" });
            colors.Add(new ColorItem() { Color = Color.Sienna, Name = "Sienna" });
            colors.Add(new ColorItem() { Color = Color.Silver, Name = "Silver" });
            colors.Add(new ColorItem() { Color = Color.SaddleBrown, Name = "SaddleBrown" });
            colors.Add(new ColorItem() { Color = Color.LightSalmon, Name = "LightSalmon" });
            colors.Add(new ColorItem() { Color = Color.MediumPurple, Name = "MediumPurple" });
            colors.Add(new ColorItem() { Color = Color.LightGreen, Name = "LightGreen" });
            colors.Add(new ColorItem() { Color = Color.Crimson, Name = "Crimson" });
            colors.Add(new ColorItem() { Color = Color.Cyan, Name = "Cyan" });
            colors.Add(new ColorItem() { Color = Color.LightPink, Name = "LightPink" });
            colors.Add(new ColorItem() { Color = Color.DarkCyan, Name = "DarkCyan" });
            colors.Add(new ColorItem() { Color = Color.DarkGoldenrod, Name = "DarkGoldenrod" });
            colors.Add(new ColorItem() { Color = Color.DarkGray, Name = "DarkGray" });
            colors.Add(new ColorItem() { Color = Color.Cornsilk, Name = "Cornsilk" });
            colors.Add(new ColorItem() { Color = Color.DarkGreen, Name = "DarkGreen" });
            colors.Add(new ColorItem() { Color = Color.DarkMagenta, Name = "DarkMagenta" });
            colors.Add(new ColorItem() { Color = Color.DarkOliveGreen, Name = "DarkOliveGreen" });
            colors.Add(new ColorItem() { Color = Color.DarkOrange, Name = "DarkOrange" });
            colors.Add(new ColorItem() { Color = Color.DarkOrchid, Name = "DarkOrchid" });
            colors.Add(new ColorItem() { Color = Color.DarkRed, Name = "DarkRed" });
            colors.Add(new ColorItem() { Color = Color.DarkSalmon, Name = "DarkSalmon" });
            colors.Add(new ColorItem() { Color = Color.DarkKhaki, Name = "DarkKhaki" });
            colors.Add(new ColorItem() { Color = Color.DarkSeaGreen, Name = "DarkSeaGreen" });
            colors.Add(new ColorItem() { Color = Color.CornflowerBlue, Name = "CornflowerBlue" });
            colors.Add(new ColorItem() { Color = Color.Chocolate, Name = "Chocolate" });
            colors.Add(new ColorItem() { Color = Color.AntiqueWhite, Name = "AntiqueWhite" });
            colors.Add(new ColorItem() { Color = Color.Aqua, Name = "Aqua" });
            colors.Add(new ColorItem() { Color = Color.Aquamarine, Name = "Aquamarine" });
            colors.Add(new ColorItem() { Color = Color.Azure, Name = "Azure" });
            colors.Add(new ColorItem() { Color = Color.Beige, Name = "Beige" });
            colors.Add(new ColorItem() { Color = Color.Bisque, Name = "Bisque" });
            colors.Add(new ColorItem() { Color = Color.Coral, Name = "Coral" });
            colors.Add(new ColorItem() { Color = Color.Black, Name = "Black" });
            colors.Add(new ColorItem() { Color = Color.Blue, Name = "Blue" });
            colors.Add(new ColorItem() { Color = Color.BlueViolet, Name = "BlueViolet" });
            colors.Add(new ColorItem() { Color = Color.Brown, Name = "Brown" });
            colors.Add(new ColorItem() { Color = Color.BurlyWood, Name = "BurlyWood" });
            colors.Add(new ColorItem() { Color = Color.CadetBlue, Name = "CadetBlue" });
            colors.Add(new ColorItem() { Color = Color.Chartreuse, Name = "Chartreuse" });
            colors.Add(new ColorItem() { Color = Color.BlanchedAlmond, Name = "BlanchedAlmond" });
            colors.Add(new ColorItem() { Color = Color.DarkSlateBlue, Name = "DarkSlateBlue" });
            colors.Add(new ColorItem() { Color = Color.DarkBlue, Name = "DarkBlue" });
            colors.Add(new ColorItem() { Color = Color.DarkTurquoise, Name = "DarkTurquoise" });
            colors.Add(new ColorItem() { Color = Color.HotPink, Name = "HotPink" });
            colors.Add(new ColorItem() { Color = Color.IndianRed, Name = "IndianRed" });
            colors.Add(new ColorItem() { Color = Color.Indigo, Name = "Indigo" });
            colors.Add(new ColorItem() { Color = Color.Ivory, Name = "Ivory" });
            colors.Add(new ColorItem() { Color = Color.Khaki, Name = "Khaki" });
            colors.Add(new ColorItem() { Color = Color.Lavender, Name = "Lavender" });
            colors.Add(new ColorItem() { Color = Color.Honeydew, Name = "Honeydew" });
            colors.Add(new ColorItem() { Color = Color.LavenderBlush, Name = "LavenderBlush" });
            colors.Add(new ColorItem() { Color = Color.LemonChiffon, Name = "LemonChiffon" });
            colors.Add(new ColorItem() { Color = Color.LightBlue, Name = "LightBlue" });
            colors.Add(new ColorItem() { Color = Color.LightCoral, Name = "LightCoral" });
            colors.Add(new ColorItem() { Color = Color.DarkSlateGray, Name = "DarkSlateGray" });
            colors.Add(new ColorItem() { Color = Color.LightGoldenrodYellow, Name = "LightGoldenrodYellow" });
            colors.Add(new ColorItem() { Color = Color.LightGray, Name = "LightGray" });
            colors.Add(new ColorItem() { Color = Color.Gray, Name = "Gray" });
            colors.Add(new ColorItem() { Color = Color.Green, Name = "Green" });
            colors.Add(new ColorItem() { Color = Color.DarkViolet, Name = "DarkViolet" });
            colors.Add(new ColorItem() { Color = Color.DeepPink, Name = "DeepPink" });
            colors.Add(new ColorItem() { Color = Color.DeepSkyBlue, Name = "DeepSkyBlue" });
            colors.Add(new ColorItem() { Color = Color.DodgerBlue, Name = "DodgerBlue" });
            colors.Add(new ColorItem() { Color = Color.Firebrick, Name = "Firebrick" });
            colors.Add(new ColorItem() { Color = Color.FloralWhite, Name = "FloralWhite" });
            colors.Add(new ColorItem() { Color = Color.DimGray, Name = "DimGray" });
            colors.Add(new ColorItem() { Color = Color.Fuchsia, Name = "Fuchsia" });
            colors.Add(new ColorItem() { Color = Color.Gainsboro, Name = "Gainsboro" });
            colors.Add(new ColorItem() { Color = Color.Goldenrod, Name = "Goldenrod" });
            colors.Add(new ColorItem() { Color = Color.GhostWhite, Name = "GhostWhite" });
            colors.Add(new ColorItem() { Color = Color.Gold, Name = "Gold" });
            colors.Add(new ColorItem() { Color = Color.ForestGreen, Name = "ForestGreen" });
        }
    }
    public class ColorItem
    {
        public Color Color { get; set; } = Color.Red;
        public string Name { get; set; }
        public string Representation { get { return this.Color.ToString(); } }
    }
}