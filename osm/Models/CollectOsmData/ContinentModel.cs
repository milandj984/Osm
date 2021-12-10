using System.Numerics;
using osm.Interfaces;

namespace osm.Models.CollectOsmData
{
	public class ContinentModel
	{
		public BigInteger OsmId { get; set; }
		
		public string Name { get; set; }
		
		public string RefName { get; set; }
		
		public string Type { get; set; }
		
		public string SubType { get; set; }
		
		public string Geometry { get; set; }

		public override string ToString()
		{
			return $"{OsmId}\t{Name}\t{RefName}\t{Type}\t{SubType}\t{Geometry}\n";
		}
	}
}