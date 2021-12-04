using System.Collections.Generic;
using osm.CollectOsmData;
using osm.CollectOsmData.Models;

namespace osm
{
	class Program
	{
		static void Main(string[] args)
		{
			string filePath = @"C:\Users\mdj\Desktop\serbia-latest.osm.pbf";

			OsmData osmData = new OsmData(filePath);
			IEnumerable<OsmDataModel> collectedData = osmData.GetData();

			foreach (OsmDataModel osmDataModel in collectedData)
			{
				
			}
		}
	}
}