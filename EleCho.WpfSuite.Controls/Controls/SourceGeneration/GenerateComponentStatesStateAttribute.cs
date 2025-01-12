using System;
using EleCho.WpfSuite.Controls.States;
using EleCho.WpfSuite.Internal;

namespace EleCho.WpfSuite.Controls.SourceGeneration
{
    /// <summary>
    /// Tell source generator to generate one component state
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class GenerateComponentStatesStateAttribute : Attribute
    {
        public string ComponentName { get; }
        public State State { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="state"></param>
        /// <exception cref="ArgumentException"></exception>
        public GenerateComponentStatesStateAttribute(string componentName, State state)
        {
            if (!StringUtils.IsIdentifier(componentName))
            {
                throw new ArgumentException("Invalid component name", nameof(componentName));
            }

            ComponentName = componentName;
            State = state;
        }
    }
}
