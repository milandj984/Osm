using System.Numerics;

namespace osm.Models.CollectOsmData
{
	public class OsmDataModel
	{
		public BigInteger OsmId { get; set; }
		
		public string Name { get; set; }
		
		public string RefName { get; set; }
		
		public string Geometry { get; set; }
	}
}