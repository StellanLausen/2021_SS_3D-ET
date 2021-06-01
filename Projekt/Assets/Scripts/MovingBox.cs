using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox : MonoBehaviour
{
    private Vector3 pos1 = new Vector3(3f, 1f, 4f);
    private Vector3 pos2 = new Vector3(3f, 4f, 4f);

    private Boolean pos1Bool = false;
    private Boolean pos2Bool = true;
    void Update()
    {
        if (pos1Bool)
        {
            transform.position = Vector3.MoveTowards(transform.position,pos2, 1f * Time.deltaTime);
            if (transform.position == pos2)
            {
                pos1Bool = false;
                pos2Bool = true;
            }
        }

        if (pos2Bool)
        {
            transform.position = Vector3.MoveTowards(transform.position,pos1, 1f * Time.deltaTime);
            if (transform.position == pos1)
            {
                pos1Bool = true;
                pos2Bool = false;
            }
        }
    }
}
