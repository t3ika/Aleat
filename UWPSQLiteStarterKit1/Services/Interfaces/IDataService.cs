using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Models;

namespace UWPSQLiteStarterKit1.Services.Interfaces
{
    public interface IDataService
    {
        /// <summary>
        /// Initialise databases
        /// </summary>
        /// <returns></returns>
        Task LoadDatabases();


        /// <summary>
        /// Get all exams
        /// </summary>
        /// <returns> All exams</returns>
        Task<List<Exam>> GetExamsAsync();
    }
}
