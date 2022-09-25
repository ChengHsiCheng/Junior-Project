using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    public bool isDestroy = true;
    void Start()
    {
        if(isDestroy)
        {
            Destroy(gameObject, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
