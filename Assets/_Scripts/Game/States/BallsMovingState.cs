using Core.States;
using Game.Stick;
using Game.Table;
using UI;

namespace Game.States
{
    public class BallsMovingState : BaseState<ApplicationStates>
    {
        public override ApplicationStates State => ApplicationStates.BallsMoving;
        private readonly ScreenView _screenView;
        private readonly ScreenTouchHandler _screenTouchHandler;
        private readonly ITableController _tableController;
        private readonly IStickController _stickController;
        private readonly PoolControllerBehaviour _poolControllerBehaviour;

        public BallsMovingState(ScreenView screenView, ITableController tableController, IStickController stickController, PoolControllerBehaviour poolControllerBehaviour, ScreenTouchHandler screenTouchHandler)
        {
            _screenView = screenView;
            _tableController = tableController;
            _stickController = stickController;
            _poolControllerBehaviour = poolControllerBehaviour;
            _screenTouchHandler = screenTouchHandler;
        }

        public override void Enter()
        {
            _screenTouchHandler.CanTouch = false;
            _stickController.CalculateHitPower();
            _tableController.StartDetectBallsMoving();
            _stickController.MakeStickDistanceAnimation();
            _poolControllerBehaviour.TrajectoryDrawer.Hide();
            _screenView.ShowLine(false);
        }

        public override void Exit()
        {
            
        }
    }

}

