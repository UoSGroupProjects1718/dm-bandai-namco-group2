using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	[Header ("Banner Button Components")]
	public SpriteRenderer[] buttonColours;
    private int colourSelect;

    [Header("Banner Button Varibles")]
    public float buttonLitTime;
    private float stayLitCounter; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ButtonLitFunction();

	}

    public void StartGame()
    {
        colourSelect = Random.Range(0,buttonColours.Length);

        buttonColours[colourSelect].color = new Color(buttonColours[colourSelect].color.r, buttonColours[colourSelect].color.g, buttonColours[colourSelect].color.b, 1f);

        stayLitCounter = buttonLitTime; 
    }

    public void ButtonLitFunction()
    {
        if(stayLitCounter > 0)
        {
            buttonLitTime -= Time.deltaTime; 
        }else if(stayLitCounter < 0)
        {
            buttonColours[colourSelect].color = new Color(buttonColours[colourSelect].color.r, buttonColours[colourSelect].color.g, buttonColours[colourSelect].color.b, 0.5f);
        }
    }
}
