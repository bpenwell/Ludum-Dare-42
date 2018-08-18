using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {

    public void loadScene(string input)
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        Debug.Log("Loading " + input);
        SceneManager.LoadScene(input);
    }

    public void quitGame(){
        Application.Quit();
    }
}
