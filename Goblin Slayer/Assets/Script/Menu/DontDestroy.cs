using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
