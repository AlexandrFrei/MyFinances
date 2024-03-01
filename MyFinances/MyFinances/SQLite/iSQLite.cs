using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinances.SQLite
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
