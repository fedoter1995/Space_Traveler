using CustomTools.Observable;
using System;

public interface IHaveHealthPoints
{
    Observable<int> CurrentHealthPoints { get; }
}

