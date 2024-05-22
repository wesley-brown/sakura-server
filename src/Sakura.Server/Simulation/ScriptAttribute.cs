using System;

namespace Sakura.Server
{
    /// <summary>
    ///     Specifies that a class represents a script.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class)]
    public sealed class ScriptAttribute : Attribute
    {

    }
}
