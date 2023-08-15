using System;

public interface IPoolsObject<T> 
{
    Action<T> OnDisableObject { get; set; }
}