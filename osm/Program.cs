using System.Threading.Tasks;

namespace osm
{
	class Program
	{
		static async Task Main(string[] args)
		{
			const string filePath = @"C:\Milan\Work\Charging-stations\Osm\europe-latest.osm.pbf";
			
			Startup startup = new Startup(filePath);
			await startup.Initiate();
		}
	}
}