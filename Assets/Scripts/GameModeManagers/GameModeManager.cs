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
            EventCallbacks.GameModeChangedEvent.RegisterListener(OnGameModeChanged);
        }

        private void Start()
        {
            playManager = new PlayManager();
            playManager.Init();

            buildManager = new BuildManager();
            buildManager.Init();
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

        private void FixedUpdate()
        {
            switch (this.currentGameMode)
            {
                case GameMode.Play:
                    //playManager.FixedUpdate();
                    break;
                case GameMode.Build:
                    buildManager.FixedUpdate();
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