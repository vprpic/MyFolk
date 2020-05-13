using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MyFolk.UI;
using MyFolk.Time;
using MyFolk.Building;

namespace MyFolk
{
    [RequireComponent(typeof(UIInputManager))]
    public class GameModeManager : MonoBehaviour
    {
        [HideInInspector]
        public UIInputManager uIInputManager;
        [HideInInspector]
        public BuildManager buildManager;
        [HideInInspector]
        public PlayManager playManager;
        public GameMode currentGameMode;

        private void Awake()
        {
            uIInputManager = GetComponent<UIInputManager>();

            playManager = new PlayManager();
            playManager.Init();

            buildManager = new BuildManager();
            buildManager.Init();

            EventCallbacks.GameModeChangedEvent.RegisterListener(OnGameModeChanged);
        }

        private void Update()
        {

            switch (this.currentGameMode)
            {
                case GameMode.Play:
                    playManager.Update();
                    break;
                case GameMode.Build:
                    buildManager.Update();
                    break;
                case GameMode.Menu:
                    break;
            }
        }


        public void OnGameModeChanged(EventCallbacks.GameModeChangedEvent eventInfo)
        {
            this.currentGameMode = eventInfo.newGameMode;
        }
    }
}