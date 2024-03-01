using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MyFinances
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            Detail = new NavigationPage(new Statistics())
            {
                BarBackgroundColor = Color.FromHex("#008000")
            };
            IsPresented = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Revenue())
            {
                BarBackgroundColor = Color.FromHex("#008000")
            };

            IsPresented = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Expenses())
            {
                BarBackgroundColor = Color.FromHex("#008000")
            };

            IsPresented = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Statistics())
            {
                BarBackgroundColor = Color.FromHex("#008000")
            };

            IsPresented = false;
        }
    }
}
