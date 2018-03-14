using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPSQLiteStarterKit1.Controls
{
    /// <summary>
    ///     Custom control used to display a busy indication
    /// </summary>
    [TemplateVisualState(Name = "IsBusyState", GroupName = "BusyStatesGroup")]
    [TemplateVisualState(Name = "NormalState", GroupName = "BusyStatesGroup")]
    public class BusyIndicator : Control
    {
        #region Fields

        //Data
        private Boolean _isLoaded;

        #endregion

        #region DPs

        /// <summary>
        ///     DP definition for BusyProgressValue property
        /// </summary>
        public static readonly DependencyProperty ProgressValueProperty = DependencyProperty.Register("ProgressValue",
                                                                                                      typeof(Double),
                                                                                                      typeof(BusyIndicator),
                                                                                                      new PropertyMetadata(0.0));

        /// <summary>
        ///     DP definition for BusyProgressValue property
        /// </summary>
        public static readonly DependencyProperty BusyMessageProperty = DependencyProperty.Register("BusyMessage",
                                                                                                    typeof(String),
                                                                                                    typeof(BusyIndicator),
                                                                                                    new PropertyMetadata(String.Empty));

        /// <summary>
        ///     DP definition for BusyProgressValue property
        /// </summary>
        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy",
                                                                                               typeof(Boolean),
                                                                                               typeof(BusyIndicator),
                                                                                               new PropertyMetadata(false,
                                                                                                                    OnIsBusyPropertyChanged));

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialize a new instance of <see cref="BusyIndicator" />
        /// </summary>
        public BusyIndicator()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the current progress value
        /// </summary>
        public Double ProgressValue
        {
            get
            {
                return (Double)GetValue(ProgressValueProperty);
            }
            set
            {
                SetValue(ProgressValueProperty,
                         value);
            }
        }

        /// <summary>
        ///     Gets or sets if we are busy or not
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return (Boolean)GetValue(IsBusyProperty);
            }
            set
            {
                SetValue(IsBusyProperty,
                         value);
            }
        }

        /// <summary>
        ///     Gets or sets the current busy message to display
        /// </summary>
        public String BusyMessage
        {
            get
            {
                return (String)GetValue(BusyMessageProperty);
            }
            set
            {
                SetValue(BusyMessageProperty,
                         value);
            }
        }

        #endregion

        #region Handlers

        private static void OnIsBusyPropertyChanged(DependencyObject d,
                                                    DependencyPropertyChangedEventArgs e)
        {
            var indicator = d as BusyIndicator;

            if ((indicator != null) && (indicator._isLoaded))
            {
                VisualStateManager.GoToState(indicator,
                                             indicator.IsBusy
                                                 ? "IsBusyState"
                                                 : "NormalState",
                                             true);
            }
        }

        private void OnLoaded(object sender,
                              RoutedEventArgs e)
        {
            _isLoaded = true;
            VisualStateManager.GoToState(this,
                                         IsBusy
                                             ? "IsBusyState"
                                             : "NormalState",
                                         true);
        }

        private void OnUnloaded(object sender,
                                RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this,
                                         "NormalState",
                                         false);
        }

        #endregion
    }
}
