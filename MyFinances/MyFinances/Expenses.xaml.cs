using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyFinances
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Expenses : ContentPage
    {
        public ObservableCollection<SQLite.Models.Expenses> Models { get; set; }
        public Expenses()
        {
            InitializeComponent();
            this.Models = new ObservableCollection<SQLite.Models.Expenses>(SQLite.Repository.Instanse.GetAll<SQLite.Models.Expenses>(DateTime.Now));
            this.fromDatePicker.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.toDatePicker.Date = DateTime.Now;
            this.fromDatePicker.DateSelected += DatePicker_DateSelected;
            this.toDatePicker.DateSelected += DatePicker_DateSelected;
            
            this.Models = new ObservableCollection<SQLite.Models.Expenses>(SQLite.Repository.Instanse.GetFromPeriod<SQLite.Models.Expenses>(this.fromDatePicker.Date,this.toDatePicker.Date));
            this.BindingContext = this;
            MessagingCenter.Subscribe<Page>(this, "BackExpenses", (sender) => { UpdateData(); });
        }
        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as SQLite.Models.Expenses;
            ((ListView)sender).SelectedItem = null;
            switch (await DisplayActionSheet("Действия", "Отмена", null, "Изменить", "Удалить"))
            {
                case "Изменить":
                    await Navigation.PushModalAsync(new DetailExpenses(item));
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
            var expenses = SQLite.Repository.Instanse.GetFromPeriod<SQLite.Models.Expenses>(this.fromDatePicker.Date, this.toDatePicker.Date);
            this.Models.Clear();
            foreach (var item in expenses)
            {
               this.Models.Add(item);
            }
        }
        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new DetailExpenses());
        }
        private void DatePicker_DateSelected(object sender, EventArgs e)
        {
            UpdateData();
        }

    }
}