using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPSQLiteStarterKit1.Services.Navigation
{
    /// <summary>
    ///     Navigation service implementation
    /// </summary>
    public class NavigationService : INavigationService
    {
        #region Fields

        //Data
        private readonly IDictionary<String, Type> _registeredPages;
        private Frame _mainFrame;

        //Services


        #endregion

        #region Ctors

        /// <summary>
        ///     Initialize a new instance
        /// </summary>
        public NavigationService()
        {

            _registeredPages = new Dictionary<String, Type>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers a page associed to a specific name
        /// </summary>
        /// <param name="viewName">The view name</param>
        /// <param name="viewType">The view type</param>
        public void RegisterPage(String viewName,
                                 Type viewType)
        {
            if (_registeredPages.ContainsKey(viewName))
                return;

            _registeredPages.Add(viewName,
                                 viewType);


        }


        /// <summary>
        ///     Starts navigating to a specified page with optional parameters
        /// </summary>
        /// <param name="pageName">The page name to display</param>
        /// <param name="parameters">The page parameters</param>
        /// <param name="clearBackStack">Defines if back stack should be cleared</param>
        public async void NavigateTo(String pageName,
                               Dictionary<String, String> parameters = null,
                               Boolean clearBackStack = false)
        {
            if (EnsureMainFrame())
            {
                await DispatcherHelper.RunAsync(() =>
                {
                    String computedPageUrl = pageName;

                    if (parameters != null)
                    {
                        String[] serializedParametersArray = parameters.Select(p => String.Format("{0}={1}",
                                                                                                  p.Key,
                                                                                                  p.Value))
                                                                       .ToArray();

                        String serializedParametersQuery = String.Join("&",
                                                                       serializedParametersArray);

                        computedPageUrl = String.Format("{0}?{1}",
                                                        pageName,
                                                        serializedParametersQuery);
                    }

                    if (_registeredPages.ContainsKey(pageName))
                    {
                        Type viewType = _registeredPages[pageName];
                        _mainFrame.Navigate(viewType,
                                            parameters);



                        if (!clearBackStack)
                            return;

                        while (_mainFrame.CanGoBack)
                            _mainFrame.BackStack.RemoveAt(0);
                    }

                });
            }
        }

        /// <summary>
        ///     Navigates back to the previous page
        /// </summary>
        /// <param name="depth">The requested back in back stack</param>
        public async void GoBack(Int32 depth = 1)
        {
            if (depth <= 0)
                return;

            if (EnsureMainFrame() && _mainFrame.CanGoBack)
            {

                await DispatcherHelper.RunAsync(() =>
                {
                    if (depth > 1)
                    {
                        Int32 removeBackStackCount = depth - 1;
                        while (removeBackStackCount >= 1)
                        {
                            PageStackEntry entry = _mainFrame.BackStack.LastOrDefault();
                            if (entry != null)
                            {
                                _mainFrame.BackStack.Remove(entry);
                            }
                            removeBackStackCount--;
                        }
                    }

                    _mainFrame.GoBack();
                });
            }
        }

        /// <summary>
        ///     Cleans the navigation stack history
        /// </summary>
        public void CleanHistory()
        {
            if (!EnsureMainFrame())
                return;

            if (!_mainFrame.CanGoBack)
                return;



            while (_mainFrame.BackStack.Any())
            {

            }
        }

        /// <summary>
        ///     Focus on the main frame
        ///     Useful method for closing the keyboard for example...
        /// </summary>
        public void FocusOnFrame()
        {
            if (_mainFrame != null)
            {
                _mainFrame.Focus(FocusState.Pointer);
            }
        }

        /// <summary>
        ///     Teste si la frame principale de l'application est bien initialisée
        /// </summary>
        /// <returns>
        ///     <value>True</value>
        ///     si la frame est initialisée,
        ///     <value>False</value>
        ///     sinon
        /// </returns>
        private Boolean EnsureMainFrame()
        {
            if (_mainFrame == null)
            {
                _mainFrame = Window.Current.Content as Frame;
            }

            return _mainFrame != null;
        }

        #endregion
    }
}
