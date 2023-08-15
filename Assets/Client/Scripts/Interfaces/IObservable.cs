using System;

namespace CustomTools.Observable
{
    public interface IObservable<T>
    {
        public event Action<T> OnChangeEvent;
    }
}
