using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mensajero : MonoBehaviour
{
    public static Mensajero mensajero;
    public float scoreMJ;

    private void Start()
    {
        if (mensajero == null)
        {
            mensajero = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

}
