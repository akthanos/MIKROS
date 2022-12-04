using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnKeyDown : MonoBehaviour
{

    public string sceneName = "MainMenu";
    public KeyCode keyCode = KeyCode.Escape;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyCode)) {
            SceneManager.LoadScene(sceneName);
        }
    }
}
