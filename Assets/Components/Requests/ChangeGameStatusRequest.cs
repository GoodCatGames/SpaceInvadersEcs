namespace SpaceInvadersLeoEcs.Components.Requests
{
    public enum GameStates {
        Play,
        Pause,
        GameOver,
        Restart,
        Exit
    }

    internal struct ChangeGameStateRequest {
        public GameStates State;
    }
}