using System.Threading.Tasks;

namespace osm.Interfaces
{
	public interface IContinent
	{
		public Task<string> WriteToCopySqlFileAsync();
	}
}