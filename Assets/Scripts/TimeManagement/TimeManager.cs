using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Time
{
    public enum GameMode
    {
        None,
        Play,
        Build,
        Menu
    }

    public class TimeManager : MonoBehaviour
    {
        [Header("Settings")]
        [HideInInspector]
        public GameMode prevGameMode;
        public GameMode currentGameMode;

        [Range(0f,4f)]
        public float prevTimeScale;
        [Range(0f,4f)]
        public float currentTimeScale;

        [Header("Other")]
        public static float realTimePassed;

		#region waiting
		public static WaitingOver waitingOver;
        private static ScriptableAction.PerformActionOver performActionOver;

        public delegate void WaitingOver(ScriptableAction.PerformActionOver performActionOver);
        public static float waitingCounter;
        public static float waitingAmount;
		#endregion waiting

        void Start()
        {
            realTimePassed = 0f;
            this.prevGameMode = GameMode.None;
            this.currentGameMode = GameMode.None;
            this.currentTimeScale = 1f;
            SetGameModeEvent.RegisterListener(SetGameMode);
            SetTimeScaleEvent.RegisterListener(SetTimeScale);
            PauseTimeScaleEvent.RegisterListener(PauseTimeScale);
        }

        void Update()
        {
            switch (this.currentGameMode)
            {
                case GameMode.Play:
                    PlayMode();
                    break;
                case GameMode.Build:
                    break;
                case GameMode.Menu:
                    break;
                case GameMode.None:
                    (new SetGameModeEvent(GameMode.Menu)).FireEvent();
                    break;
            }
        }

        private void PlayMode()
        {
            realTimePassed += UnityEngine.Time.deltaTime * this.currentTimeScale;
        }

		#region events
		public void SetGameMode(SetGameModeEvent eventInfo)
        {
            if (eventInfo.newGameMode == this.currentGameMode)
                return;
            this.prevGameMode = this.currentGameMode;
            this.currentGameMode = eventInfo.newGameMode;
            if(this.currentGameMode == GameMode.Build || this.currentGameMode == GameMode.Menu)
            {
                //if (currentTimeScale > 0f)
                    SetTimeScale(0f);
                    //PauseGame();
            }
            else if(this.currentGameMode == GameMode.Play)
            {
                //if (currentTimeScale <= 0f)// && prevTimeScale > 0f)
                    SetPrevTimeScale();
                //else if(prevTimeScale <= 0f)
                //{
                //    SetTimeScale(1f);
                //}
            }
            (new EventCallbacks.GameModeChangedEvent(this.prevGameMode, this.currentGameMode)).FireEvent();
        }
        public void SetTimeScale(SetTimeScaleEvent eventInfo)
        {
            SetTimeScale(eventInfo.newTimeScale);
        }

        public void PauseTimeScale(PauseTimeScaleEvent eventInfo)
        {
            PauseGame();
        }
		#endregion events

		#region timescale
		private void SetTimeScale(float timescale)
        {
            if (Mathf.Approximately(timescale, this.currentTimeScale))
            {
                this.prevTimeScale = this.currentTimeScale;
                this.currentTimeScale = timescale;
                return;
            }
            this.prevTimeScale = this.currentTimeScale;
            this.currentTimeScale = timescale;
            (new TimeScaleChangedEvent(this.prevTimeScale, this.currentTimeScale)).FireEvent();
        }

        private void SetPrevTimeScale()
        {
            float temp = this.prevTimeScale;
            this.prevTimeScale = this.currentTimeScale;
            this.currentTimeScale = temp;
            (new TimeScaleChangedEvent(this.prevTimeScale, this.currentTimeScale)).FireEvent();
        }

        private void PauseGame()
        {
            if(this.currentTimeScale > 0f)
            {
                this.SetTimeScale(0f);
            }
            else
            {
                this.SetPrevTimeScale();
            }
        }

        public static float GetTime()
        {
            return realTimePassed;
        }
        #endregion timescale
    }
}