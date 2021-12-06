using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using osm.Configurations;
using osm.Interfaces;
using osm.Models.CreateCopyFile;

namespace osm.Models.CollectOsmData
{
	public class Europe : IContinent
	{
		private readonly string _filePath;
		
		private readonly string _folderPath;

		public Europe(string filePath, string folderPath)
		{
			_filePath = filePath;
			_folderPath = folderPath;
		}

		private IEnumerable<ContinentModel> CollectFromOsm()
		{
			Console.WriteLine("Collecting data for Europe");
			OsmData osmData = new OsmData(_filePath);
			
			return osmData.GetData();
		}

		public async Task<string> WriteToCopySqlFileAsync()
		{
			IEnumerable<ContinentModel> data = CollectFromOsm();
			CreateCopyFile<EuropeDefinition> createCopyFile = new CreateCopyFile<EuropeDefinition>(_folderPath, data, new EuropeDefinition(), nameof(Europe));
			
			return await createCopyFile.CreateAsync();
		}
	}
}