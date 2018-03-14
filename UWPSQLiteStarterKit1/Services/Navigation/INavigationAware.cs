using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace UWPSQLiteStarterKit1.Services.Navigation
{
    /// <summary>
    /// The INavigationAware interface should be used for view models that require persisting and loading state due to suspend/resume events.
    /// The Rte.Vip.ViewModels.SqliViewModelBase base class implements this interface, therefore every view model that inherits from it
    /// will support the navigation aware methods. 
    /// </summary>
    public interface INavigationAware
    {
        /*/// <summary>
		/// Called when navigation is performed to a page. You can use this method to load state if it is available.
		/// </summary>
		/// <param name="navigationParameter">The navigation parameter.</param>
		/// <param name="navigationMode">The navigation mode.</param>
		/// <param name="viewModelState">The state of the view model.</param>
		void OnNavigatedTo(Object navigationParameter, NavigationMode navigationMode, Dictionary<String, Object> viewModelState);

		/// <summary>
		/// This method will be called when navigating away from a page. You can use this method to save your view model data in case of a suspension event.
		/// </summary>
		/// <param name="viewModelState">The state of the view model.</param>
		/// <param name="suspending">If set to <c>true</c> you are navigating away from this view model due to a suspension event.</param>
		void OnNavigatedFrom(Dictionary<String, Object> viewModelState, Boolean suspending);*/

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if it is available.
        /// </summary>
        /// <param name="navigationParameter">The navigation parameter.</param>
        /// <param name="navigationMode">The navigation mode.</param>
        void OnNavigatedTo(Object navigationParameter, NavigationMode navigationMode);

        /// <summary>
        /// This method will be called when navigating away from a page. You can use this method to save your view model data in case of a suspension event.
        /// </summary>
        /// <param name="navigationParameter">The navigation parameter.</param>
        /// <param name="navigationMode">The navigation mode.</param>
        void OnNavigatedFrom(Object navigationParameter, NavigationMode navigationMode);
    }
}
