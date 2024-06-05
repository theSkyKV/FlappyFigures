using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Project.Services.Paths
{
	public class Path
	{
		public readonly string JsonSaveData;
		public readonly string BinarySaveData;
		public readonly string FigureInfo;

		private readonly List<PathInfo> _paths;

		private const string DataPath = "Config/path";

		public Path()
		{
			try
			{
				var data = Resources.Load<TextAsset>(DataPath).ToString();
				_paths = JsonConvert.DeserializeObject<List<PathInfo>>(data);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			JsonSaveData = GetPath("JsonSaveData");
			BinarySaveData = GetPath("BinarySaveData");
			FigureInfo = GetPath("FigureInfo");
		}

		private string GetPath(string key)
		{
			var info = _paths.FirstOrDefault(p => p.Key == key);
			if (info == null)
			{
				return string.Empty;
			}

			var pattern = GetPattern(info.Pattern);
			var slash = string.IsNullOrEmpty(pattern) ? "" : "/";
			var extension = info.Pattern == PathPattern.Resources ? "" : $".{info.Extension}";
			var str = $"{pattern}{slash}{info.Value}{extension}";

			return str;
		}

		private string GetPattern(PathPattern pattern)
		{
			return pattern switch
			{
				PathPattern.Resources => string.Empty,
				PathPattern.PersistentDataPath => Application.persistentDataPath,
				_ => string.Empty
			};
		}
	}
}