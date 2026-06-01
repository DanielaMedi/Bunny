using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Text;

public class PlayerController : MonoBehaviour
{
    //SPECH
    public PlayerController playerController;


    [SerializeField]
    private string[] m_Keywords;

    private KeywordRecognizer m_Recognizer;


    //MOVE

    public float moveSpeed = 5f;       // Velocidad de movimiento horizontal
    public float forwardSpeed = 10f;   // Velocidad de movimiento hacia adelante
    public float laneDistance = 2.5f;  // Distancia entre las "líneas" o "carriles"
    public float horizontalLimit = 5f; // Limite de movimiento horizontal (izquierda/derecha)
    Animator animBunny;
    //public GameObject imagen;

    private int currentLane = 0;       // -1 = izquierda, 0 = centro, 1 = derecha



    private void Start()
    {
        animBunny = GetComponent<Animator>();
        //imagen.SetActive(false);

        /*InvokeRepeating("MoveLeft",1,2);
        InvokeRepeating("MoveRight",2, 2);*/

        m_Recognizer = new KeywordRecognizer(m_Keywords);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognized;
        m_Recognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        //builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        //builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        //builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        //Debug.Log(builder.ToString());

        switch (args.text)
        {
            case "izquierda":
                Debug.Log("IZQUIERDA");
                MoveLeft();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Fxmuv");
                break;
            case "derecha":
                Debug.Log("DERECHA");
                MoveRight();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Fxmuv");
                break;
            case "menu":
                Debug.Log("mENU");
                GetComponent<MainMenu>().Menu();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Seleccion");
                break;
            case "jugar":
                Debug.Log("play");
                GetComponent<MainMenu>().ToGame();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Seleccion");
                break;
            case "ayuda":
                GetComponent<MainMenu>().Instructivo();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Seleccion");
                break;
            case "salida":
                GetComponent<MainMenu>().Exit();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Seleccion");
                break;
            case "atras":
                Debug.Log("atas");
                GetComponent<MainMenu>().Inicio();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Seleccion");
                break;
            case "reiniciar":
                Debug.Log("funciona el atras");
                GetComponent<MainMenu>().ToGame();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Seleccion");
                break;

        }
    }


    void Update()
    {
        // Movimiento automático hacia adelante en el eje Z
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime);

        // Limitar el movimiento horizontal dentro de los límites especificados
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -horizontalLimit, horizontalLimit);
        transform.position = position;

        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }

    // Método para moverse a la izquierda
    public void MoveLeft()
    {
        Debug.Log("MoverseIzquierda");
        if (currentLane > -1) // Solo moverse si no está ya en el límite izquierdo
        {
            currentLane--;
            Vector3 newPosition = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);
            StartCoroutine(SmoothMove(newPosition));
            animBunny = GetComponent<Animator>();
        }
    }

    // Método para moverse a la derecha
    public void MoveRight()
    {
        Debug.Log("MoverseDerecha");
        if (currentLane < 1) // Solo moverse si no está ya en el límite derecho
        {
            currentLane++;
            Vector3 newPosition = new Vector3(currentLane * laneDistance, transform.position.y, transform.position.z);
            StartCoroutine(SmoothMove(newPosition));
            animBunny = GetComponent<Animator>();
        }
    }

 
    // Movimiento suave hacia la nueva posición horizontal (izquierda/derecha)
    private IEnumerator SmoothMove(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = 0.2f; // Duración del movimiento suave
        Vector3 targetPosition2 = targetPosition;

        Vector3 startPosition = transform.position;
        while (elapsedTime < duration)
        {
            targetPosition2.z = playerController.transform.position.z;
            transform.position = Vector3.Lerp(startPosition, targetPosition2, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "piedra")
        {
            Muerte();
        }
        if (collision.gameObject.tag == "zanahoria")
        {
            GetComponent<Score>().score += 10;
            Destroy(collision.gameObject);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Fxrecord");
        }

    }
    private void Muerte()
    {
        GetComponent<Score>().OnDeath();
        animBunny.SetTrigger("Die");
        moveSpeed = 0;
        forwardSpeed = 0;
        laneDistance = 0;
        
    }
}
