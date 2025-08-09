﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace EleCho.WpfSuite.Input
{
    /// <summary>
    /// Stylus simulated touch device
    /// </summary>
    public class StylusTouchDevice : TouchDevice
    {
        #region Class Members

        private static StylusTouchDevice? _device;
        private static UIElement? _currentStylusUIElement;
        private static bool _stylusMoved;
        private static Point _stylusDownPosition;

        /// <summary>
        /// Touch position
        /// </summary>
        public Point Position { get; set; }

        #endregion

        #region Public Static Methods

        private static void RegisterEvents(UIElement root)
        {
            root.PreviewStylusDown += StylusDown;
            root.PreviewStylusMove += StylusMove;
            root.PreviewStylusUp += StylusUp;
        }

        private static void UnregisterEvents(UIElement root)
        {
            root.PreviewStylusDown -= StylusDown;
            root.PreviewStylusMove -= StylusMove;
            root.PreviewStylusUp -= StylusUp;
        }

        #endregion

        #region Private Static Methods


        private static double GetPointDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private static void StylusDown(object sender, StylusDownEventArgs e)
        {
            if (e.OriginalSource is DependencyObject originSource &&
                !GetSimulate(originSource))
            {
                return;
            }

            var currentPosition = e.GetPosition(null);

            if (_device != null &&
                _device.IsActive)
            {
                _device.ReportUp();
                _device.Deactivate();
                _device = null;
            }

            _device = new StylusTouchDevice(e.Device.GetHashCode());
            _device.SetActiveSource(e.Device.ActiveSource);
            _device.Position = currentPosition;
            _device.Activate();
            _device.ReportDown();

            _stylusMoved = false;
            _stylusDownPosition = currentPosition;
            _currentStylusUIElement = null;
        }

        private static void StylusMove(object sender, StylusEventArgs e)
        {
            if (sender is not DependencyObject dependencyObject)
                return;

            if (e.OriginalSource is DependencyObject originSource &&
                !GetSimulate(originSource))
            {
                return;
            }

            var currentPosition = e.GetPosition(null);

            if (_device != null &&
                _device.IsActive &&
                (_stylusMoved || GetPointDistance(_stylusDownPosition, currentPosition) >= GetMoveThreshold(dependencyObject)))
            {
                _device.Position = currentPosition;
                _device.ReportMove();

                if (sender is UIElement element &&
                    !_stylusMoved)
                {
                    _currentStylusUIElement = element;
                    e.StylusDevice.Capture(_currentStylusUIElement, CaptureMode.SubTree);
                }

                _stylusMoved = true;
            }
        }

        private static void StylusUp(object sender, StylusEventArgs e)
        {
            if (e.OriginalSource is DependencyObject originSource &&
                !GetSimulate(originSource))
            {
                return;
            }

            var currentPosition = e.GetPosition(null);

            if (_device != null &&
                _device.IsActive)
            {
                var device = _device;
                _device = null;

                device.Position = e.GetPosition(null);
                device.ReportUp();
                device.Deactivate();

                if (_currentStylusUIElement is not null)
                {
                    e.StylusDevice.Capture(null);
                }
            }
        }

        #endregion

        #region Constructors

        private StylusTouchDevice(int deviceId) :
            base(deviceId)
        {
            Position = new Point();
        }

        #endregion

        #region Overridden methods

        /// <inheritdoc/>
        public override TouchPointCollection GetIntermediateTouchPoints(IInputElement relativeTo)
        {
            return new TouchPointCollection();
        }

        /// <inheritdoc/>
        public override TouchPoint GetTouchPoint(IInputElement relativeTo)
        {
            Point point = Position;
            if (relativeTo != null)
            {
                point = this.ActiveSource.RootVisual.TransformToDescendant((Visual)relativeTo).Transform(Position);
            }

            Rect rect = new Rect(point, new Size(1, 1));

            return new TouchPoint(this, point, rect, TouchAction.Move);
        }

        #endregion



        /// <summary>
        /// Get value of Simulate property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static bool GetSimulate(DependencyObject obj)
        {
            return (bool)obj.GetValue(SimulateProperty);
        }

        /// <summary>
        /// Set value of Simulate property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetSimulate(DependencyObject obj, bool value)
        {
            obj.SetValue(SimulateProperty, value);
        }


        /// <summary>
        /// Get value of MoveThreshold property
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static double GetMoveThreshold(DependencyObject obj)
        {
            return (double)obj.GetValue(MoveThresholdProperty);
        }

        /// <summary>
        /// Set value of MoveThreshold property
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetMoveThreshold(DependencyObject obj, double value)
        {
            obj.SetValue(MoveThresholdProperty, value);
        }


        /// <summary>
        /// The DependencyProperty of Simulate property
        /// </summary>
        public static readonly DependencyProperty SimulateProperty =
            DependencyProperty.RegisterAttached("Simulate", typeof(bool), typeof(StylusTouchDevice), new PropertyMetadata(false, SimulatePropertyChanged));

        /// <summary>
        /// The DependencyProperty of MoveThreshold property
        /// </summary>
        public static readonly DependencyProperty MoveThresholdProperty =
            DependencyProperty.RegisterAttached("MoveThreshold", typeof(double), typeof(StylusTouchDevice), new PropertyMetadata(3.0));



        private static void SimulatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UIElement element ||
                e.NewValue is not bool newValue)
            {
                return;
            }

            if (newValue)
            {
                RegisterEvents(element);
            }
            else
            {
                UnregisterEvents(element);
            }
        }
    }
}
