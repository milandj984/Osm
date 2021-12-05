using System;

namespace osm.CustomAttributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ContinentAttribute : Attribute
	{
		public string Continent { get; }

		public ContinentAttribute(string continent)
		{
			Continent = continent;
		}
	}
}