using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTools.Observable
{
    public interface IObservable<T>
    {
        public event Action<T> OnChangeEvent;
    }
}
