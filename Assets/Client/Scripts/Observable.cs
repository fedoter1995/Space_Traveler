using System;

namespace CustomTools.Observable
{
    public class Observable<T> : IObservable<T>
    {
        private T value;
        public event Action<T> OnChangeEvent;
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnChangeEvent?.Invoke(this.value);
            }
        }

        public Observable()
        {
            this.value = default;
        }
        public Observable(T value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

