using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ManageScene : MonoBehaviour
{
    public int currentScene;
    int nextScene ;

    // Start is called before the first frame update
    void Start()
    {
        // Get index of current scene
        currentScene = SceneManager.GetActiveScene().buildIndex;
        
        // Add 1 to load next scene ahead of current scene
        nextScene = currentScene + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Exit Game
    public void Exit(){

        #if UNITY_EDITOR
        {
            EditorApplication.isPlaying = false;
        }
        #else
        {
            Application.Quit();
        }
        #endif
    }

    // Scene Manager (Switch from one to another)
    public void SwitchScene(){
        SceneManager.LoadScene (nextScene);
    }

    public void Restart(){
        SceneManager.LoadScene (currentScene);
    }
}
