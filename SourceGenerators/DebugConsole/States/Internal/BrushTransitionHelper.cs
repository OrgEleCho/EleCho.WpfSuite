using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace EleCho.WpfSuite.Controls.States.Internal
{
    internal class BrushTransitionHelper : DependencyObject
    {
        private readonly Brush? _from;
        private readonly Brush? _to;
        private readonly DependencyObject _target;
        private readonly DependencyProperty _targetProperty;

        private Brush? _cache;

        public BrushTransitionHelper(Brush? from, Brush? to, DependencyObject target, DependencyProperty targetProperty)
        {
            this._from = from;
            this._to = to;
            this._target = target;
            this._targetProperty = targetProperty;
        }

        public double Progress
        {
            get { return (double)GetValue(ProgressProperty); }
            set { SetValue(ProgressProperty, value); }
        }

        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(BrushTransitionHelper), new PropertyMetadata(0.0, propertyChangedCallback: OnProgressChanged));

        private static void OnProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
