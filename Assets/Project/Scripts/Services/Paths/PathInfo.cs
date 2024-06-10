using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Project.Services.Paths
{
	public class PathInfo
	{
		public string Key { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public PathPattern Pattern { get; set; }

		public string Value { get; set; }

		public string Extension { get; set; }
	}
}