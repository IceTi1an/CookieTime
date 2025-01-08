using UnityEngine;

namespace Game
{
    public class GameStart : MonoBehaviour
    {
        private GameStateChanger _gameStateChanger = new();

        private void Start()
        {
            InjectService.Inject(this);

            DontDestroyOnLoad(gameObject);

            _gameStateChanger.ChangeState(new MainMenuState());
        }
    }
}
