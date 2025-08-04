using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectDetection : MonoBehaviour
{
    public GameObject panelDialogEnchufe;
    public Animator animator;

    public RandomEvent randomEvent;// Asigna esto desde el Inspector de Unity
    public PlayerController playerController;
    public TimerVida timerVida; 
    private bool isNearToDoor = false;
    private bool isNearToBed = false;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        timerVida = GetComponent<TimerVida>();

        randomEvent = FindFirstObjectByType<RandomEvent>();
        panelDialogEnchufe.SetActive(false);


    }

    void OnTriggerEnter2D(Collider2D Collision)
    {
        // Es mejor usar Tags para identificar objetos. Crea un Tag "Enchufe" y asígnaselo al objeto del enchufe.
        if (Collision.CompareTag("enchufe"))
        {
            Debug.Log("Está cerca del enchufe");
            panelDialogEnchufe.SetActive(true);
        }

        if (Collision.CompareTag("puerta"))
        {
            Debug.Log("Está cerca de la puerta");
            isNearToDoor = true;
        }
        if (Collision.CompareTag("cuna"))
        {
            Debug.Log("Está cerca de la cuna");
             isNearToBed = true;
        }
    }
    void OnTriggerExit2D(Collider2D Collision)
    {
        if (Collision.CompareTag("enchufe"))
        {
            Debug.Log("Está lejos del enchufe.");
            panelDialogEnchufe.SetActive(false);
        }
        if (Collision.CompareTag("puerta"))
        {
            Debug.Log("Está lejos de la puerta.");
            isNearToDoor = false;
        }
    }


    void Update()
    {
        if (panelDialogEnchufe.activeSelf && Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isElectrocuted", true);
            playerController.ReceiveDamage(0.5f);
        }

        if (isNearToDoor == true && Input.GetKey(KeyCode.LeftControl))
        {
            SceneManager.LoadScene("evento-raptado");
        }
        if (isNearToBed == true && Input.GetKey(KeyCode.LeftControl) && timerVida.timerCount >= timerVida.maxTime)
        {
            SceneManager.LoadScene("evento-raptado");
        }
    }

    // Se cambió a OnTriggerEnter2D porque tu juego usa físicas 2D (Rigidbody2D en PlayerController).
    
   
}

