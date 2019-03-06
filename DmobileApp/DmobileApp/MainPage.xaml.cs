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
    public partial class Mainpage : TabbedPage
    {
        public Mainpage ()
        {
            InitializeComponent();
            var navigationPage = new NavigationPage(new Profile());
            navigationPage.Title = "Profile";
            Children.Add(navigationPage);
            navigationPage = new NavigationPage(new ListSms());
            navigationPage.Title = "SMS";
            Children.Add(navigationPage);
        }
    }
}