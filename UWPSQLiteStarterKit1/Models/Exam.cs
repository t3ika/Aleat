using Framework.Database.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSQLiteStarterKit1.Models
{
    [Table("Exam")]
    public class Exam
    {

        [PrimaryKey, Column("Id"), Indexed]
        public int Id { get; set; }

        [Column("Name")]
        public String Name { get; set; }

        [Column("Description")]
        public String Description { get; set; }

        [Column("ShortName")]
        public String ShortName { get; set; }

        [Column("Passed")]
        public int Passed { get; set; }

    }
}
