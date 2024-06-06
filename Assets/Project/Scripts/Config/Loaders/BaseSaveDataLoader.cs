using System;
using Newtonsoft.Json;
using Project.Services.SaveSystems;
using UnityEngine;

namespace Project.Config.Loaders
{
	public class BaseSaveDataLoader
	{
		private readonly string _path;

		public BaseSaveDataLoader(string path)
		{
			_path = path;
		}

		public SaveData Load()
		{
			SaveData data = null;

			try
			{
				var json = Resources.Load<TextAsset>(_path).ToString();
				data = JsonConvert.DeserializeObject<SaveData>(json);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			return data;
		}
	}
}