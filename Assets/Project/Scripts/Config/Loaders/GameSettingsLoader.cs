using System;
using Newtonsoft.Json;
using Project.Config.Entities;
using UnityEngine;

namespace Project.Config.Loaders
{
	public class GameSettingsLoader
	{
		private readonly string _path;

		public GameSettingsLoader(string path)
		{
			_path = path;
		}

		public GameSettings Get()
		{
			GameSettings data = null;
			try
			{
				var json = Resources.Load<TextAsset>(_path).ToString();
				data = JsonConvert.DeserializeObject<GameSettings>(json);
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			return data;
		}
	}
}