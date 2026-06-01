using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class conectiondb : MonoBehaviour
{
    private DateTime currentDateTime;
    public InputField scoreMaximo;
    public InputField score;
    public int id;
    public void Enviar()
    {
        //se llama a la corutina
        StartCoroutine(Upload());
    }
    //se define la corutina

    IEnumerator Upload()
    {
        currentDateTime = DateTime.Now;
        WWWForm form1 = new WWWForm();
        form1.AddField("id",  id.ToString());
        form1.AddField("score", score.text);
        
        UnityWebRequest www = UnityWebRequest.Post("http://localhost/phpmyadmin/index.php?route=/sql&db=bunnyrunning&table=puntuacion&pos=0", form1);
        yield return www.SendWebRequest();

        //if (www.isNetworkError || www.isHttpError)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Form upload complete!");
        //}
    }
}
