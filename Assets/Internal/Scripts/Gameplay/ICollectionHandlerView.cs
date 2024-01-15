using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ICollectionHandlerView 
{
    public bool CheckKey(int compKey);
    public bool CountCheck(int currentCount);

    public int GetTotal();

    public Objective GetObjective();
}
