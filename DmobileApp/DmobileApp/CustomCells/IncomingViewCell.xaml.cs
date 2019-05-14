using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DmobileApp.CustomCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IncomingViewCell : ViewCell
    {
        public IncomingViewCell()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            Debug.WriteLine("PIPE => Image Tap");
            var img = sender as Image;
            //Navigation.PushAsync();
        }
    }
}