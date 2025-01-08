using System;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls.SourceGeneration
{
    /// <summary>
    /// Tell source generator what property to generate
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class GenerateStatesPropertyAttribute : Attribute
    {
        /// <summary>
        /// Property to generate
        /// </summary>
        public StateProperty Property { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="property"></param>
        public GenerateStatesPropertyAttribute(StateProperty property)
        {
            Property = property;
        }
    }
}
