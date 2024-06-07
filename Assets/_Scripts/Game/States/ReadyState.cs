using System;
using System.Collections;
using System.Collections.Generic;
using Core.States;
using Game.Stick;
using Game.Table;
using UI;
using UnityEngine;

namespace Game.States
{
    public class ReadyState : BaseState<ApplicationStates>
    {
        public override ApplicationStates State => ApplicationStates.Ready;
        private readonly ScreenView _screenView;
        private readonly ScreenTouchHandler _screenTouchHandler;
        private readonly IStickController _stickController;
        private readonly PoolControllerBehaviour _poolControllerBehaviour;

        public ReadyState(
            ScreenView screenView,
            IStickController stickController,
            PoolControllerBehaviour poolControllerBehaviour,
            ScreenTouchHandler screenTouchHandler)
        {
            _screenView = screenView;
            _stickController = stickController;
            _poolControllerBehaviour = poolControllerBehaviour;
            _screenTouchHandler = screenTouchHandler;
        }

        public override void Enter()
        {
            _stickController.InitOnShowComplete(OnShowComplete);
            _stickController.ShowStick();
            _stickController.PrepareOnReady();
            _stickController.SetStartDrawLinePosition();
            _poolControllerBehaviour.TrajectoryDrawer.Show();
            _screenView.ShowLine(true);
        }

        private void OnShowComplete()
        {
            _screenTouchHandler.CanTouch = true;
        }

        public override void Exit()
        {
            
        }
    }

}

