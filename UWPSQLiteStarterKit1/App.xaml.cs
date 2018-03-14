using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Constants;
using UWPSQLiteStarterKit1.Services;
using UWPSQLiteStarterKit1.Services.Interfaces;
using UWPSQLiteStarterKit1.ViewModels;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace UWPSQLiteStarterKit1
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : ApplicationBase
    {
        /// <summary>
        /// Initialize application IOC container
        /// </summary>
        public override void InitializeContainer()
        {
            base.InitializeContainer();

            RegistreInstances();
        }

        /// <summary>
        /// Creates the Frame.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns>An ainstance of a Frame that holds the app content.</returns>
        protected override Frame CreateFrame(IActivatedEventArgs args)
        {
            // Create a Frame to act as the navigation context and navigate to the first page
            return new Frame
            {
                CacheSize = 1,
                Background = new SolidColorBrush(Colors.White)

            };
        }

        /// <summary>
        /// Override this method with logic that will be performed after the application is initialized. For example, navigating to the application's home page.
        /// </summary>
        /// <param name="args">The <see cref="LaunchActivatedEventArgs"/> instance containing the event data.</param>
        protected override Task OnLaunchApplication(LaunchActivatedEventArgs args)
        {
            NavigationService.NavigateTo(Pages.MainPage);

            Resuming += App_Resuming;

            return Task.FromResult<object>(null);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Resuming(object sender, object e)
        {
            NavigationService.NavigateTo(Pages.MainPage);
            RegistreInstances();
        }

        private void RegistreInstances()
        {
            //Register ViewModels
            SimpleIoc.Default.Register<MainViewModel>();

            //Register services
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<IAccessFoldersFilesService, AccessFoldersFilesService>();


            //Configure navigation service and register known pages
            NavigationService.RegisterPage(Pages.MainPage,
                                           typeof(MainPage));


        }

    }
}
