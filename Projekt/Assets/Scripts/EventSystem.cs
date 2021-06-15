using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem Current;
    private void Awake()
    {
        Current = this;
    }
    
    public event Action<int> ONSwitchChange;
    public void SwitchChange(int id)
    {
        if(ONSwitchChange != null)
        {
            ONSwitchChange(id);
        }
    }
    public event Action<int> ONCheckpointEnter; 
    public void CheckpointEnter(int id)
    {
        if (ONCheckpointEnter != null)
        {
            ONCheckpointEnter(id);
        }
    }
}
