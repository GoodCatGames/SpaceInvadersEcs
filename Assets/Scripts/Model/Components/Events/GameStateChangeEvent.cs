namespace Model.Components.Events
{
    public enum GameStateEnum
    {
        Play,
        Pause,
        GameOver,
        Restart,
        Exit
    }

    public struct GameStateChangeEvent
    {
        public GameStateEnum State;
    }
}