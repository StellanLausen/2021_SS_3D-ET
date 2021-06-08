using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;
    private int id;
    private void Awake()
    {
        current = this;
    }
    
    public event Action<int> onSwitchChange;
    public void switchChange(int id)
    {
        if(onSwitchChange != null)
        {
            onSwitchChange(id);
        }
    }
}
