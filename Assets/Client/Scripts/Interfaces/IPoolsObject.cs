using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolsObject<T>
{
    event Action<T> OnDisableEvent;
}