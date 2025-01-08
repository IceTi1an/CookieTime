using UnityEngine;

namespace Game
{
    public class GameStateChanger : MonoBehaviour
    {
        private GameState _currentGameState;

        public void ChangeState(GameState gameState)
        {
            _currentGameState?.Exit();

            _currentGameState = gameState;
            _currentGameState.gameStateChanger = this;
            _currentGameState.Enter();
        }
    }
}