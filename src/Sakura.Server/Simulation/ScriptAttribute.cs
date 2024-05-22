using System;

namespace Sakura.Server
{
    /// <summary>
    ///     Specifies that a class represents a script.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = false)]
    public sealed class ScriptAttribute : Attribute
    {

    }
}
