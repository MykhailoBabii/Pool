using System.Collections;
using System.Collections.Generic;
using Core.States;
using Game.Stick;
using UI;
using UnityEngine;

namespace Game.States
{
    public class StartState : BaseState<ApplicationStates>
    {
        public override ApplicationStates State => ApplicationStates.Start;
        private IStateMachine<ApplicationStates> _applicationStateMachine;
        private readonly ITableController _tableController;
        private readonly IStickController _stickController;
        private readonly CameraController _cameraController;
        private readonly ScreenView _screenView;


        public StartState(
            ITableController tableController,
            IStateMachine<ApplicationStates> applicationStateMachine,
            CameraController cameraController,
            IStickController stickController,
            ScreenView screenView)
        {
            _tableController = tableController;
            _applicationStateMachine = applicationStateMachine;
            _cameraController = cameraController;
            _stickController = stickController;
            _screenView = screenView;
        }

        public override void Enter()
        {
            _cameraController.AdjustCameraSize();
            _tableController.Init();
            _stickController.Init();
            // _tableController.ResetTable();
            _tableController.SpawnBalls();
            _applicationStateMachine.SwitchToState(ApplicationStates.Ready);
            _screenView.FadeAnimation();
        }

        public override void Exit()
        {

        }
    }

}

