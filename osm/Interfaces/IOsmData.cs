using System.Numerics;

namespace osm.Interfaces
{
	public interface IOsmData
	{
		public BigInteger OsmId { get; set; }

		public string Name { get; set; }

		public string RefName { get; set; }

		public string Geometry { get; set; }
	}
}