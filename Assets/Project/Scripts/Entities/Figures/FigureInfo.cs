using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace Project.Entities.Figures
{
	public class FigureInfo
	{
		public int Id { get; set; }

		public string StringId { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public FigureType Type { get; set; }

		public float Mass { get; set; }

		public float GravityScale { get; set; }

		public int LifeCount { get; set; }

		public string SpritePath { get; set; }

		[JsonIgnore]
		public Sprite Sprite { get; set; }
	}
}