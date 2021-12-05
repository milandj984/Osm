namespace osm.Interfaces
{
	public interface ITableDefinition
	{
		public string SchemaName { get; }
		
		public string TableName { get; }

		public string Columns { get; }

		public string GeometryIndexName { get; }
	}
}