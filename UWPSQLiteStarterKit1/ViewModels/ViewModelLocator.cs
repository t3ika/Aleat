
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Services;
using UWPSQLiteStarterKit1.Services.Interfaces;
using UWPSQLiteStarterKit1.Services.Navigation;

namespace UWPSQLiteStarterKit1.ViewModels
{
    /// <summary>
    /// Contains all statics references to the view models of the application
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initialize a new instance
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                //Register default services
                SimpleIoc.Default.Register<INavigationService, NavigationService>();

                // Register specific application services
                SimpleIoc.Default.Register<IDataService, DataService>();
                SimpleIoc.Default.Register<IAccessFoldersFilesService, AccessFoldersFilesService>();

            }


            SimpleIoc.Default.Register<MainViewModel>();
        }

        /// <summary>
        /// Get the main viewmodel
        /// </summary>
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }


        /// <summary>
        /// Clean the view models
        /// </summary>
        public static void Cleanup()
        {

        }
    }
}
