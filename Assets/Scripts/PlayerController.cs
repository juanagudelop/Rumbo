
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isReceivingDamage;
    public GameObject gameOverImage;
    public float speed = 5f;
    public float jumpForce = 3f;
    public float health = 6;
    public bool isGrounded;
    public Animator animator;
    public float damageCooldown = 1f; // Tiempo de espera antes de poder recibir daño nuevamente
    Vector2 PosicionInicio;
    private float VidaMax;

    public float danioPorCiclo = 0.1f;

    private Image barraVidaImage;

    private bool animationDead = false;

    void Start()
    {
        GameObject barraVidaObj = GameObject.FindWithTag("barraVida");
        if (barraVidaObj != null)
        {
            barraVidaImage = barraVidaObj.GetComponent<Image>();
            if (barraVidaImage == null)
            {
                Debug.LogError("El objeto con tag 'barraVida' no tiene componente Image");
            }
        }
        else{Debug.LogError("No se encontró objeto con tag 'barraVida'");}
        rb = GetComponent<Rigidbody2D>();
        VidaMax = health;
    }

    void Update()
    {
        float movimiento = Input.GetAxisRaw("Horizontal");

        if (movimiento > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movimiento < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetFloat("movement", movimiento);
        rb.linearVelocity = new Vector2(movimiento * speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        // animator.SetBool("recibeDanio", isReceivingDamage);
        Cry();
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jumpForce), ForceMode2D.Impulse);
            animator.SetBool("isGrounded", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            isGrounded = true;
            animator.SetBool("isGrounded", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            isGrounded = false;
        }
    }

    public void ReceiveDamage(/*Vector2 direction,*/ float damage)
    {
        if (!isReceivingDamage)
        {
            isReceivingDamage = true;
            barraVidaImage.fillAmount -= damage; // Reduce la salud del jugador y esta variable es el dividendo de la vida maxima que seria la vida maxima.
            // Vector2 rebote = new Vector2(direction.x, 1).normalized;
            // rb.AddForce(rebote * 5, ForceMode2D.Impulse);

            if (health <= 0)
            {
                // Manejar la muerte del jugador
                dead();
            }

            // Restablecer el estado de recibir daño después de un tiempo
            Invoke(nameof(DeactivateDamage), damageCooldown);
        }
    }

    public void Daño()
    {
        if (barraVidaImage != null)
        {
            barraVidaImage.fillAmount -= danioPorCiclo;
            Debug.Log($"¡Daño recibido! Vida actual: {barraVidaImage.fillAmount * 100}%");

            if (barraVidaImage.fillAmount <= 0)
            {
                Debug.Log("¡El bebé ha muerto por falta de cuidados!");
                animator.SetBool("isDead", true);
                // playerController.dead();
                // Aquí puedes añadir lógica de game over
            }
        }
    }
    public void DañoPorNoLimpiar()
    {
        if (barraVidaImage != null)
        {
            barraVidaImage.fillAmount -= danioPorCiclo;
            Debug.Log($"¡Daño recibido! Vida actual: {barraVidaImage.fillAmount * 100}%");
            if (barraVidaImage.fillAmount <= 0)
            {
                dead();
            }
            
        }   
    }

    public void dead()
    {
        if (barraVidaImage.fillAmount <= 0)
        {
            animator.SetBool("isDead", true);
            if (animationDead == true)
            {
                Debug.Log("¡El bebé ha muerto por falta de cuidados!");
                SceneManager.LoadScene("Muerte"); 
            }

            
        }
        
    }

    public void DeactivateDamage()
    {
        isReceivingDamage = false;
        animator.SetBool("isElectrocuted", false);
    }
    public void ModificateState()
    {
        animationDead = true;
        
    }
    public void Respawn()
    {
        rb.position = PosicionInicio;
        animator.SetBool("isDead", false);
        health = VidaMax;

    }

    public void Cry()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("isCrying", true);
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            animator.SetBool("isCrying", false);
        }
    }

    public void hacerLaPopo()
    {
        int numCagar = 2;
        int numeroAleatorio = UnityEngine.Random.Range(1, 10); // Genera un número entre 1 y 9
        Debug.Log("Número aleatorio entero: " + numeroAleatorio);
        if (numCagar == numeroAleatorio)
        {

        }
    }

    
}
