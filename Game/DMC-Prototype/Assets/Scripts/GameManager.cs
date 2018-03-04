using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    [Header("Colour Settings for Numbers")]
    public SpriteRenderer[] alpha;
    private int numberSelect;
    private bool isLit;
    private bool isUnlit; 

    [Header("Sequence Settings")]
    public float stayLit;
    private float stayLitCounter;
    public float waitBetweenNumbers;
    private float waitBetweenCounter;
    public List<int> numberSequence;
    private int numberPositionInSequence;

    [Header("Audio Manager")]
    public AudioSource[] numberSounds; 

    private bool isGameActive;
    private int playerInputInSequence;

  

    void Start () {
		
	}
	
	
	void Update () {
        if (isLit)
        {
            stayLitCounter -= Time.deltaTime;

            if (stayLitCounter < 0)
            {
                alpha[numberSequence[numberPositionInSequence]].color = new Color(alpha[numberSequence[numberPositionInSequence]].color.r, alpha[numberSequence[numberPositionInSequence]].color.g, alpha[numberSequence[numberPositionInSequence]].color.b, 0.5f);
                isLit = false;
                numberSounds[numberSequence[numberPositionInSequence]].Stop();
                isUnlit = true;
                waitBetweenCounter = waitBetweenNumbers;
                numberPositionInSequence++;
            }

            if (isUnlit)
            {
                waitBetweenCounter -= Time.deltaTime;

                if (numberPositionInSequence >= numberSequence.Count)
                {
                    isUnlit = false;
                    isGameActive = true; 
                }else
                {
                    if(waitBetweenCounter < 0)
                    {
                        
                        alpha[numberSequence[numberPositionInSequence]].color = new Color(alpha[numberSequence[numberPositionInSequence]].color.r, alpha[numberSequence[numberPositionInSequence]].color.g, alpha[numberSequence[numberPositionInSequence]].color.b, 1f);
                        numberSounds[numberSequence[numberPositionInSequence]].Play();
                        stayLitCounter = stayLit;
                        isLit = true;
                        isUnlit = false; 
                    }
                }
            }
        }
	}

    public void StartGame()
    {
        numberSequence.Clear(); 

        numberPositionInSequence = 0;
        playerInputInSequence = 0; 
        numberSequence.Add(numberSelect); 
        numberSelect = Random.Range(0, alpha.Length);
        alpha[numberSequence[numberPositionInSequence]].color = new Color(alpha[numberSequence[numberPositionInSequence]].color.r, alpha[numberSequence[numberPositionInSequence]].color.g, alpha[numberSequence[numberPositionInSequence]].color.b, 1f);
        numberSounds[numberSequence[numberPositionInSequence]].Play();
        stayLitCounter = stayLit;
        isLit = true; 

    }

    public void ButtonChecker(int whichButton)
    {
        if (isGameActive)
        {



            if (numberSequence[playerInputInSequence] == whichButton)
            {
                Debug.Log("Button Has been pressed");

                playerInputInSequence++; 
                if(playerInputInSequence >= numberSequence.Count)
                {
                    numberPositionInSequence = 0;
                    playerInputInSequence = 0;


                    numberSequence.Add(numberSelect);
                    numberSelect = Random.Range(0, alpha.Length);
                    alpha[numberSequence[numberPositionInSequence]].color = new Color(alpha[numberSequence[numberPositionInSequence]].color.r, alpha[numberSequence[numberPositionInSequence]].color.g, alpha[numberSequence[numberPositionInSequence]].color.b, 1f);
                    numberSounds[numberSequence[numberPositionInSequence]].Play();
                    stayLitCounter = stayLit;
                    isLit = true;

                    isGameActive = false; 
                }
            }
            else
            {
                Debug.Log("Wrong Button");

                isGameActive = false; 
            }

        }
    }
}
