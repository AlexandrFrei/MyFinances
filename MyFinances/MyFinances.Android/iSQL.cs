using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.IO;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Dependency(typeof(MyFinances.Droid.SQLite))]
namespace MyFinances.Droid
{
    class SQLite : MyFinances.SQLite.ISQLite
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;

        }
    }
}