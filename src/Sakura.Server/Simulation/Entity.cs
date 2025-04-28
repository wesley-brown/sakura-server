namespace Sakura.Server
{
	/// <summary>
	/// A representation of an entity in a simulation's view model.
	/// </summary>
	/// <remarks>
	/// It is important to understand that this is just a view model
	/// representation of an entity and is not passed to systems.
	/// </remarks>
	public sealed class Entity
	{
		public string ID { get; set; }

		public float[] Position { get; set; }

		public float[] Rotation { get; set; }

		public float[] Scale { get; set; }

		public string ModelID { get; set; }

		/// <summary>
		/// The world space location that this entity interacted with last
		/// tick, if any. Null otherwise.
		/// </summary>
		/// <remarks>
		/// If this entity represents a player, when this value is not null,
		/// this value will be where that player clicked in world space during
		/// the last tick. If that player did not click during the last click,
		/// this will be null. If this entity does not represent a player, this
		/// value will always be null.
		/// </remarks>
		public float[] InteractedWorldSpaceLocation{ get; set;}

		/// <summary>
		/// The list of entities that interacted with this entity last tick.
		/// </summary>
		/// <remarks>
		/// The definition of an "interaction" is purposefully left vague. This
		/// allows one client to treat a click on this centity as an
		/// "interaction" while another can treat looking at this entity and
		/// pressing an "interaction button" as an "interaction".
		/// </remarks>
		public Entity[] Interactors { get; set;}
	}
}
