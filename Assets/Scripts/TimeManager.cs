using EventCallbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyFolk.Time
{
    public enum GameMode
    {
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

        [HideInInspector]
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
            this.prevGameMode = GameMode.Play;
            this.currentGameMode = GameMode.Play;
            this.currentTimeScale = 1f;
            SetGameModeEvent.RegisterListener(SetGameMode);
            SetTimeScaleEvent.RegisterListener(SetTimeScale);
            PauseTimeScaleEvent.RegisterListener(PauseTimeScale);
        }

        void Update()
        {
#if UNITY_EDITOR
            ////TODO: remove
            //if (!this.currentGameMode.Equals(this.prevGameMode))
            //    (new EventCallbacks.GameModeChangedEvent(this.prevGameMode, this.currentGameMode)).FireEvent();

            ////TODO: remove
            //if (!this.currentTimeScale.Equals(this.prevTimeScale))
            //{
            //    //UnityEngine.Time.timeScale = this.currentTimeScale;
            //    (new EventCallbacks.TimeScaleChangedEvent(this.prevTimeScale, this.currentTimeScale)).FireEvent();
            //}
#endif

            switch (this.currentGameMode)
            {
                case GameMode.Play:
                    PlayMode();
                    break;
                case GameMode.Build:
                    break;
                case GameMode.Menu:
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


        private void SetTimeScale(float timescale)
        {
            if (Mathf.Approximately(timescale, this.currentTimeScale))
                return;
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
    }
}