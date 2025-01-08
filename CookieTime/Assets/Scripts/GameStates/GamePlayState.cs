using UI;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game
{
    public class GamePlayState : GameState
    {
        [Inject] public UIFactory _uiFactory;
        private GamePlay_UI _gamePlay_UI;

        public override void Enter()
        {
            InjectService.Inject(this);
            SceneManager.LoadSceneAsync("GamePlay").completed += async (_) =>
            {
                _gamePlay_UI = _uiFactory.GetUI<GamePlay_UI>() as GamePlay_UI;
                _gamePlay_UI.exitButton.onClick.AddListener(OpenMainMenu);
            };
        }
        private void OpenMainMenu()
        {
            gameStateChanger.ChangeState(new MainMenuState());
        }

    }
}