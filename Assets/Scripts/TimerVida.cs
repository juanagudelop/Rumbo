using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TimerVida : MonoBehaviour
{
    public float timerCount = 0f;
    [SerializeField] public float maxTime = 1200f;
    private Image barraTiempo;

    void Start()
    {
        GameObject objBarraTiempo = GameObject.FindWithTag("barraTiempo");
        if (objBarraTiempo)
        {
            barraTiempo = objBarraTiempo.GetComponent<Image>();
        }

    }


    void Update()
    {
        timerCount += Time.deltaTime;

        barraTiempo.fillAmount = timerCount / maxTime;
        // Debug.Log(barraTiempo.fillAmount);
        if (timerCount >= maxTime) { 
            SceneManager.LoadScene("Etapa-Niño"); 
        }
    }
}

