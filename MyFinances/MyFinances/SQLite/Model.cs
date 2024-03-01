using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MyFinances.SQLite.Models
{
    public class Model
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public decimal Count { get; set; }

    }

    [Table("Expenses")]
    public class Expenses : Model
    {
        //public string Type { get; set; }
    }

    [Table("Revenue")]
    public class Revenue : Model
    {
       // public string Type { get; set; }
    }
}
