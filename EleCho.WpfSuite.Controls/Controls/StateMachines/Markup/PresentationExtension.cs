using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace EleCho.WpfSuite.Controls.StateMachines.Markup
{
    public class PresentationExtension : MarkupExtension
    {
        public StateProperty Property { get; } = StateProperty.Background;

        public PresentationExtension()
        {

        }

        public PresentationExtension(StateProperty property)
        {
            Property = property;
        }



        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new Binding()
            {
                RelativeSource = new RelativeSource()
                {
                    Mode = RelativeSourceMode.Self,
                },
                Path = new System.Windows.PropertyPath($"(0).{Property}", StateMachine.PresentationProperty)
            };
        }
    }
}
