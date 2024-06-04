using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Project.Services.SaveSystems
{
	public class JsonSaveSystem : ISaveSystem
	{
		private readonly string _path;

		public JsonSaveSystem(string path)
		{
			_path = path;
		}

		public void Save(SaveData data)
		{
			try
			{
				var json = JsonConvert.SerializeObject(data, Formatting.Indented);
				File.WriteAllText(_path, json);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}

		public SaveData Load()
		{
			SaveData data = null;

			if (!File.Exists(_path))
			{
				return null;
			}

			try
			{
				var json = File.ReadAllText(_path);
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