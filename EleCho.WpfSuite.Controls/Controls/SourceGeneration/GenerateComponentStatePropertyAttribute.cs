using System;
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Internal;

namespace EleCho.WpfSuite.Controls.SourceGeneration
{
    /// <summary>
    /// Tell source generator to generate one component state property
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class GenerateComponentStatePropertyAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="property"></param>
        /// <exception cref="ArgumentException"></exception>
        public GenerateComponentStatePropertyAttribute(string componentName, StateProperty property)
        {
            if (!StringUtils.IsIdentifier(componentName))
            {
                throw new ArgumentException("Invalid component name", nameof(componentName));
            }

            ComponentName = componentName;
            Property = property;
        }

        public string ComponentName { get; }
        public StateProperty Property { get; }
    }
}
