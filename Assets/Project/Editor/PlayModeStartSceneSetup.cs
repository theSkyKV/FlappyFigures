using UnityEditor;
using UnityEditor.SceneManagement;

namespace Project.Editor
{
	[InitializeOnLoad]
	public class PlayModeStartSceneSetup
	{
		private const int StartSceneIndex = 0;

		static PlayModeStartSceneSetup()
		{
			SceneListChanged();
			EditorBuildSettings.sceneListChanged += SceneListChanged;
		}

		private static void SceneListChanged()
		{
			if (EditorBuildSettings.scenes.Length == 0)
			{
				return;
			}

			var scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[StartSceneIndex].path);
			EditorSceneManager.playModeStartScene = scene;
		}
	}
}