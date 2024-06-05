using Game.States;

namespace Core.States
{
    public class StatesInstaller
    {
        private IStateMachine<ApplicationStates> _applicationStateMachine;
        private readonly StartState _startState;
        private readonly ReadyState _readyState;
        private readonly BallsMovingState _ballsMovingState;

        public StatesInstaller(
            StartState startState,
            IStateMachine<ApplicationStates> applicationStateMachine,
            ReadyState readyState,
            BallsMovingState ballsMovingState)
        {
            _startState = startState;
            _applicationStateMachine = applicationStateMachine;
            _readyState = readyState;
            _ballsMovingState = ballsMovingState;
        }

        public void InitStates()
        {
            _applicationStateMachine.InitStates
            (
                _startState,
                _readyState,
                _ballsMovingState
            );
        }

        public void StartApplication()
        {
            _applicationStateMachine.SwitchToState(ApplicationStates.Start);
        }
    }
}
