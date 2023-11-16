using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : MonoBehaviour
{
    public void SonAgua()
    {
        Audio.instance.agua.Play();
    }
}
