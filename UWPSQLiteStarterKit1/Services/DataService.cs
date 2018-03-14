using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Models;
using UWPSQLiteStarterKit1.Services.Domains;
using UWPSQLiteStarterKit1.Services.Interfaces;

namespace UWPSQLiteStarterKit1.Services
{
    /// <summary>
    /// Access to database services
    /// </summary>
    /// 
    public class DataService : IDataService
    {

        #region Fields

        //Datas
        private Dictionary<String, BaseSQLiteDatabaseDomain> _domains;
        private String basePath = string.Empty;
        private string _baseId = "1";


        //Services
        private readonly IAccessFoldersFilesService _accessFoldersFilesService;

        #endregion

        #region Ctors

        public DataService(IAccessFoldersFilesService accessFoldersFilesService)
        {
            _accessFoldersFilesService = accessFoldersFilesService;
            _domains = new Dictionary<String, BaseSQLiteDatabaseDomain>();
            _domains.Clear();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initialise databases
        /// </summary>
        /// <returns></returns>
        public async Task LoadDatabases()
        {

            if (_domains.Count == 0)
            {
                List<string> DatabasesPath = await _accessFoldersFilesService.ListBasesPath();
                foreach (string basePath in DatabasesPath)
                {
                    BaseSQLiteDatabaseDomain domain = new BaseSQLiteDatabaseDomain(basePath);
                    _domains.Add(_baseId, domain);
                }
            }

        }

        /// <summary>
        /// Get all exams
        /// </summary>
        /// <returns> All exams</returns>
        public async Task<List<Exam>> GetExamsAsync()
        {
            return await _domains[_baseId.ToString()].Exam.Items.OrderBy(c => c.Name).ToListAsync();

        }
        #endregion

    }
}
