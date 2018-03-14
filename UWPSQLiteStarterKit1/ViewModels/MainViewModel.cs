using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Constants;
using UWPSQLiteStarterKit1.Models;
using UWPSQLiteStarterKit1.Services.Interfaces;
using UWPSQLiteStarterKit1.Services.Navigation;
using Windows.UI.Xaml.Navigation;

namespace UWPSQLiteStarterKit1.ViewModels
{
    /// <summary>
    /// Main application ViewModel
    /// </summary>
    public class MainViewModel : BaseViewModelBase
    {
        #region Fields

        //Datas
        private ObservableCollection<Exam> _exams;

        //Commands
        private RelayCommand _executeWithNoParameterCommand;
        private RelayCommand _executeAsyncWithNoParameterCommand;
        private RelayCommand _navigationWithNoParameterCommand;
        private RelayCommand<Exam> _accessToExamCommand;
        //Services
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;


        #endregion

        #region Ctors
        /// <summary>
        /// Initialize a new instance
        /// </summary>
        public MainViewModel(INavigationService navigationService,
            IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Get the ExecuteWithNoParameterCommand
        /// </summary>
        public RelayCommand ExecuteWithNoParameterCommand
        {
            get
            {
                return _executeWithNoParameterCommand ?? (_executeWithNoParameterCommand = new RelayCommand(ExecuteWithNoParameter));

            }
        }


        /// <summary>
        /// Get the ExecuteAsyncWithNoParameterCommand
        /// </summary>
        public RelayCommand ExecuteAsyncWithNoParameterCommand
        {
            get
            {
                return _executeAsyncWithNoParameterCommand ?? (_executeAsyncWithNoParameterCommand = new RelayCommand(async () => await ExecuteAsyncWithNoParameter()));

            }
        }

        /// <summary>
        /// Get the AccessToExamCommand
        /// </summary>
        public RelayCommand<Exam> AccessToExamCommand
        {
            get
            {
                return _accessToExamCommand ?? (_accessToExamCommand = new RelayCommand<Exam>(AccessToExam));

            }
        }

        /// <summary>
        /// Get the NavigationWithNoParameterCommand
        /// </summary>
        public RelayCommand NavigationWithNoParameterCommand
        {
            get
            {
                return _navigationWithNoParameterCommand ?? (_navigationWithNoParameterCommand = new RelayCommand(NavigationWithNoParameter));

            }
        }


        #endregion

        #region Properties

        /// <summary>
        /// Get the list exams
        /// </summary>
        public ObservableCollection<Exam> Exams
        {
            get
            {
                return _exams ?? (_exams = new ObservableCollection<Exam>());
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the View Model
        /// </summary>
        /// <returns></returns>
        private async Task Initialize()
        {

            IsBusy = true;

            List<Exam> ExamLst = new List<Exam>();

            await _dataService.LoadDatabases();

            ExamLst = await _dataService.GetExamsAsync();

            if (Exams != null && Exams.Count > 0)
            {
                Exams.Clear();
            }

            foreach (Exam exam in ExamLst)
            {
                Exams.Add(exam);
            }

            IsBusy = false;

        }

        /// <summary>
        /// Contains the command execution
        /// </summary>
        private void ExecuteWithNoParameter()
        {
            //Put your code here
        }


        /// <summary>
        /// Contains the asynchronous command with no parameter
        /// </summary>
        /// <returns></returns>
        private async Task ExecuteAsyncWithNoParameter()
        {
            await Initialize();
        }

        /// <summary>
        /// Contains the command for navigating to the details of an exam
        /// </summary>
        /// <param name="exam"></param>
        private void AccessToExam(Exam exam)
        {
            if (exam != null)
            {

                Dictionary<String, String> parameters = new Dictionary<String, String>();

                parameters.Add("Id", exam.Id.ToString());
                parameters.Add("Name", exam.Name);
                parameters.Add("Description", exam.Description);

                _navigationService.NavigateTo(Pages.ExamDetailsPage, parameters);

            }
        }


        /// <summary>
        /// Contains the command for navigation with no parameter
        /// </summary>
        private void NavigationWithNoParameter()
        {

            _navigationService.NavigateTo(Pages.ExamDetailsPage);

        }



        #endregion

        #region Handlers

        /// <summary>
        /// On Navigated To
        /// </summary>
        /// <param name="navigationParameter"></param>
        /// <param name="navigationMode"></param>
        public async override void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode)
        {

            await Initialize();
        }

        #endregion
    }
}
