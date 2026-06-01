using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;
using System.Text;

public class MainMenu : MonoBehaviour
{
    public void ToGame()
    {
        SceneManager.LoadScene("RunnerTests");
    }
    public void Inicio()
    {
        SceneManager.LoadScene("Inicio");
        print("cargando la escena inicio");
    }
    public void Instructivo()
    {
        SceneManager.LoadScene("Instructivo");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Manager");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
