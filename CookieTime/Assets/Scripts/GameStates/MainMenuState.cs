using UnityEngine;
using System;
using Zenject;
using UI;
using UnityEngine.SceneManagement;

namespace Game
{
    public class MainMenuState : GameState
    {
        [Inject] public UIFactory _uiFactory;

        private UIBase _uiContainer;

        [SerializeField] private MainMenu_UI _mainMenu_UI;
        [SerializeField] private LevelMenu_UI _levelMenu_UI;

        public override void Enter()
        {
            InjectService.Inject(this);
            SceneManager.LoadSceneAsync("MainMenu").completed += async (_) =>
            {
                _mainMenu_UI = _uiFactory.GetUI<MainMenu_UI>() as MainMenu_UI;
                _levelMenu_UI = _uiFactory.GetUI<LevelMenu_UI>() as LevelMenu_UI;

                OpenMainMenu();

                _mainMenu_UI.playButton.onClick.AddListener(OpenLevelMenu);
                _mainMenu_UI.shopButton.onClick.AddListener(OpenShopMenu);
                _mainMenu_UI.exitButton.onClick.AddListener(Exit);

                _levelMenu_UI.goToGamePlay.onClick.AddListener(OpenGamePlay);
                _levelMenu_UI.goBackToMainMenu.onClick.AddListener(OpenMainMenu);
            };
        }
        private void OpenMainMenu()
        {
            _mainMenu_UI.gameObject.SetActive(true);
            _levelMenu_UI.gameObject.SetActive(false);
        }
        private void OpenLevelMenu()
        {
            _mainMenu_UI.gameObject.SetActive(false);
            _levelMenu_UI.gameObject.SetActive(true);
        }
        private void OpenGamePlay()
        {
            gameStateChanger.ChangeState(new GamePlayState());
        }
        private void OpenShopMenu()
        {

        }
        private void Exit()
        {
            Application.Quit();
        }
    }
}