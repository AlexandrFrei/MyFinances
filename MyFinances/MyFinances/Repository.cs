using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace MyFinances.SQLite
{
    public class Repository
    {
        private const string DATABASE_NAME = "MyFinances.db";
        private static SQLiteConnection database { get; set; }
        private Repository()
        {
            database = new SQLiteConnection(DependencyService.Get<ISQLite>().GetDatabasePath(DATABASE_NAME));
            database.CreateTable<Models.Expenses>();
            database.CreateTable<Models.Revenue>();
        }
        private static Repository _instanse;
        public static Repository Instanse
        {
            get
            {
                if (_instanse == null)
                {
                    _instanse = new Repository();
                }
                return _instanse;
            }
        }
        public void Add<T>(T model)
        {
            database.Insert(model);
        }
        public void Remove<T>(T model)
        {
            database.Delete(model);
        }
        public void Update<T>(T model)
        {
            database.Update(model);
        }
        public List<T> GetAll<T>(DateTime from) where T: Models.Model, new()
        {
            return database.Table<T>().Where(x => x.Date >= from).OrderBy(x => x.Date).ToList();
        }
        public List<T> GetFromPeriod<T>(DateTime from, DateTime to) where T : Models.Model, new()
        {
            return database.Table<T>().Where(x => x.Date >= from && x.Date <= to).OrderBy(x => x.Date).ToList();
        }

    }
}
