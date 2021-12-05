using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using osm.Configurations;
using osm.Interfaces;

namespace osm.Models.CreateCopyFile
{
	public class CreateCopyFile<TData, TTableDefinition> where TData : class where TTableDefinition : ITableDefinition
	{
		private readonly string _outputPath;
		
		private readonly IEnumerable<TData> _data;

		private readonly string _fileName;

		private readonly string _schemaName;

		private readonly string _tableName;
		
		private readonly string _columns;
		
		private readonly string _geometryIndexNameIndex;

		public CreateCopyFile(string outputPath, IEnumerable<TData> data, TTableDefinition tableDefinition, string continentName)
		{
			_outputPath = outputPath;
			_data = data;
			_schemaName = tableDefinition.SchemaName;
			_tableName = tableDefinition.TableName;
			_columns = tableDefinition.Columns;
			_geometryIndexNameIndex = tableDefinition.GeometryIndexName;
			_fileName = $"COPY-{continentName}.sql";
		}

		public async Task<string> CreateAsync()
		{
			string filePath = Path.Join(_outputPath, _fileName);
			using StreamWriter writer = new StreamWriter(filePath);
			StringBuilder builder = new StringBuilder();

			string headers = $@"\connect {PostgreSql.DbName}

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

";
			builder.Append(headers);
			
			string preOperations = $"TRUNCATE TABLE {_schemaName}.{_tableName};\nDROP INDEX {_schemaName}.{_geometryIndexNameIndex};\n\n";
			builder.Append(preOperations);
			
			string copyDeclaration = $"COPY {_schemaName}.{_tableName} {_columns} FROM stdin;\n";
			builder.Append(copyDeclaration);

			foreach (TData osmData in _data)
			{
				builder.Append(osmData);
			}
			builder.Append("\\.\n\n");

			string postOperations = $"CREATE INDEX {_geometryIndexNameIndex} ON {_schemaName}.{_tableName} USING gist (geometry);\n";
			builder.Append(postOperations);
			
			await writer.WriteAsync(builder.ToString());
			writer.Close();

			return filePath;
		}
	}
}