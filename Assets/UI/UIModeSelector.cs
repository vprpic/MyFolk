using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.UI
{
	public class UIModeSelector : MonoBehaviour
	{
		public Time.GameMode currentGameMode;

		public UIMenuMode menuMode;
		public UIPlayMode playMode;
		public UIBuildMode buildMode;

		private void Awake()
		{
			GameModeChangedEvent.RegisterListener(OnGameModeChanged);
		}
		private void Start()
		{
			SetMenuMode();
		}

		public void OnBuildModeClick()
		{
			(new SetGameModeEvent(Time.GameMode.Build)).FireEvent();
		}
		public void OnPlayModeClick()
		{
			UpdateSkinnedMeshRendererColliders.UpdateAllSkinnedMeshRendererColliders();
			(new SetGameModeEvent(Time.GameMode.Play)).FireEvent();
		}
		public void OnMenuModeClick()
		{
			(new SetGameModeEvent(Time.GameMode.Menu)).FireEvent();
		}

		public void OnGameModeChanged(GameModeChangedEvent eventInfo)
		{
			if (this.currentGameMode.Equals(eventInfo.newGameMode))
				return;
			this.currentGameMode = eventInfo.newGameMode;
			switch (currentGameMode)
			{
				case Time.GameMode.Play:
					SetPlayMode();
					break;
				case Time.GameMode.Build:
					SetBuildMode();
					break;
				case Time.GameMode.Menu:
					SetMenuMode();
					break;
			}

		}
		private void SetPlayMode()
		{
			buildMode.canvasGroup.alpha = 0f;
			buildMode.canvasGroup.interactable = false;
			buildMode.canvasGroup.blocksRaycasts = false;
			menuMode.canvasGroup.alpha = 0f;
			menuMode.canvasGroup.interactable = false;
			menuMode.canvasGroup.blocksRaycasts = false;
			playMode.canvasGroup.alpha = 1f;
			playMode.canvasGroup.interactable = true;
			playMode.canvasGroup.blocksRaycasts = true;
		}

		private void SetBuildMode()
		{
			playMode.canvasGroup.alpha = 0f;
			playMode.canvasGroup.interactable = false;
			playMode.canvasGroup.blocksRaycasts = false;
			menuMode.canvasGroup.alpha = 0f;
			menuMode.canvasGroup.interactable = false;
			menuMode.canvasGroup.blocksRaycasts = false;
			buildMode.canvasGroup.alpha = 1f;
			buildMode.canvasGroup.interactable = true;
			buildMode.canvasGroup.blocksRaycasts = true;
		}
		private void SetMenuMode()
		{
			playMode.canvasGroup.interactable = false;
			buildMode.canvasGroup.interactable = false;
			menuMode.canvasGroup.alpha = 1f;
			menuMode.canvasGroup.interactable = true;
			menuMode.canvasGroup.blocksRaycasts = true;
		}
	}
}