using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public GameObject textPanel;
    public Animator animator;

    public RandomEvent randomEvent;// Asigna esto desde el Inspector de Unity

    void Start()
    {
         randomEvent = FindFirstObjectByType<RandomEvent>();
        textPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D Collision)
    {
        // Es mejor usar Tags para identificar objetos. Crea un Tag "Enchufe" y asígnaselo al objeto del enchufe.
        if (Collision.CompareTag("enchufe"))
        {
            Debug.Log("Está cerca del enchufe");
            textPanel.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D Collision)
    {
        if (Collision.CompareTag("enchufe"))
        {
            Debug.Log("Está lejos del enchufe");
            textPanel.SetActive(false);
        }
    }


    void Update()
    {
        if (textPanel.activeSelf && Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isElectrocuted", true);
            randomEvent.DañoPorNoLimpiar();
        }
    }

    // Se cambió a OnTriggerEnter2D porque tu juego usa físicas 2D (Rigidbody2D en PlayerController).
    
   
}

