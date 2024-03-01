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
    public partial class DetailRevenue : ContentPage
    {
        public DetailRevenue()
        {
            InitializeComponent();
        }

        public bool IsUpdate { get; set; } = false;
        private int _id { get; set; }
        public DetailRevenue(SQLite.Models.Revenue model)
        {
            InitializeComponent();
            this._id = model.Id;
            this.nameEntry.Text = model.Name;
            //this.typeEntry.Text = model.Type;
            this.countEntry.Text = model.Count.ToString();
            this.datePicker.Date = model.Date;
            this.IsUpdate = true;
        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            StringBuilder errorBuilder = null;
            if (nameEntry.Text == null || nameEntry.Text == "")
            {
                if (errorBuilder == null)
                {
                    errorBuilder = new StringBuilder("Поле \"Название\" не заполнено\n");
                }
                else
                {
                    errorBuilder.AppendLine("Поле \"Название\" не заполнено");
                }
            }
            //if (typeEntry.Text == null || typeEntry.Text == "")
            //{
            //    if (errorBuilder == null)
            //    {
            //        errorBuilder = new StringBuilder("Поле \"Источник\" не заполнено\n");
            //    }
            //    else
            //    {
            //        errorBuilder.AppendLine("Поле \"Источник\" не заполнено");
            //    }
            //}
            decimal count = 0;
            if (countEntry.Text == null || countEntry.Text == "")
            {
                if (errorBuilder == null)
                {
                    errorBuilder = new StringBuilder("Поле \"Cумма\" не заполнено\n");
                }
                else
                {
                    errorBuilder.AppendLine("Поле \"Сумма\" не заполнено");
                }
            }
            else
            {
                try
                {
                    count = Convert.ToDecimal(countEntry.Text);
                }
                catch (Exception)
                {
                    if (errorBuilder == null)
                    {
                        errorBuilder = new StringBuilder("В поле \"Сумма\" ошибка ввода\n");
                    }
                    else
                    {
                        errorBuilder.AppendLine("В поле \"Сумма\" ошибка ввода");
                    }
                }
            }
            if (errorBuilder != null)
            {
                await DisplayAlert("Ошибка", errorBuilder.ToString(), "Закрыть");
            }
            else
            {
                try
                {
                    var model = new SQLite.Models.Revenue() { Name = nameEntry.Text, Date = datePicker.Date, Count = count, /*Type = typeEntry.Text*/ };
                    if (IsUpdate)
                    {
                        model.Id = this._id;
                        SQLite.Repository.Instanse.Update(model);
                    }
                    else
                    {
                        SQLite.Repository.Instanse.Add(model);
                    }
                    await DisplayAlert("Сохранение", "Сохранение прошло успешно.", "Закрыть");
                    MessagingCenter.Send<Page>(this, "BackExpenses");
                    this.OnBackButtonPressed();
                }
                catch
                {
                    await DisplayAlert("Ошибка", "Что-то пошло не так", "Закрыть");
                }
            }
        }
        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            this.OnBackButtonPressed();
        }
    }
}