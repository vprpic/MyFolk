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

		public void OnBuildModeClick()
		{
			(new SetGameModeEvent(Time.GameMode.Build)).FireEvent();
		}
		public void OnPlayModeClick()
		{
			(new SetGameModeEvent(Time.GameMode.Play)).FireEvent();
		}
		public void OnMenuModeClick()
		{
			(new SetGameModeEvent(Time.GameMode.Menu)).FireEvent();
		}

		public void OnGameModeChanged(GameModeChangedEvent eventInfo)
		{
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
			menuMode.canvasGroup.alpha = 0f;
			playMode.canvasGroup.alpha = 1f;
		}

		private void SetBuildMode()
		{
			playMode.canvasGroup.alpha = 0f;
			menuMode.canvasGroup.alpha = 0f;
			buildMode.canvasGroup.alpha = 1f;
		}
		private void SetMenuMode()
		{
			menuMode.canvasGroup.alpha = 1f;
		}
	}
}