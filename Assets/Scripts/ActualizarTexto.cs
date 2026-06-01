using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ActualizarTexto : MonoBehaviour
{
    public Text txScore;
    public Text hightScore;

    private void Start()
    {
        float hs = PlayerPrefs.GetFloat("HS", 0);
        if (hs < Mensajero.mensajero.scoreMJ)
        {
            PlayerPrefs.SetFloat("HS", Mensajero.mensajero.scoreMJ);
        }
        txScore.text = Mensajero.mensajero.scoreMJ + "";
        hightScore.text = PlayerPrefs.GetFloat("HS", 0) + "";
    }
}
