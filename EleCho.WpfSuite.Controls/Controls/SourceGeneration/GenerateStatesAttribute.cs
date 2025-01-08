using System;

namespace EleCho.WpfSuite.Controls.SourceGeneration
{
    /// <summary>
    /// Tell source generator to generate states code
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GenerateStatesAttribute : Attribute
    { }
}
