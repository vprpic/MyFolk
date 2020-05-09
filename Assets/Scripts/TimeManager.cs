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
        public float realTimePassed;

        void Start()
        {
            this.realTimePassed = 0f;
            this.prevGameMode = GameMode.Play;
            this.currentGameMode = GameMode.Play;
            this.currentTimeScale = 1f;
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
            this.realTimePassed += UnityEngine.Time.deltaTime * this.currentTimeScale;
        }

        public void SetGameMode(GameMode gm)
        {
            if (gm == this.currentGameMode)
                return;
            this.prevGameMode = this.currentGameMode;
            this.currentGameMode = gm;
            (new EventCallbacks.GameModeChangedEvent(this.prevGameMode, this.currentGameMode)).FireEvent();
        }

        public void SetTimeScale(float timescale)
        {
            if (Mathf.Approximately(timescale, this.currentTimeScale))
                return;
            this.prevTimeScale = this.currentTimeScale;
            this.currentTimeScale = timescale;
            //UnityEngine.Time.timeScale = this.currentTimeScale;
            (new EventCallbacks.TimeScaleChangedEvent(this.prevTimeScale, this.currentTimeScale)).FireEvent();
        }
        public void SetPrevTimeScale()
        {
            float temp = this.prevTimeScale;
            this.prevTimeScale = this.currentTimeScale;
            this.currentTimeScale = temp;
            //UnityEngine.Time.timeScale = this.currentTimeScale;
            (new EventCallbacks.TimeScaleChangedEvent(this.prevTimeScale, this.currentTimeScale)).FireEvent();
        }


        public void PauseGame()
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
    }
}