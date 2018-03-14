using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSQLiteStarterKit1.Services.Interfaces
{
    /// <summary>
    /// Access to the folders
    /// </summary>
    public interface IAccessFoldersFilesService
    {
        /// <summary>
        /// Get the  database in the project
        /// </summary>
        /// <returns>The path of the database</returns>
        Task<List<string>> ListBasesPath();
    }
}
