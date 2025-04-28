namespace Sakura.Server
{
	/// <summary>
	/// A representation of the current world being simulated.
	/// </summary>
	public class World
	{
		/// <summary>
		/// The entities currently being simulated.
		/// </summary>
		public Entity[] Entities { get; set; }
	}
}
