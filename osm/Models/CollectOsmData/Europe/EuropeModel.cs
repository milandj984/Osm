using System.Numerics;
using osm.Interfaces;

namespace osm.Models.CollectOsmData.Europe
{
	public class EuropeModel : IOsmData
	{
		public BigInteger OsmId { get; set; }
		
		public string Name { get; set; }
		
		public string RefName { get; set; }
		
		public string Geometry { get; set; }

		public override string ToString()
		{
			return $"{OsmId}\t{Name}\t{RefName}\t{Geometry}\n";
		}
	}
}