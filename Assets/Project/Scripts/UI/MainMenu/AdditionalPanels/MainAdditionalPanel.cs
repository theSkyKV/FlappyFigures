using System.Collections.Generic;
using System.Linq;
using Project.Core;
using Project.Entities.Figures;
using Project.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.UI.MainMenu.AdditionalPanels
{
	public class MainAdditionalPanel : AdditionalPanelBase
	{
		[SerializeField]
		private Button _leftButton;

		[SerializeField]
		private Button _rightButton;

		[SerializeField]
		private Button _openNowButton;

		[SerializeField]
		private Image _icon;

		[SerializeField]
		private TMP_Text _record;

		[SerializeField]
		private TMP_Text _name;

		[SerializeField]
		private TMP_Text _description;

		private LinkedList<FigureInfo> _figureInfos;
		private LinkedListNode<FigureInfo> _current;

		private FigureInfo _figureInfo;

		public void Init()
		{
			_figureInfos = ProjectContext.Instance.FigureInfos.ToLinkedList();
			_current = _figureInfos.Nodes()
						   .FirstOrDefault(f => f.Value.Type == ProjectContext.Instance.Figure.Type)
					   ?? _figureInfos.First;
		}

		public override void Activate()
		{
			_leftButton.onClick.AddListener(OnLeftButtonClicked);
			_rightButton.onClick.AddListener(OnRightButtonClicked);
			UpdateInfo();
			base.Activate();
		}

		public override void Deactivate()
		{
			_leftButton.onClick.RemoveListener(OnLeftButtonClicked);
			_rightButton.onClick.RemoveListener(OnRightButtonClicked);
			base.Deactivate();
		}

		private void UpdateInfo()
		{
			_figureInfo = _current?.Value;

			if (_figureInfo == null)
			{
				return;
			}

			ProjectContext.Instance.UpdateFigure(_figureInfo.Type);
			_icon.sprite = _figureInfo.Sprite;
			_name.text = _figureInfo.Name;
			_description.text = _figureInfo.Description;
		}

		private void OnLeftButtonClicked()
		{
			_current = _current.PreviousOrLast();
			UpdateInfo();
			EventSystem.current.SetSelectedGameObject(null);
		}

		private void OnRightButtonClicked()
		{
			_current = _current.NextOrFirst();
			UpdateInfo();
			EventSystem.current.SetSelectedGameObject(null);
		}
	}
}