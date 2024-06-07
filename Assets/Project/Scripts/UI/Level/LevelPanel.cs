using TMPro;
using UnityEngine;

namespace Project.UI.Level
{
	public class LevelPanel : MonoBehaviour
	{
		[SerializeField]
		private PausePanel _pausePanel;

		[SerializeField]
		private GameOverPanel _gameOverPanel;

		[SerializeField]
		private TMP_Text _score;

		public void UpdateScore(int value)
		{
			_score.text = value.ToString();
		}
	}
}