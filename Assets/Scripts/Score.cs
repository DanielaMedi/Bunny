using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Score : MonoBehaviour
{
    public GameObject mensajero;

    public float score = 0.0f;

    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;

    public bool isDead = false;

    public Text scoreText;
    public DeathMenu deathMenu;

    private void Start()
    {
        mensajero = GameObject.Find("Mensajero");
    }

    // Update is called once per frame
    void Update ()
    {
        if (isDead)
        {
            return;
        }

        if (score >= scoreToNextLevel)
        {
            LevelUp();
        }

        score += Time.deltaTime * difficultyLevel;
        scoreText.text = ((int)score).ToString();
	}

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
        {
            return;
        }

        scoreToNextLevel *= 2;
        difficultyLevel++;

        //GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);
    }

    public void OnDeath()
    {
        int numeroInt = (int)score;
        mensajero.GetComponent<Mensajero>().scoreMJ = numeroInt;
        isDead = true;
        if (PlayerPrefs.GetFloat("Highscore") < score)
            PlayerPrefs.SetFloat("Highscore", score);
        deathMenu.ToggleEndMenu(score);
        StartCoroutine(Actualizar());
    }

    IEnumerator Actualizar()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/bunny/index.php?score="+score);
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
