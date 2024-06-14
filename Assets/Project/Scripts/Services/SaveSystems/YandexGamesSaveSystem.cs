using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using UnityEngine;

namespace Project.Services.SaveSystems
{
	public class YandexGamesSaveSystem : ISaveSystem
	{
		[DllImport("__Internal")]
		private static extern void SaveExtern(string data);

		[DllImport("__Internal")]
		private static extern void LoadExtern();

		public void Save(SaveData data)
		{
			try
			{
				var json = JsonConvert.SerializeObject(data, Formatting.Indented);
				SaveExtern(json);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}
		}

		public SaveData Load()
		{
			LoadExtern();

			return null;
		}

		public void Reset()
		{ }
	}
}