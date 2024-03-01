using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace MyFinances
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Revenue : ContentPage
    {
        public ObservableCollection<SQLite.Models.Revenue> Models { get; set; }
        public Revenue()
        {
            InitializeComponent();
            this.Models = new ObservableCollection<SQLite.Models.Revenue>(SQLite.Repository.Instanse.GetAll<SQLite.Models.Revenue>(DateTime.Now));
            this.fromDatePicker.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.toDatePicker.Date = DateTime.Now;
            this.fromDatePicker.DateSelected += DatePicker_DateSelected;
            this.toDatePicker.DateSelected += DatePicker_DateSelected;

            this.Models = new ObservableCollection<SQLite.Models.Revenue>(SQLite.Repository.Instanse.GetFromPeriod<SQLite.Models.Revenue>(this.fromDatePicker.Date, this.toDatePicker.Date));
            this.BindingContext = this;
            MessagingCenter.Subscribe<Page>(this, "BackExpenses", (sender) => { UpdateData(); });
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as SQLite.Models.Revenue;
            ((ListView)sender).SelectedItem = null;
            switch (await DisplayActionSheet("Действия", "Отмена", null, "Изменить", "Удалить"))
            {
                case "Изменить":
                    await Navigation.PushModalAsync(new DetailRevenue(item));
                    break;
                case "Удалить":
                    SQLite.Repository.Instanse.Remove(item);
                    UpdateData();
                    break;
            }
        }

        private void ListView_Refreshing(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var expenses = SQLite.Repository.Instanse.GetFromPeriod<SQLite.Models.Revenue>(this.fromDatePicker.Date, this.toDatePicker.Date);
            this.Models.Clear();
            foreach (var item in expenses)
            {
                this.Models.Add(item);
            }
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new DetailRevenue());
        }
        private void DatePicker_DateSelected(object sender, EventArgs e)
        {
            UpdateData();
        }
    }
}