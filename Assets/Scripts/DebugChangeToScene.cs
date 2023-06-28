using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugChangeToScene
{
    public SceneAsset scene;
    // Start is called before the first frame update
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Start()
    {
        SceneManager.LoadScene("MainMenu");
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
