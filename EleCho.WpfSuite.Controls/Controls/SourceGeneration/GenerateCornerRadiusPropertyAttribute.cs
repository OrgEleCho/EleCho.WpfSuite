using System;

namespace EleCho.WpfSuite.Controls.SourceGeneration
{
    /// <summary>
    /// Tell source generator go generate CornerRadius property definition
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GenerateCornerRadiusPropertyAttribute : Attribute
    {

    }
}
