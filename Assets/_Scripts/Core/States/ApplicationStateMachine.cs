using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.States
{
    public interface IStateMachine<T>
    {
        IState<T> Current { get; }
        IState<T> Previous { get; }
        void SwitchToState(T state, bool exitFromPrevious = true);
        void InitStates(params IState<T>[] states);
    }

    public class BaseStateMachine<T> : IStateMachine<T>
    {
        private Dictionary<T, IState<T>> _states = new Dictionary<T, IState<T>>();

        private IState<T> _current;
        private IState<T> _previous;

        public IState<T> Current => _current;

        public IState<T> Previous => _previous;

        public BaseStateMachine(params IState<T>[] states)
        {
            InitStates(states);
        }

        public BaseStateMachine()
        {

        }

        public void SwitchToState(T state, bool exitFromCurrent = true)
        {
            var nextState = _states[state];
            if (exitFromCurrent == true)
            {
                _current?.Exit();
            }

            _previous = _current;

            _current = nextState;
            _current.Prepare();
            _current.Enter();

            Debug.Log($"State {state} OK");
        }

        public void InitStates(params IState<T>[] states)
        {
            foreach (var state in states)
            {
                _states[state.State] = state;
            }
        }
    }
}