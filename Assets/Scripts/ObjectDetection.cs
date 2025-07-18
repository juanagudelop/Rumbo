using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public GameObject panelDialogEnchufe;
    public Animator animator;

    public RandomEvent randomEvent;// Asigna esto desde el Inspector de Unity
    public PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
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
    }
    void OnTriggerExit2D(Collider2D Collision)
    {
        if (Collision.CompareTag("enchufe"))
        {
            Debug.Log("Está lejos del enchufe");
            panelDialogEnchufe.SetActive(false);
        }
    }


    void Update()
    {
        if (panelDialogEnchufe.activeSelf && Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isElectrocuted", true);
            playerController.ReceiveDamage(0.5f);
        }
    }

    // Se cambió a OnTriggerEnter2D porque tu juego usa físicas 2D (Rigidbody2D en PlayerController).
    
   
}

