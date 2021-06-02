using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public static EventSystem current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> gravityChanged;

    public void isgravtiyChanged(int id)
    {
        if (gravityChanged != null)
        {
            gravityChanged(id);
        }
    }
}
