using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Controls.States.Markup
{
    /// <summary>
    /// Return the binding of Showing property. Use this in Style or ControlTemplate.
    /// </summary>
    [MarkupExtensionReturnType(typeof(Binding))]
    public class ShowingPropertyExtension : MarkupExtension
    {
        /// <summary>
        /// Target property
        /// </summary>
        public StateProperty Property { get; } = StateProperty.Background;

        /// <inheritdoc/>
        public ShowingPropertyExtension()
        {

        }

        /// <inheritdoc/>
        public ShowingPropertyExtension(StateProperty property)
        {
            Property = property;
        }

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var dp = Property switch
            {
                StateProperty.Background => StateManager.ShowingBackgroundProperty,
                StateProperty.Foreground => StateManager.ShowingForegroundProperty,
                StateProperty.BorderBrush => StateManager.ShowingBorderBrushProperty,
                StateProperty.Padding => StateManager.ShowingPaddingProperty,
                StateProperty.BorderThickness => StateManager.ShowingBorderThicknessProperty,
                StateProperty.CornerRadius => StateManager.ShowingCornerRadiusProperty,
                _ => throw new ArgumentException("Invalid property", nameof(Property))
            };

            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget &&
                provideValueTarget.TargetObject is Setter)
            {
                return new Binding()
                {
                    RelativeSource = new RelativeSource()
                    {
                        Mode = RelativeSourceMode.Self,
                    },

                    Path = new PropertyPath(dp)
                };
            }
            else
            {
                return new Binding()
                {
                    RelativeSource = new RelativeSource()
                    {
                        Mode = RelativeSourceMode.TemplatedParent,
                    },

                    Path = new PropertyPath(dp)
                };
            }
        }
    }
}
