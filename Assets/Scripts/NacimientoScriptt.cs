using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NacimientoScriptt : MonoBehaviour
{
    private TMP_Text keyToPress;
    List<Char> keys = new List<Char>("QWERTYUIOPASDFGHJKLÑZXCVBNM1234567890".ToCharArray());
    public int numberOfLetters = 10;
    private int currentNumberOfLetters;
    char currentKey;

    int successes = 0;
    int failed = 0;

    private float timeLimit = 1.0f; // Tiempo límite de 1 segundo
    private float timer = 0.0f;

    void Start()
    {
        GameObject textObject = GameObject.FindWithTag("PresionarTecla");
        if (textObject != null)
        {
            keyToPress = textObject.GetComponent<TextMeshProUGUI>();
            GenerateNewLetter();
        }
        else
        {
            Debug.LogError("No se encontró un objeto con el tag 'PresionarTecla'.");
        }
    }

    void Update()
    {
        if (currentNumberOfLetters >= numberOfLetters) return;

        timer += Time.deltaTime;

        if (timer >= timeLimit)
        {
            failed++;
            Debug.Log(failed);
            Debug.Log("Failed (Timeout)");
            currentNumberOfLetters++;
            timer = 0.0f;

            if (currentNumberOfLetters < numberOfLetters)
            {
                GenerateNewLetter();
            }
            else
            {
                EndGame();
            }
        }

        if (Input.inputString.Length > 0)
        {
            char keyPress = Input.inputString[0];
            if (char.ToUpper(keyPress) == currentKey)
            {
                successes++;
                Debug.Log(successes);
                Debug.Log("Correct");
                currentNumberOfLetters++;
                timer = 0.0f;
            }
            else
            {
                failed++;
                Debug.Log(failed);
                Debug.Log("Failed");
                currentNumberOfLetters++;
                timer = 0.0f;
            }

            if (currentNumberOfLetters < numberOfLetters)
            {
                GenerateNewLetter();
            }
            else
            {
                EndGame();
            }
        }
    }
    void GenerateNewLetter()
    {
        int randomIndex = UnityEngine.Random.Range(0, keys.Count);
        currentKey = keys[randomIndex];
        keyToPress.text = currentKey.ToString();
        timer = 0.0f; // Reiniciar el temporizador
    }

    void EndGame()
    {
        Debug.Log("Correctas: " + successes);
        Debug.Log("Fallidas: " + failed);
        if (successes > failed)
        {
            keyToPress.text = "Sobreviviste al nacimiento";
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene + 1);
        }
        else if (successes == failed)
        {
            keyToPress.text = "Quedaste medio bobo al nacer";
        }
        else
        {
            keyToPress.text = "tan bobo que no pudiste ni nacer";
        }
    }

}
