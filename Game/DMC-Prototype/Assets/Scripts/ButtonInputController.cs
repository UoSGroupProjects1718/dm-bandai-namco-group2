using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInputController : MonoBehaviour {

    [Header("Button Components")]
    public int buttonNumberPlayerOne;
    private GameManager GM;
    private AudioSource buttonSound;

    void Start () {
        GM = FindObjectOfType<GameManager>();
        buttonSound = GetComponent<AudioSource>();
	}
	
	
	void Update () {
		
	}

    public void OnMouseUp()
    {
        GM.ButtonCheckerForPlayerOne(buttonNumberPlayerOne);
        buttonSound.Play();
    }
}
