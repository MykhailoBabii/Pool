using System.Collections;
using System.Collections.Generic;
using Core.States;
using UnityEngine;

namespace Game.States
{
    public class GameOverState : BaseState<ApplicationStates>
    {
        public override ApplicationStates State => ApplicationStates.GameOver;
        private IStateMachine<ApplicationStates> _applicationStateMachine;
        private readonly ITableController _tableController;

        public GameOverState(
            IStateMachine<ApplicationStates> applicationStateMachine,
            ITableController tableController)
        {
            _applicationStateMachine = applicationStateMachine;
            _tableController = tableController;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {

        }
    }

}

