using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Models;
using UWPSQLiteStarterKit1.SQLite;

namespace UWPSQLiteStarterKit1.Services.Domains
{
    /// <summary>
    /// Gestionnaire d'une base sqlite
    /// </summary>
    public class BaseSQLiteDatabaseDomain : SQLiteDatabaseDomain
    {

        #region Fields  
        //Entities
        private SQLiteTableEntity<Exam> _exam;


        #endregion


        #region Ctors

        /// <summary>
        /// Initialise une nouvelle instance    
        /// </summary>
        /// <param name="path">Chemin vers la base à charger</param>
        public BaseSQLiteDatabaseDomain(String path)
            : base(path)
        {
            Path = path;
        }

        #endregion

        #region Methods

        //public async Task Initialize()
        //{
        //    QuizzInfo = await Quizz.Items.FirstOrDefaultAsync();
        //}

        #endregion  

        #region Properties      

        /// <summary>
        /// Obtiens le chemin de la base
        /// </summary>
        public String Path
        {
            get;
            private set;
        }

        //public Quizz QuizzInfo
        //{
        //    get;
        //    private set;
        //}

        /// <summary>
        /// Obtiens l'entitée Exam
        /// </summary>
        public SQLiteTableEntity<Exam> Exam
        {
            get
            {
                return _exam ?? (_exam = CreateTableEntity<Exam>());
            }
        }

        #endregion


    }
}
