using System.Collections.Generic;
using System.Threading.Tasks;
using osm.Configurations;
using osm.Interfaces;
using osm.Models.CreateCopyFile;

namespace osm.Models.CollectOsmData.Europe
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

		private IEnumerable<EuropeModel> CollectFromOsm()
		{
			OsmData osmData = new OsmData(_filePath);
			
			return osmData.GetData<EuropeModel>();
		}

		private async Task<string> WriteToCopySqlFileAsync()
		{
			IEnumerable<EuropeModel> data = CollectFromOsm();
			CreateCopyFile<EuropeModel, EuropeDefinition> createCopyFile = new CreateCopyFile<EuropeModel, EuropeDefinition>(_folderPath, data, 
				new EuropeDefinition(), nameof(Europe));
			
			return await createCopyFile.CreateAsync();
		}

		public async Task MigrateAsync()
		{
			string sqlCopyFilePath = await WriteToCopySqlFileAsync();
		}
	}
}