using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    public string next_scene;
    // Start is called before the first frame update
    public void LoadScene()
    {
        SceneManager.LoadScene(next_scene);
    }
}
