using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class TextoTemblorosoConSonido : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AudioClip clickClip;
    public AudioClip seleccion;  // Nuevo sonido para OnPointerEnter

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private bool isShaking = false;
    private Button button;
    private AudioSource audioSource;
    private bool hasPlayedSeleccionSound = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        button = GetComponent<Button>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isShaking = true;
        StartCoroutine(Temblar());

        if (!hasPlayedSeleccionSound && seleccion != null)
        {
            audioSource.clip = seleccion;
            audioSource.Play();
            hasPlayedSeleccionSound = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isShaking = false;
        rectTransform.anchoredPosition = originalPosition;
        hasPlayedSeleccionSound = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickClip != null)
        {
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempSource.clip = clickClip;
            tempSource.volume = 0.8f;
            tempSource.Play();

            DontDestroyOnLoad(tempAudio);
            Destroy(tempAudio, clickClip.length);
        }

        if (button != null)
        {
            button.onClick.Invoke();
        }
    }

    System.Collections.IEnumerator Temblar()
    {
        while (isShaking)
        {
            float offsetX = Random.Range(-2f, 2f);
            float offsetY = Random.Range(-2f, 2f);
            rectTransform.anchoredPosition = originalPosition + new Vector2(offsetX, offsetY);
            yield return new WaitForSeconds(0.02f);
        }

        rectTransform.anchoredPosition = originalPosition;
    }
}
