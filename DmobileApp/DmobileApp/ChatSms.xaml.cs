using DmobileApp.Model;
using DmobileApp.Services;
using DmobileApp.ViewModels;
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
	public partial class ChatSms : ContentPage
	{
        private int _cust_no;
		public ChatSms(profile_data profile)
		{
			InitializeComponent ();
            Title = profile.CUST_NAME;
            _cust_no = profile.CUST_NO;
            BindingContext = new MainPageViewModel(profile.CUST_NO);
            //scrollList.ScrollToAsync(MessageControls, ScrollToPosition.End, true);
            var last = MessagesListView.ItemsSource.Cast<MessageViewModel>().LastOrDefault();
            MessagesListView.ScrollTo(last, ScrollToPosition.End, true);
           // MessagesListView.ScrollTo(MessagesListView.ItemsSource.Cast<Grid>().Count(), ScrollToPosition.End, true);
            //DependencyService.Get<IMessage>().longAlert("ดึงข้อมูลสำเร็จ");
        }
        private void MyListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            MessagesListView.SelectedItem = null;
        }

        private void MyListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            MessagesListView.SelectedItem = null;

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