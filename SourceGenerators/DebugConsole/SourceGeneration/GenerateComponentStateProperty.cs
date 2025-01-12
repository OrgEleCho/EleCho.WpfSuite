using System;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls.SourceGeneration
{
    /// <summary>
    /// Tell source generator to generate one component state property
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class GenerateComponentStateProperty : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="property"></param>
        /// <exception cref="ArgumentException"></exception>
        public GenerateComponentStateProperty(string componentName, StateProperty property)
        {
            ComponentName = componentName;
            Property = property;
        }

        public string ComponentName { get; }
        public StateProperty Property { get; }
    }
}
