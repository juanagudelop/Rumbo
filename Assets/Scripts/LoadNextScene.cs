using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.anyKeyDown){
        //     int NextScene = SceneManager.GetActiveScene().buildIndex;
        //     ChangeScene(NextScene);        
        // }
    }

    public void ChangeScene(int NextScene){
        
        SceneManager.LoadScene(NextScene);
    }
}
