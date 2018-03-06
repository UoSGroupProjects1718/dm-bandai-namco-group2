using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class LoadSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadMainGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadHowToPlayScreen()
    {
        SceneManager.LoadScene(2); 
    }
}
