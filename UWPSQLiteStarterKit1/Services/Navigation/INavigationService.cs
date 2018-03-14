using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSQLiteStarterKit1.Services.Navigation
{
    /// <summary>
    /// Interface definition of a navigation service
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Registers a view associed to a specific name
        /// </summary>
        /// <param name="viewName">The view name</param>
        /// <param name="viewType">The view type</param>
        void RegisterPage(String viewName,
                          Type viewType);

        /// <summary>
        ///     Starts navigating to a specified page with optional parameters
        /// </summary>
        /// <param name="pageName">The page name to display</param>
        /// <param name="parameters">The page parameters</param>
        /// <param name="clearBackStack">Defines if back stack should be cleared</param>
        void NavigateTo(String pageName,
                        Dictionary<String, String> parameters = null,
                        Boolean clearBackStack = false);

        /// <summary>
        ///     Navigates back to the previous page
        /// </summary>
        /// <param name="depth">The requested back in back stack</param>
        void GoBack(Int32 depth = 1);

        /// <summary>
        ///     Cleans the navigation stack history
        /// </summary>
        void CleanHistory();

        /// <summary>
        ///     Focus on the main frame
        ///     Useful method for closing the keyboard for example...
        /// </summary>
        void FocusOnFrame();
    }
}
