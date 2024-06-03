using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace EleCho.WpfSuite
{
    public static class TaskExtensions
    {
        public static Task WaitForCompleted(this Timeline animation)
        {
#if NETCOREAPP
            var taskCompletionSource = new TaskCompletionSource();
#else
            var taskCompletionSource = new TaskCompletionSource<bool>();
#endif

            var eventHandler = default(EventHandler);

            eventHandler = (s, e) =>
            {
#if NETCOREAPP
                taskCompletionSource.SetResult();
#else
                taskCompletionSource.SetResult(true);
#endif
                animation.Completed -= eventHandler;
            };

            animation.Completed += eventHandler;

            return taskCompletionSource.Task;
        }
    }
}
