using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public Text scoreText;
    public Image backgroundImage;

    private bool isShowned = false;

    private float transition = 0.0f;
    Animator animBunny;
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isShowned)
            return;

        transition += Time.deltaTime;
        backgroundImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
	}

    public void ToggleEndMenu(float score)
    {
        Invoke("TerminarPartida", 1.5f);
        Invoke("Cargarinto", 2f);
    }
    
    void Cargarinto()
    {
        SceneManager.LoadScene("Manager");
    }

    private void TerminarPartida()
    {
        gameObject.SetActive(true);
        //scoreText.text = ((int)score).ToString();
        isShowned = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
        animBunny = GetComponent<Animator>();
    }
    public void Instructivo()
    {
        SceneManager.LoadScene("Instructivo");
    }
    public void Exit()
    {
        SceneManager.LoadScene("Exit");
    }
    public void ToGame()
    {
        SceneManager.LoadScene("RunnerTests");
    }
    public void Inicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}
