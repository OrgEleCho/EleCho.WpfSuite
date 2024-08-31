using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    /// <summary>
    /// ItemsControl Utilities
    /// </summary>
    public static class ItemsControlUtils
    {
        /// <summary>
        /// Remove items from <see cref="ItemsControl"/> that has a <see cref="ContentControl"/> container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Task TransitioningRemoveRangeAsync<T>(this ItemsControl container, IEnumerable<T> items)
            => TransitioningRemoveRangeAsync(container, items, true);

        /// <summary>
        /// Remove items from <see cref="ItemsControl"/> that has a <see cref="ContentControl"/> container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="items"></param>
        /// <param name="forwardTransition"></param>
        /// <returns></returns>
        public static Task TransitioningRemoveRangeAsync<T>(this ItemsControl container, IEnumerable<T> items, bool forwardTransition)
            => Task.WhenAll(items.Select(item => TransitioningRemoveAsync(container, item, forwardTransition)));

        /// <summary>
        /// Remove an item from <see cref="ItemsControl"/> that has a <see cref="ContentControl"/> container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Task TransitioningRemoveAsync<T>(this ItemsControl container, T item)
            => TransitioningRemoveAsync(container, item, true);

        /// <summary>
        /// Remove an item from <see cref="ItemsControl"/> that has a <see cref="ContentControl"/> container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="item"></param>
        /// <param name="forwardTransition"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static async Task TransitioningRemoveAsync<T>(this ItemsControl container, T item, bool forwardTransition)
        {
            IEnumerable items = container.Items;
            if (container.ItemsSource is not null)
            {
                items = container.ItemsSource;
            }

            if (items is not ICollection<T> itemsCollection)
            {
                throw new InvalidOperationException("Cannot remove item from 'ItemsControl', because it's 'ItemsSource' is not 'ICollection'");
            }

            var itemControl = container.ItemContainerGenerator.ContainerFromItem(item);

            if (itemControl is null)
            {
                return;
            }

            if (itemControl is System.Windows.Controls.ContentControl itemContentControl)
            {
                itemControl = VisualTreeHelper.GetChild(itemContentControl, 0);
            }

            if (itemControl is ContentPresenter itemContentPresenter)
            {
                itemControl = VisualTreeHelper.GetChild(itemContentPresenter, 0);
            }

            if (itemControl is EleCho.WpfSuite.Controls.ContentControl itemTransitioningContentControl)
            {
                itemTransitioningContentControl.SetContent(null, forwardTransition);
                await itemTransitioningContentControl.WaitForTransitionAsync();
                itemsCollection.Remove(item);
            }
        }
    }
}
