using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NacimientoScriptt : MonoBehaviour
{
    public Image keySpriteImage;
    public Sprite[] keySprites; // 0: normal, 1: presionado, 2+: explosión

    public Image warningImage;
    public Sprite warningSprite;
    public float explosionFrameRate = 0.05f;
    public float timeLimit = 2.0f;

    List<char> keys = new List<char>("QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());
    public int numberOfLetters = 10;
    private int currentNumberOfLetters;
    char currentKey;

    int successes = 0;
    int failed = 0;

    private float timer = 0.0f;
    private bool isExploding = false;

    void Start()
    {
        warningImage.gameObject.SetActive(false);
        GenerateNewLetter();
    }

    void Update()
    {
        if (currentNumberOfLetters >= numberOfLetters) return;

        timer += Time.deltaTime;

        if (timer >= timeLimit)
        {
            failed++;
            currentNumberOfLetters++;
            StartCoroutine(ExplodeKey());
            if (currentNumberOfLetters < numberOfLetters)
                Invoke(nameof(GenerateNewLetter), keySprites.Length * explosionFrameRate);
            else
                Invoke(nameof(EndGame), keySprites.Length * explosionFrameRate);
        }

        if (Input.inputString.Length > 0 && !isExploding)
        {
            char keyPress = Input.inputString[0];
            if (char.ToUpper(keyPress) == currentKey)
            {
                successes++;
                currentNumberOfLetters++;
                StartCoroutine(CorrectKeyPressed());
            }
            else
            {
                failed++;
                currentNumberOfLetters++;
                StartCoroutine(IncorrectKeyPressed());
            }

            if (currentNumberOfLetters < numberOfLetters)
                Invoke(nameof(GenerateNewLetter), keySprites.Length * explosionFrameRate);
            else
                Invoke(nameof(EndGame), keySprites.Length * explosionFrameRate);
        }
    }

    void GenerateNewLetter()
    {
        int randomIndex = UnityEngine.Random.Range(0, keys.Count);
        currentKey = keys[randomIndex];
        keySpriteImage.sprite = keySprites[0];
        timer = 0.0f;
        isExploding = false;
    }

    IEnumerator ExplodeKey()
    {
        isExploding = true;
        for (int i = 2; i < keySprites.Length; i++)
        {
            keySpriteImage.sprite = keySprites[i];
            yield return new WaitForSeconds(explosionFrameRate);
        }
    }

    IEnumerator CorrectKeyPressed()
    {
        isExploding = true;
        keySpriteImage.sprite = keySprites[1]; // sprite presionado
        yield return new WaitForSeconds(0.1f); // pequeña pausa visual
        yield return StartCoroutine(ExplodeKey());
    }

    IEnumerator IncorrectKeyPressed()
    {
        isExploding = true;
        warningImage.gameObject.SetActive(true);
        warningImage.sprite = warningSprite;

        float shakeDuration = 0.5f;
        float shakeSpeed = 0.05f;
        float shakeMagnitude = 10f;

        Vector3 originalPos = warningImage.rectTransform.localPosition;

        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float x = Mathf.Sin(elapsed * 100f) * shakeMagnitude;
            warningImage.rectTransform.localPosition = originalPos + new Vector3(x, 0, 0);
            elapsed += Time.deltaTime;
            yield return new WaitForSeconds(shakeSpeed);
        }

        warningImage.rectTransform.localPosition = originalPos;
        warningImage.gameObject.SetActive(false);

        yield return StartCoroutine(ExplodeKey());
    }

    void EndGame()
    {
        Debug.Log("Correctas: " + successes);
        Debug.Log("Fallidas: " + failed);

        string mensajeFinal;
        if (successes > failed)
            mensajeFinal = "Sobreviviste al nacimiento";
        else if (successes == failed)
            mensajeFinal = "Quedaste medio bobo al nacer";
        else
            mensajeFinal = "Tan bobo que no pudiste ni nacer";

        keySpriteImage.enabled = false;
        warningImage.enabled = false;

        Debug.Log(mensajeFinal);

        if (successes > failed)
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene + 1);
        }
    }
}
