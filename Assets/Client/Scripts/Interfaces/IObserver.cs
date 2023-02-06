using System;


namespace CustomTools.Observable
{
    public interface IObserver<T> : IDisposable
    {
        void AddObservable(IObservable<T> observable);
    }
}
