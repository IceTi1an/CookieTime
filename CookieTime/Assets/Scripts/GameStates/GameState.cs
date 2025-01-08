namespace Game
{
    public abstract class GameState
    {
        public GameStateChanger gameStateChanger;

        public void SetGameStateChanger(GameStateChanger _gameStateChanger)
        {
            gameStateChanger = _gameStateChanger;
        }
        public virtual void Enter()
        {

        }
        public virtual void Process()
        {

        }
        public virtual void Exit()
        {

        }
    }
}