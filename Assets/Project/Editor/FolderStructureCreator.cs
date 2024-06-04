using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Project.Editor
{
	public class FolderStructureCreator : EditorWindow
	{
		private static string _projectName = "PROJECT_NAME";

		[MenuItem("Assets/Create Default Folders")]
		private static void SetUpFolders()
		{
			var window = CreateInstance<FolderStructureCreator>();
			window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
			window.ShowPopup();
		}

		private static void CreateAllFolders()
		{
			var folders = new List<string>
			{
				"Art",
				"Audio",
				"Editor",
				"Prefabs",
				"Scripts",
				"Scenes",
				"UI"
			};

			var uiFolders = new List<string>
			{
				"Fonts",
				"Icons"
			};

			string path;
			foreach (var folder in folders)
			{
				if (Directory.Exists("Assets/" + folder))
				{
					continue;
				}

				path = "Assets/" + _projectName + "/" + folder;
				Directory.CreateDirectory(path);
				File.Create(path + "/.keep");
			}

			foreach (var subfolder in uiFolders)
			{
				path = "Assets/" + _projectName + "/UI/" + subfolder;
				if (Directory.Exists(path))
				{
					continue;
				}

				Directory.CreateDirectory(path);
				File.Create(path + "/.keep");
			}

			AssetDatabase.Refresh();
		}

		private static void SetRootNamespace()
		{
			EditorSettings.projectGenerationRootNamespace = _projectName;
		}

		private void OnGUI()
		{
			EditorGUILayout.LabelField("Insert the project name used as the root folder");
			_projectName = EditorGUILayout.TextField("Project name:", _projectName);
			Repaint();
			GUILayout.Space(70);
			if (GUILayout.Button("Generate"))
			{
				CreateAllFolders();
				SetRootNamespace();
				Close();
			}
			else if (GUILayout.Button("Cancel"))
			{
				Close();
			}
		}
	}
}