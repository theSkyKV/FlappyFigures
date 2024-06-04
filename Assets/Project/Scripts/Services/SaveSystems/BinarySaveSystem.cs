using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Project.Services.SaveSystems
{
	public class BinarySaveSystem : ISaveSystem
	{
		private readonly string _path;

		public BinarySaveSystem(string path)
		{
			_path = path;
		}

		public void Save(SaveData saveData)
		{
			try
			{
				using var file = File.Create(_path);
				new BinaryFormatter().Serialize(file, saveData);
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
				using var file = File.Open(_path, FileMode.Open);
				var loadedData = new BinaryFormatter().Deserialize(file);
				data = (SaveData) loadedData;
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			return data;
		}
	}
}