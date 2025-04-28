using System;

namespace Sakura.Server
{
	/// <summary>
	/// An example script that just moves an entity. Note that this script does
	/// not use any backing systems, it just updates this entity's view model.
	/// </summary>
	public class TestScript : Script
	{
		// This public variable could be named anything. How would the
		// networking component know to set this to the destination value of a
		// "Move Body" network message?
		public float[] destination;

		/// <summary>
		/// A variable specific to this script. Since every script is a normal
		/// C# class, we can set it here or in the constructor.
		/// </summary>
		private readonly float xIncrement;

		// Use ordinary C# constructor to create our script. Scripts are
		// recreated every tick.
		public TestScript()
		{
			// Set out script variable
			xIncrement = 1.0f;
		}

        [Tick]
		public void Tick()
		{
			// Simulate movement without a backing system by just updating this
			// entity's view model.
			Console.WriteLine($"{entity.ID} starting position = <{entity.Position[0]}, {entity.Position[1]}, {entity.Position[2]}>");
			entity.Position[0] += xIncrement;
			Console.WriteLine($"{entity.ID} ending position = <{entity.Position[0]}, {entity.Position[1]}, {entity.Position[2]}>");
		}
	}
}

// Maybe instead of having public variables on scripts that the networking
// component sets there are just public variables on the overall simulation
// model that various use cases would probably want to read from? For example,
// if the Entity model had a "ClickedWorldSpaceLocation" variable that the
// networking component always set for each entity (or not set if they didn't
// click on any world space location in the previous tick). Then, a script
// could use the value of each entity's "ClickedWorldSpaceLocation" as the
// destination parameter of the "Move Body" use case. So the variables wouldn't
// be specific to a use case or script, it would be up to each script to use
// these variables to exectue the use cases the script wants to.
