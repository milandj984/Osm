using osm.Interfaces;

namespace osm.Configurations
{
	public class EuropeDefinition : ITableDefinition
	{
		public string SchemaName => "public";
		
		public string TableName => "europe";
		
		public string Columns => "(osm_id, name, ref_name, type, sub_type, geometry)";

		public string GeometryIndexName => "idx_gist_europe_geometry";
	}
}