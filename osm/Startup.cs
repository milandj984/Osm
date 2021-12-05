using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
			IEnumerable<Type> classTypes = assembly.GetTypes().Where(t => t.IsClass && (t.Namespace?.StartsWith("osm.Models.CollectOsmData") ?? false));

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
				IContinent instance = Activator.CreateInstance(classType, _filePath, _folderPath) as IContinent;
				await instance!.MigrateAsync();
			}
		}
	}
}