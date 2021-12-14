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
		
		// private const string SubType2 = "motorway_link";
		
		private const string Srid = "4326";

		public OsmData(string filePath)
		{
			_filePath = filePath;
		}

		private static bool Condition(OsmGeo osm)
		{
			// if (osm.Type == OsmGeoType.Node || (osm.Type == OsmGeoType.Way && (osm.Tags.Contains(Type, SubType) || osm.Tags.Contains(Type, SubType2))))
			if (osm.Type == OsmGeoType.Node || (osm.Type == OsmGeoType.Way && osm.Tags.Contains(Type, SubType)))
			{
				if (osm.Tags.ContainsKey("id"))
				{
					osm.Tags.RemoveKey("id");
				}

				return true;
			}

			return false;
		}

		public IEnumerable<ContinentModel> GetData()
		{
			using FileStream fileStream = File.OpenRead(_filePath);
			using PBFOsmStreamSource source = new PBFOsmStreamSource(fileStream);
			
			IEnumerable<OsmGeo> filtered = source.Where(Condition);

			using IFeatureStreamSource features = filtered.ToFeatureSource();
			IEnumerable<IFeature> lines = features.Where(f => f.Geometry is LineString);

			foreach (IFeature line in lines)
			{
				yield return new ContinentModel()
				{
					OsmId = BigInteger.Parse(line.Attributes.GetOptionalValue("id")?.ToString() ?? "0"),
					Geometry = $"SRID={Srid};" + line.Geometry,
					Name = line.Attributes.GetOptionalValue("int_ref")?.ToString() ?? "\\N",
					RefName = line.Attributes.GetOptionalValue("ref")?.ToString() ?? "\\N",
					Type = Type,
					SubType = line.Attributes.GetOptionalValue(Type)?.ToString() ?? "\\N",
				};
			}
		}
	}
}