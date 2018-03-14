using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSQLiteStarterKit1.Services.Navigation;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.ApplicationSettings;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace UWPSQLiteStarterKit1.ViewModels
{
    /// <summary>
    /// Provides application behavior to supplement the default Application class.
    /// </summary>
    public abstract class ApplicationBase : Application
    {
        #region Fields
        private bool _isRestoringFromTermination;
        public SplashScreen SplashScreen;
        #endregion

        #region Ctors
        /// <summary>
        /// Initializes the singleton application object. This is the first line of authored code executed.
        /// </summary>
        protected ApplicationBase()
        {
            _isRestoringFromTermination = false;

            Suspending += OnSuspending;
            UnhandledException += OnApplicationBaseUnhandledException;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the navigation service.
        /// </summary>
        /// <value>
        /// The navigation service.
        /// </value>
        protected INavigationService NavigationService
        {
            get;
            set;
        }



        /// <summary>
        /// Factory for creating the ExtendedSplashScreen instance.

        public bool IsSuspending
        {
            get;
            private set;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize application IOC container
        /// </summary>
        public virtual void InitializeContainer()
        {
            //Set locator provider
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //Register default services
            SimpleIoc.Default.Register<INavigationService, NavigationService>();

            //Configure navigation service
            NavigationService = SimpleIoc.Default.GetInstance<INavigationService>();
        }

        /// <summary>
        /// Gets the Settings charm action items.
        /// </summary>
        /// <returns>The list of Setting charm action items that will populate the Settings pane.</returns>
        protected virtual IEnumerable<SettingsCommand> GetSettingsCommands()
        {
            return null;
        }

        /// <summary>
        /// Initializes the Frame and its content.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns>A task of a Frame that holds the app content.</returns>
        protected Frame InitializeFrameAsync(IActivatedEventArgs args)
        {
            var rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content, just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = CreateFrame(args);



                OnInitialize(args);

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            return rootFrame;
        }


        #endregion

        #region Handlers

        /// <summary>
        /// Invoked when the application is launched normally by the end user. Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            InitializeContainer();

            this.SplashScreen = args.SplashScreen;
            Frame rootFrame = InitializeFrameAsync(args);

            DispatcherHelper.Initialize();

            if (rootFrame != null && (!_isRestoringFromTermination))
            {
                InputPane.GetForCurrentView().Showing += new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(_OnInputPaneShowing);
                InputPane.GetForCurrentView().Hiding += new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(_OnInputPaneHiding);

                await OnLaunchApplication(args);
            }

            // Ensure the current window is active
            Window.Current.Activate();


        }

        private void _OnInputPaneShowing(InputPane sender, InputPaneVisibilityEventArgs eventArgs)
        {
            eventArgs.EnsuredFocusedElementInView = false;

            FrameworkElement focusedControl = FocusManager.GetFocusedElement() as FrameworkElement;

            if (focusedControl != null)
            {
                GeneralTransform transform = focusedControl.TransformToVisual(null);
                Point screenPoint = transform.TransformPoint(new Point(0, 0));

                double Bottom = screenPoint.Y + focusedControl.ActualHeight;

                double TopAdjust = Bottom - (eventArgs.OccludedRect.Top - 30);
                if (TopAdjust >= 0)
                    TopAdjust += 30;
                else
                    TopAdjust = 0;

                if (TopAdjust > 0)
                {
                    while (focusedControl.Parent != null && focusedControl.Parent is FrameworkElement)
                    {
                        if (focusedControl.Parent is Popup)
                        {
                            ((UIElement)focusedControl.Parent).RenderTransform = new TranslateTransform()
                            {
                                Y = -TopAdjust
                            };
                            break;
                        }
                        focusedControl = (FrameworkElement)focusedControl.Parent;
                    }
                }
            }
        }

        private void _OnInputPaneHiding(InputPane sender, InputPaneVisibilityEventArgs eventArgs)
        {
            eventArgs.EnsuredFocusedElementInView = false;
            FrameworkElement focusedControl = FocusManager.GetFocusedElement() as FrameworkElement;

            if (focusedControl != null)
            {
                while (focusedControl.Parent != null && focusedControl.Parent is FrameworkElement)
                {
                    if (focusedControl.Parent is Popup)
                    {
                        ((UIElement)focusedControl.Parent).RenderTransform = new TranslateTransform()
                        {
                            Y = 0
                        };
                        break;
                    }
                    focusedControl = (FrameworkElement)focusedControl.Parent;
                }
            }
        }

        /// <summary>
        /// Called when an unhandled exception occurs
        /// </summary>
        /// <param name="sender">The exeption sender</param>
        /// <param name="e">The exception args</param>
        private void OnApplicationBaseUnhandledException(object sender,
                                                           UnhandledExceptionEventArgs e)
        {
            e.Handled = HandleUnhandledException(e);
        }

        /// <summary>
        /// Handles application exceptions
        /// </summary>
        /// <param name="e">The exception args</param>
        /// <returns><value>true</value> if execption is handled, otherwise <value>false</value></returns>
        protected virtual Boolean HandleUnhandledException(UnhandledExceptionEventArgs e)
        {
            return false;
        }

        /// <summary>
        /// Creates the Frame.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        /// <returns>An ainstance of a Frame that holds the app content.</returns>
        protected abstract Frame CreateFrame(IActivatedEventArgs args);

        /// <summary>
        /// Override this method with logic that will be performed after the application is initialized. For example, navigating to the application's home page.
        /// </summary>
        /// <param name="args">The <see cref="LaunchActivatedEventArgs"/> instance containing the event data.</param>
        protected abstract Task OnLaunchApplication(LaunchActivatedEventArgs args);

        /// <summary>
        /// Override this method with the initialization logic of your application. Here you can initialize services, repositories, and so on.
        /// </summary>
        /// <param name="args">The <see cref="IActivatedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnInitialize(IActivatedEventArgs args)
        {
        }

        /// <summary>
        /// Invoked when application execution is being suspended. Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            IsSuspending = true;
            try
            {
                var deferral = e.SuspendingOperation.GetDeferral();

                //Bootstrap inform navigation service that app is suspending.
                //NavigationService.Suspending();

                // Save application state
                //await SessionStateService.SaveAsync();

                deferral.Complete();
            }
            finally
            {
                IsSuspending = false;
            }
        }

        #endregion
    }
}
