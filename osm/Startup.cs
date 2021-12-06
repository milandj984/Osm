using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using osm.Configurations;
using osm.Interfaces;

namespace osm
{
	public class Startup
	{
		private readonly string _filePath;
		
		private readonly string _fileName;
		
		private readonly string _folderPath;

		public Startup(string filePath)
		{
			_filePath = filePath;
			_fileName = Path.GetFileNameWithoutExtension(filePath);
			_folderPath = Path.GetDirectoryName(filePath);
		}

		private Type GetCorrespondingModelOrDefault()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			IEnumerable<Type> classTypes = assembly.GetTypes().Where(t => t.IsClass && t.Namespace == "osm.Models.CollectOsmData");

			foreach (Type classType in classTypes)
			{
				if (_fileName.ToLower().Contains(classType.Name.ToLower()))
				{
					return classType;
				}
			}

			return default;
		}

		public async Task Initiate()
		{
			Type classType = GetCorrespondingModelOrDefault();

			if (classType == null)
			{
				Console.WriteLine("No corresponding model found for a given file");
			}
			else
			{
				// Read osm file and write data to sql file
				IContinent instance = Activator.CreateInstance(classType, _filePath, _folderPath) as IContinent;
				string sqlFilePath = await instance!.WriteToCopySqlFileAsync();

				// Write sql file to database
				string command = $"/C set PGPASSWORD={PostgreSql.Password}&psql -U {PostgreSql.User} -h {PostgreSql.Host} -p {PostgreSql.Port} -f {sqlFilePath}";
				Process.Start("cmd.exe", command)!.WaitForExit();
			}
		}
	}
}