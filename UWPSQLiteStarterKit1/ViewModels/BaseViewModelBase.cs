using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Services.Navigation;
using Windows.UI.Xaml.Navigation;

namespace UWPSQLiteStarterKit1.ViewModels
{
    /// <summary>
    /// A base ViewModel class for Rte application
    /// </summary>
    public abstract class BaseViewModelBase : ViewModelBase, INavigationAware
    {
        #region Fields

        //Data
        private String _busyMessage;
        private Boolean _isBusy;
        private Boolean _isInitialized;



        #endregion

        #region Ctors

        /// <summary>
        /// Destructor
        /// </summary>
        ~BaseViewModelBase()
        {

        }

        #endregion

        #region Properties




        // totalCountdownTime is what sets how long the timer will run for
        public TimeSpan totalCountdownTime;

        /// <summary>
        /// Gets or sets if the view model is busy or not
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                Set(ref _isBusy,
                    value);

                OnIsBusyChanged();
            }
        }

        /// <summary>
        /// Gets or sets if the view model is intialized
        /// </summary>
        public Boolean IsInitialized
        {
            get
            {
                return _isInitialized;
            }

            set
            {
                Set(ref _isInitialized,
                    value);
            }
        }

        /// <summary>
        /// Gets or set the busy message
        /// </summary>
        public String BusyMessage
        {
            get
            {
                return _busyMessage;
            }

            set
            {
                Set(ref _busyMessage,
                    value);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Cleanup ViewModel resources. Override this method, clean up and then call base.Cleanup().
        /// </summary>
        public override void Cleanup()
        {
            base.Cleanup();
        }


        #endregion

        #region Handlers

        /// <summary>
        /// Called when IsBusy property value changed
        /// </summary>
        protected virtual void OnIsBusyChanged()
        {
        }

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if it is available.
        /// </summary>
        /// <param name="navigationParameter">The navigation parameter.</param>
        /// <param name="navigationMode">The navigation mode.</param>
        public virtual void OnNavigatedTo(Object navigationParameter,
                                      NavigationMode navigationMode)
        {

        }

        /// <summary>
        /// This method will be called when navigating away from a page. You can use this method to save your view model data in case of a suspension event.
        /// </summary>
        /// <param name="navigationParameter">The navigation parameter.</param>
        /// <param name="navigationMode">The navigation mode.</param>
        public virtual void OnNavigatedFrom(Object navigationParameter,
                                          NavigationMode navigationMode)
        {

        }

        #endregion
    }
}
