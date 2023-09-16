using System;

namespace Sakura.Server
{
    /// <summary>
    ///     Specifies that a method represents a script's fixed update logic.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TickAttribute : Attribute
    {

    }
}
