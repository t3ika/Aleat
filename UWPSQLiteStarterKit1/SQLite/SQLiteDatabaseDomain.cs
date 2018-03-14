using Framework.Database.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSQLiteStarterKit1.SQLite
{
    /// <summary>
    /// Generic SQLite database manager
    /// </summary>
    public class SQLiteDatabaseDomain
    {
        #region Fields
        protected SQLiteAsyncConnection _database;
        #endregion

        #region Ctors
        /// <summary>
        /// Initialize a new instance
        /// </summary>
        /// <param name="path">The database path</param>
        protected SQLiteDatabaseDomain(String path)
        {
            LoadDatabase(path);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Clears all resources
        /// </summary>
        public virtual void Cleanup()
        {
            SQLiteConnectionPool.Shared.Reset();
            _database = null;
        }

        /// <summary>
        /// Drops the database
        /// </summary>
        public virtual void DropDatabase()
        {

        }

        public async Task<int> ExecuteRequest(string request, params string[] parameters)
        {
            string requestString = String.Format(request, parameters.ToArray());

            int result = await Connection.ExecuteAsync(requestString);

#warning TPB : Permet de notifier que la base à changé dans le cas ou la requête fait de la modification. Code a déplacer vers des entité personnalisés
            await BaseChanged();

            return result;
        }

        /// <summary>
        /// Loads databse
        /// </summary>
        /// <param name="path">The database path</param>
        private void LoadDatabase(String path)
        {
            _database = new SQLiteAsyncConnection(path);
        }

        /// <summary>
        /// Creates a table entity
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>An table entity instance</returns>
        protected SQLiteTableEntity<T> CreateTableEntity<T>() where T : new()
        {
            SQLiteTableEntity<T> entity = new SQLiteTableEntity<T>(_database);
            entity.OnBaseChanged += entity_OnBaseChanged;
            return entity;
        }

#pragma warning disable
        /// <summary>
        /// Drops the database
        /// </summary>
        public virtual async Task BaseChanged()
        {

        }
#pragma warning restore
        #endregion

        #region Properties
        /// <summary>
        /// Gets the database connection
        /// </summary>
        protected SQLiteAsyncConnection Connection
        {
            get
            {
                return _database;
            }
        }

        #endregion

        #region Hander

        /// <summary>
        /// Event for detect baseChange
        /// </summary>
        /// <param name="dataBase"></param>
        private async void entity_OnBaseChanged(Type dataBase)
        {
            await BaseChanged();
        }
        #endregion
    }
}
