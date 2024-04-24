using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace EleCho.WpfSuite
{
    public static class ItemsControlUtils
    {
        public static Task TransitioningRemoveRangeAsync<T>(this ItemsControl container, IEnumerable<T> items)
            => TransitioningRemoveRangeAsync(container, items, true);

        public static Task TransitioningRemoveRangeAsync<T>(this ItemsControl container, IEnumerable<T> items, bool forwardTransition)
            => Task.WhenAll(items.Select(item => TransitioningRemoveAsync(container, item, forwardTransition)));

        public static Task TransitioningRemoveAsync<T>(this ItemsControl container, T item)
            => TransitioningRemoveAsync(container, item, true);

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

            if (itemControl is ContentControl itemContentControl)
            {
                itemControl = VisualTreeHelper.GetChild(itemContentControl, 0);
            }

            if (itemControl is ContentPresenter itemContentPresenter)
            {
                itemControl = VisualTreeHelper.GetChild(itemContentPresenter, 0);
            }

            if (itemControl is TransitioningContentControl itemTransitioningContentControl)
            {
                itemTransitioningContentControl.SetContent(null, forwardTransition);
                await itemTransitioningContentControl.WaitForTransitionAsync();
                itemsCollection.Remove(item);
            }
        }
    }
}
