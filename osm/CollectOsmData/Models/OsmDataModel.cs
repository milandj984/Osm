using System.Numerics;

namespace osm.CollectOsmData.Models
{
	public class OsmDataModel
	{
		public BigInteger OsmId { get; set; }
		
		public string Name { get; set; }
		
		public string RefName { get; set; }
		
		public string Geometry { get; set; }
	}
}