using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Services.Navigation;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWPSQLiteStarterKit1.Controls
{
    /// <summary>
    /// Defines a base application page
    /// </summary>
    public class BasePage : Page
    {
        #region Fields
        private List<Control> _visualStateAwareControls;

        /// <summary>
        /// Name of default visual state
        /// </summary>
        public static readonly string DefaultLayoutVisualState = "DefaultLayout";

        /// <summary>
        /// Name of portraite visual state
        /// </summary>
        public static readonly string PortraitLayoutVisualState = "PortraitLayout";

        /// <summary>
        /// Name of Minimal or narrow visual state
        /// </summary>
        public static readonly string MinimalLayoutVisualState = "MinimalLayout";

        /// <summary>
        /// Width of page that should trigger Minimal visual state
        /// </summary>
        public int MinimalLayoutWidth { get; set; }

        //Commands
        private RelayCommand _goBackCommand;

        #endregion

        #region Ctors
        /// <summary>
        /// Initialize a new instance
        /// </summary>
        public BasePage()
        {
            if (DesignMode.DesignModeEnabled)
                return;

            // When this page is part of the visual tree make two changes:
            // 1) Map application view state to visual state for the page
            // 2) Handle keyboard and mouse navigation requests
            this.Loaded += (sender,
                            e) =>
            {
                StartLayoutUpdates(sender, e);

                // Keyboard and mouse navigation only apply when occupying the entire window
                if (ActualHeight == Window.Current.Bounds.Height
                    && ActualWidth == Window.Current.Bounds.Width)
                {
                    // Listen to the window directly so focus isn't required
                    Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += OnCoreDispatcherAcceleratorKeyActivated;
                    Window.Current.CoreWindow.PointerPressed += OnCoreWindowPointerPressed;
                }
            };

            // Undo the same changes when the page is no longer visible
            this.Unloaded += (sender,
                              e) =>
            {
                //this.StopLayoutUpdates(sender, e);
                Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated -= OnCoreDispatcherAcceleratorKeyActivated;
                Window.Current.CoreWindow.PointerPressed -= OnCoreWindowPointerPressed;
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// <see cref="RelayCommand"/> used to bind to the back Button's Command property
        /// for navigating to the most recent item in back navigation history, if a Frame
        /// manages its own navigation history.
        /// 
        /// The <see cref="RelayCommand"/> is set up to use the virtual method <see cref="GoBack"/>
        /// as the Execute Action and <see cref="CanGoBack"/> for CanExecute.
        /// </summary>
        public RelayCommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    _goBackCommand = new RelayCommand(
                        () => GoBack(this,
                                     null),
                        CanGoBack);
                }
                return _goBackCommand;
            }
            set
            {
                _goBackCommand = value;
            }
        }

        #endregion

        #region Methods


        /// <summary>
        /// Invoked as an event handler to navigate backward in the page's associated
        /// <see cref="Frame"/> until it reaches the top of the navigation stack.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="eventArgs">Event data describing the conditions that led to the event.</param>
        protected virtual void GoHome(object sender,
                                      RoutedEventArgs eventArgs)
        {
            // Use the navigation frame to return to the topmost page
            if (this.Frame != null)
            {
                while (this.Frame.CanGoBack)
                    this.Frame.GoBack();
            }
        }

        /// <summary>
        /// Virtual method used by the <see cref="GoBackCommand"/> property
        /// to determine if the <see cref="Frame"/> can go back.
        /// </summary>
        /// <returns>
        /// true if the <see cref="Frame"/> has at least one entry 
        /// in the back navigation history.
        /// </returns>
        public virtual bool CanGoBack()
        {
            return this.Frame != null && this.Frame.CanGoBack;
        }

        /// <summary>
        /// Invoked as an event handler to navigate backward in the navigation stack
        /// associated with this page's <see cref="Frame"/>.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="eventArgs">Event data describing the conditions that led to the
        /// event.</param>
        protected virtual void GoBack(object sender,
                                      RoutedEventArgs eventArgs)
        {
            // Use the navigation frame to return to the previous page
            if (this.Frame != null && this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        /// <summary>
        /// Invoked as an event handler to navigate forward in the navigation stack
        /// associated with this page's <see cref="Frame"/>.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="eventArgs">Event data describing the conditions that led to the
        /// event.</param>
        protected virtual void GoForward(object sender,
                                         RoutedEventArgs eventArgs)
        {
            // Use the navigation frame to move to the next page
            if (this.Frame != null && this.Frame.CanGoForward)
                this.Frame.GoForward();
        }


        /// <summary>
        /// Invoked as an event handler, typically on the <see cref="FrameworkElement.Loaded"/>
        /// event of a <see cref="Control"/> within the page, to indicate that the sender should
        /// start receiving visual state management changes that correspond to application view
        /// state changes.
        /// </summary>
        /// <param name="sender">Instance of <see cref="Control"/> that supports visual state
        /// management corresponding to view states.</param>
        /// <param name="eventArgs">Event data that describes how the request was made.</param>
        /// <remarks>The current view state will immediately be used to set the corresponding
        /// visual state when layout updates are requested. A corresponding
        /// <see cref="FrameworkElement.Unloaded"/> event handler connected to
        /// <see cref="StopLayoutUpdates"/> is strongly encouraged. Instances of
        /// <see cref="VisualStateAwarePage"/> automatically invoke these handlers in their Loaded and
        /// Unloaded events.</remarks>
        /// <seealso cref="DetermineVisualState"/>
        /// <seealso cref="InvalidateVisualState"/>
        public void StartLayoutUpdates(object sender, RoutedEventArgs eventArgs)
        {
            var control = sender as Control;
            if (control == null) return;
            if (this._visualStateAwareControls == null)
            {
                // Start listening to view state changes when there are controls interested in updates
                Window.Current.SizeChanged += this.WindowSizeChanged;
                this._visualStateAwareControls = new List<Control>();
            }
            this._visualStateAwareControls.Add(control);

            // Set the initial visual state of the control
            VisualStateManager.GoToState(control, DetermineVisualState(this.ActualWidth, this.ActualHeight), false);
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState(e.Size.Width, e.Size.Height);
        }


        /// <summary>
        /// Invoked as an event handler, typically on the <see cref="FrameworkElement.Unloaded"/>
        /// event of a <see cref="Control"/>, to indicate that the sender should start receiving
        /// visual state management changes that correspond to application view state changes.
        /// </summary>
        /// <param name="sender">Instance of <see cref="Control"/> that supports visual state
        /// management corresponding to view states.</param>
        /// <param name="eventArgs">Event data that describes how the request was made.</param>
        /// <remarks>The current view state will immediately be used to set the corresponding
        /// visual state when layout updates are requested.</remarks>
        /// <seealso cref="StartLayoutUpdates"/>
        public void StopLayoutUpdates(object sender, RoutedEventArgs eventArgs)
        {
            var control = sender as Control;
            if (control == null || this._visualStateAwareControls == null) return;
            this._visualStateAwareControls.Remove(control);
            if (this._visualStateAwareControls.Count == 0)
            {
                // Stop listening to view state changes when no controls are interested in updates
                this._visualStateAwareControls = null;
                Window.Current.SizeChanged -= this.WindowSizeChanged;
            }
        }

        /// <summary>
        /// Translates <see cref="ApplicationViewState"/> values into strings for visual state
        /// management within the page. The default implementation uses the names of enum values.
        /// Subclasses may override this method to control the mapping scheme used.
        /// </summary>
        /// <returns>Visual state name used to drive the
        /// <see cref="VisualStateManager"/></returns>
        /// <seealso cref="InvalidateVisualState"/>
        protected virtual string DetermineVisualState(double width, double height)
        {
            if (width <= MinimalLayoutWidth)
            {
                return MinimalLayoutVisualState;
            }

            if (width < height)
            {
                return PortraitLayoutVisualState;
            }

            return DefaultLayoutVisualState;
        }

        /// <summary>
        /// Updates all controls that are listening for visual state changes with the correct
        /// visual state.
        /// </summary>
        /// <remarks>
        /// Typically used in conjunction with overriding <see cref="DetermineVisualState"/> to
        /// signal that a different value may be returned even though the view state has not
        /// changed.
        /// </remarks>
        public void InvalidateVisualState(double width, double height)
        {
            if (this._visualStateAwareControls != null)
            {
                string visualState = DetermineVisualState(width, height);
                foreach (var layoutAwareControl in this._visualStateAwareControls)
                {
                    VisualStateManager.GoToState(layoutAwareControl, visualState, false);
                }
            }
        }
        #endregion

        #region Handlers

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            INavigationAware vm = DataContext as INavigationAware;

            if (vm != null)
            {
                vm.OnNavigatedTo(e.Parameter, e.NavigationMode);
            }

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            INavigationAware vm = DataContext as INavigationAware;

            if (vm != null)
            {
                vm.OnNavigatedFrom(e.Parameter, e.NavigationMode);
            }

            base.OnNavigatedFrom(e);
        }

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //	INavigationAware vm = DataContext as INavigationAware;

        //	if (vm != null)
        //	{
        //		vm.OnNavigatingFrom(cancel =>
        //		{
        //			e.Cancel = cancel;
        //			base.OnNavigatingFrom(e);
        //		});
        //	}
        //	else
        //		base.OnNavigatingFrom(e);
        //}

        /// <summary>
        /// Invoked on every keystroke, including system keys such as Alt key combinations, when
        /// this page is active and occupies the entire window. Used to detect keyboard navigation
        /// between pages even when the page itself doesn't have focus.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="args">Event data describing the conditions that led to the event.</param>
        private void OnCoreDispatcherAcceleratorKeyActivated(CoreDispatcher sender,
                                                             AcceleratorKeyEventArgs args)
        {
            var virtualKey = args.VirtualKey;

            // Only investigate further when Left, Right, or the dedicated Previous or Next keys
            // are pressed
            if ((args.EventType == CoreAcceleratorKeyEventType.SystemKeyDown ||
                 args.EventType == CoreAcceleratorKeyEventType.KeyDown) &&
                (virtualKey == VirtualKey.Left || virtualKey == VirtualKey.Right ||
                 (int)virtualKey == 166 || (int)virtualKey == 167))
            {
                var coreWindow = Window.Current.CoreWindow;
                var downState = CoreVirtualKeyStates.Down;
                bool menuKey = (coreWindow.GetKeyState(VirtualKey.Menu) & downState) == downState;
                bool controlKey = (coreWindow.GetKeyState(VirtualKey.Control) & downState) == downState;
                bool shiftKey = (coreWindow.GetKeyState(VirtualKey.Shift) & downState) == downState;
                bool noModifiers = !menuKey && !controlKey && !shiftKey;
                bool onlyAlt = menuKey && !controlKey && !shiftKey;

                if (((int)virtualKey == 166 && noModifiers) ||
                    (virtualKey == VirtualKey.Left && onlyAlt))
                {
                    // When the previous key or Alt+Left are pressed navigate back
                    args.Handled = true;
                    this.GoBack(this,
                                new RoutedEventArgs());
                }
                else if (((int)virtualKey == 167 && noModifiers) ||
                         (virtualKey == VirtualKey.Right && onlyAlt))
                {
                    // When the next key or Alt+Right are pressed navigate forward
                    args.Handled = true;
                    this.GoForward(this,
                                   new RoutedEventArgs());
                }
            }
        }

        /// <summary>
        /// Invoked on every mouse click, touch screen tap, or equivalent interaction when this
        /// page is active and occupies the entire window. Used to detect browser-style next and
        /// previous mouse button clicks to navigate between pages.
        /// </summary>
        /// <param name="sender">Instance that triggered the event.</param>
        /// <param name="args">Event data describing the conditions that led to the event.</param>
        private void OnCoreWindowPointerPressed(CoreWindow sender,
                                                PointerEventArgs args)
        {
            var properties = args.CurrentPoint.Properties;

            // Ignore button chords with the left, right, and middle buttons
            if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed ||
                properties.IsMiddleButtonPressed)
                return;

            // If back or foward are pressed (but not both) navigate appropriately
            bool backPressed = properties.IsXButton1Pressed;
            bool forwardPressed = properties.IsXButton2Pressed;
            if (backPressed ^ forwardPressed)
            {
                args.Handled = true;
                if (backPressed)
                    this.GoBack(this,
                                new RoutedEventArgs());
                if (forwardPressed)
                    this.GoForward(this,
                                   new RoutedEventArgs());
            }
        }

        #endregion
    }
}
