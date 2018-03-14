using Framework.Database.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSQLiteStarterKit1.SQLite
{
    /// <summary>
    /// Generic SQLite table entity manager
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public class SQLiteTableEntity<T> where T : new()
    {
        #region Fields
        private readonly SQLiteAsyncConnection _database;

        public delegate void SQLiteTableEntityHandler(Type entityType);
        public event SQLiteTableEntityHandler OnBaseChanged;
        #endregion

        #region Ctors
        /// <summary>
        /// Initialize a new instance
        /// </summary>
        /// <param name="database">The database connection</param>
        public SQLiteTableEntity(SQLiteAsyncConnection database)
        {
            _database = database;
        }
        #endregion

        /// <summary>
        /// Gets all table items
        /// </summary>
        public AsyncTableQuery<T> Items
        {
            get
            {
                return _database.Table<T>();
            }
        }

        /// <summary>
        /// Inserts the given object and retrieves its auto incremented primary key if it has one.
        /// </summary>
        /// <param name="entity"> The object to insert. </param>
        /// <returns>
        /// The number of rows added to the table.
        /// </returns>
        public async Task<Int32> InsertAsync(T entity)
        {
            int actionReturn = await _database.InsertAsync(entity);
            NotifyChange();
            return actionReturn;
        }

        /// <summary>
        /// Updates all of the columns of a table using the specified object except for its primary key. The object is required to have a primary key.
        /// </summary>
        /// <param name="entity"> The object to update. It must have a primary key designated using the PrimaryKeyAttribute. </param>
        /// <returns>
        /// The number of rows updated.
        /// </returns>
        public async Task<Int32> Update(T entity)
        {
            int actionReturn = await _database.UpdateAsync(entity);
            NotifyChange();
            return actionReturn;
        }

        /// <summary>
        /// Deletes the given object from the database using its primary key.
        /// </summary>
        /// <param name="entity"> The object to delete. It must have a primary key designated using the PrimaryKeyAttribute.</param>
        /// <returns>
        /// The number of rows deleted.
        /// </returns>
        public async Task<Int32> Delete(T entity)
        {
            return await _database.DeleteAsync(entity);
        }

        /// <summary>
        /// Notify when base change
        /// </summary>
        private void NotifyChange()
        {
            if (OnBaseChanged != null)
                OnBaseChanged(typeof(T));
        }
    }
}
