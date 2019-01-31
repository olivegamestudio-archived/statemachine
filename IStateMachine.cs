using System;

namespace OliveGameStudio.Core.StateMachine
{
    public interface IStateMachine<T> where T : Enum
    {
        T CurrentState { get; }

        TimeSpan TimeInState { get; }

        void AddUpdate(T state, Action<TimeSpan> action);

        void ChangeState(T state);

        event Action<T> Changed;

        void Update(TimeSpan time);

    }
}