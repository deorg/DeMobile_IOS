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
	public partial class Fistpage : ContentPage
	{
		public Fistpage ()
		{
			InitializeComponent ();
		}

        private void btnGreet_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Greeting", $"Hello Mr.{txtName.Text}", "OK");
            Navigation.PushAsync(new Mainpage());

        }
        private void btnClear_Clicked(object sender, EventArgs e)
        {
            txtName.Text = "";
        }
    }
}