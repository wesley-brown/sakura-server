namespace Sakura.Server
{
    /// <summary>
    ///     Represents a script that can be attached to an entity.
    /// </summary>
    public class Script
    {
        /// <summary>
        ///     The simulation view model entity this script is attached to.
        ///     This is set when a script is created by the server.
        /// </summary>
        protected readonly Entity entity;
    }
}
