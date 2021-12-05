using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using OsmSharp;
using OsmSharp.Geo;
using OsmSharp.Geo.Streams;
using OsmSharp.Streams;

namespace osm.Models.CollectOsmData
{
	public class OsmData
	{
		private readonly string _filePath;

		private const string Type = "highway";
		
		private const string SubType = "motorway";
		
		private const string Srid = "4326";

		public OsmData(string filePath)
		{
			_filePath = filePath;
		}

		public IEnumerable<OsmDataModel> GetData()
		{
			using (FileStream fileStream = File.OpenRead(_filePath))
			{
				using (PBFOsmStreamSource source = new PBFOsmStreamSource(fileStream))
				{
					IEnumerable<OsmGeo> filtered = source.Where(osm => osm.Type == OsmGeoType.Node || (osm.Type == OsmGeoType.Way && osm.Tags.Contains(Type, SubType)));
					
					using (IFeatureStreamSource features = filtered.ToFeatureSource())
					{
						IEnumerable<IFeature> lines = features.Where(f => f.Geometry is LineString);

						foreach (IFeature line in lines)
						{
							yield return new OsmDataModel()
							{
								OsmId = BigInteger.Parse(line.Attributes.GetOptionalValue("id")?.ToString() ?? "0"),
								Geometry = $"SRID={Srid};" + line.Geometry,
								Name = line.Attributes.GetOptionalValue("int_ref")?.ToString() ?? "/N",
								RefName = line.Attributes.GetOptionalValue("ref")?.ToString() ?? "/N"
							};
						}
					}
				}
			}
		}
	}
}