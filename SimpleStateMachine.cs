using System;
using System.Collections.Generic;

namespace OliveGameStudio.Core.StateMachine
{
    public class SimpleStateMachine<T> : IStateMachine<T> where T : Enum
    {
        public T CurrentState { get; private set; }

        public TimeSpan TimeInState { get; private set; }

        readonly Dictionary<T, Action<TimeSpan>> _stateUpdate = new Dictionary<T,Action<TimeSpan>>();

        public SimpleStateMachine(T initialState)
        {
            CurrentState = initialState;
        }

        public void AddUpdate(T state, Action<TimeSpan> action)
        {
            _stateUpdate.Add(state, action);
        }

        public void ChangeState(T state)
        {
            CurrentState = state;
            TimeInState = TimeSpan.Zero;
            Changed.Invoke(CurrentState);
        }

        public event Action<T> Changed = delegate { };

        public void Update(TimeSpan time)
        {
            TimeInState += time;
            if (_stateUpdate.ContainsKey(CurrentState))
            {
                _stateUpdate[CurrentState].Invoke(time);
            }
        }
    }
}
