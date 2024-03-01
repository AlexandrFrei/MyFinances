using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyFinances
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistics : ContentPage
    {
        public Statistics()
        {
            InitializeComponent();
            this.fromDatePicker.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.toDatePicker.Date = DateTime.Now;
            this.fromDatePicker.DateSelected += DatePicker_DateSelected;
            this.toDatePicker.DateSelected += DatePicker_DateSelected;
            UpdateData();
        }

        private void UpdateData()
        {
            var expenses = SQLite.Repository.Instanse.GetFromPeriod<SQLite.Models.Expenses>(this.fromDatePicker.Date, this.toDatePicker.Date);
            decimal expensesCount = 0;
            foreach (var item in expenses)
            {
                expensesCount += item.Count;
            }
            var revenues = SQLite.Repository.Instanse.GetFromPeriod<SQLite.Models.Revenue>(this.fromDatePicker.Date, this.toDatePicker.Date);
            decimal revenuesCount = 0;
            foreach (var item in revenues)
            {
                revenuesCount += item.Count;
            }
            var result = (expensesCount - revenuesCount) * (-1);
            revenueLabel.Text = "Доходы: " + revenuesCount + " руб.";
            expensesLabel.Text = "Расходы: " + expensesCount + " руб.";
            resultLabel.Text = "Итог: " + result + " руб.";
        }

        private void DatePicker_DateSelected(object sender, EventArgs e)
        {
            UpdateData();
        }

    }
}