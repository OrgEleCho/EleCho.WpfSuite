using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleCho.WpfSuite.Controls.States;

namespace EleCho.WpfSuite.Controls.SourceGeneration
{
    /// <summary>
    /// Tell source generator to generate state property definitions
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class GenerateStatePropertiesAttribute : Attribute
    {
        /// <summary>
        /// State to generate
        /// </summary>
        public State State { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="state"></param>
        public GenerateStatePropertiesAttribute(State state)
        {
            State = state;
        }
    }
}
