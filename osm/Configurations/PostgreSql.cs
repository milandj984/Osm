namespace osm.Configurations
{
	public static class PostgreSql
	{
		public static string DbName => "OsmDb";
		
		public static string User => "postgres";

		public static string Password => "postgres";
		
		public static string Host => "localhost";
		
		public static int Port => 5432;
	}
}