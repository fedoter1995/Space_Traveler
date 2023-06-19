using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolsObject<T> 
{
    Action<T> OnDisableObject { get; set; }
}