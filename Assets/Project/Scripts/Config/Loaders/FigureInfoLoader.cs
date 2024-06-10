using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Project.Entities.Figures;
using UnityEngine;

namespace Project.Config.Loaders
{
	public class FigureInfoLoader
	{
		private readonly string _path;

		public FigureInfoLoader(string path)
		{
			_path = path;
		}

		public List<FigureInfo> GetAll()
		{
			List<FigureInfo> list = null;
			try
			{
				var data = Resources.Load<TextAsset>(_path).ToString();
				list = JsonConvert.DeserializeObject<List<FigureInfo>>(data);

				if (list == null)
				{
					return null;
				}

				foreach (var info in list)
				{
					info.Sprite = Resources.Load<Sprite>(info.SpritePath);
				}
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			return list;
		}
	}
}