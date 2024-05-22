using System;

namespace Sakura.Server
{
	public class TestScript
	{
		[Tick]
		public void Tick()
		{
			Console.WriteLine("Tick");
		}
	}
}
