using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace EleCho.WpfSuite.Media.Animation
{
    /// <summary>
    /// Delegate for creating animation timeline
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="from">From value</param>
    /// <param name="to">To value</param>
    /// <param name="duration">Animation duration</param>
    /// <param name="easingFunction">Animation easing function</param>
    /// <returns></returns>
    public delegate AnimationTimeline ValueAnimatorTimelineFactory<T>(T? from, T? to, Duration duration, IEasingFunction easingFunction);
}
