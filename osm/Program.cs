using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using osm.Models.CollectOsmData;
using osm.Models.CollectOsmData.Europe;

namespace osm
{
	class Program
	{
		static async Task Main(string[] args)
		{
			const string filePath = @"C:\Users\mdj\Desktop\Osm\europe-serbia-latest.osm.pbf";
			
			Startup startup = new Startup(filePath);
			await startup.Initiate();
		}
	}
}