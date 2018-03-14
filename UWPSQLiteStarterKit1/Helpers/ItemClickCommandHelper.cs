using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPSQLiteStarterKit1.Helpers
{
    /// <summary>
    /// Class allowing command to be called when item is clicked on a <see cref="ListViewBase"/> component
    /// </summary>
    public static class ItemClickCommandHelper
    {
        /// <summary>
        /// Attached property definition Command
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command",
                                                                                                        typeof(ICommand),
                                                                                                        typeof(ItemClickCommandHelper),
                                                                                                        new PropertyMetadata(null,
                                                                                                                             OnCommandPropertyChanged));

        /// <summary>
        /// Sets command value
        /// </summary>
        /// <param name="d">The attached dependency object</param>
        /// <param name="value">The new command value</param>
        public static void SetCommand(DependencyObject d,
                                      ICommand value)
        {
            d.SetValue(CommandProperty,
                       value);
        }

        /// <summary>
        /// Gets the command value
        /// </summary>
        /// <param name="d">The attached property</param>
        /// <returns>The attached command</returns>
        public static ICommand GetCommand(DependencyObject d)
        {
            return (ICommand)d.GetValue(CommandProperty);
        }

        /// <summary>
        /// Called when command property changed
        /// </summary>
        /// <param name="d">The dependency object</param>
        /// <param name="e">The change event args</param>
        private static void OnCommandPropertyChanged(DependencyObject d,
                                                     DependencyPropertyChangedEventArgs e)
        {
            ListViewBase control = d as ListViewBase;

            if (control != null)
                control.ItemClick += OnItemClick;
        }

        /// <summary>
        /// Called when an item is clicked on the <see cref="ListViewBase"/>
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The item click event arg</param>
        private static void OnItemClick(object sender,
                                        ItemClickEventArgs e)
        {
            ListViewBase control = sender as ListViewBase;
            ICommand command = GetCommand(control);

            if (command != null && command.CanExecute(e.ClickedItem))
                command.Execute(e.ClickedItem);
        }
    }
}
