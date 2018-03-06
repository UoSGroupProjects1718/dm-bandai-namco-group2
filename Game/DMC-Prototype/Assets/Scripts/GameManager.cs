using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour {

    [Header("Player Win/Lose Components")]
    public SpriteRenderer playerOneWins;
    public SpriteRenderer playerTwoWins;
    public SpriteRenderer playerOneLoses;
    public SpriteRenderer playerTwoLoses;
    public SpriteRenderer playersdraw; 

    bool player1Correct;
    bool player2Correct;
    bool player1TurnOver;
    bool player2TurnOver;

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
    public bool playSequence; 

    [Header("Audio Manager")]
    public AudioSource[] numberSounds; 

    private bool isGameActive;
    private int playerInputInSequence;

    [Header("Button Settings")]
    public GameObject current_Button;
    public GameObject Reset_Button; 
  

    void Start () {
        playSequence = true;
	}
	
	
	void Update ()
    {
        if (isLit & playSequence)
        {
            /**************
               its lit fam
                   )
                  ) \
                 / ) (
                 \(_)/  
             **************/

            stayLitCounter -= Time.deltaTime;

            if (stayLitCounter < 0)
            {
                alpha[numberSequence[numberPositionInSequence]].color = new Color
                    (alpha[numberSequence[numberPositionInSequence]].color.r, 
                    alpha[numberSequence[numberPositionInSequence]].color.g, 
                    alpha[numberSequence[numberPositionInSequence]].color.b, 
                    0.5f);

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
                }
                else
                {
                    if(waitBetweenCounter < 0)
                    {
                        alpha[numberSequence[numberPositionInSequence]].color = new Color
                            (alpha[numberSequence[numberPositionInSequence]].color.r, 
                            alpha[numberSequence[numberPositionInSequence]].color.g, 
                            alpha[numberSequence[numberPositionInSequence]].color.b, 
                            1f);
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
        // Reset variables for the game
        ResetMatch();

        Destroy(current_Button.gameObject); 
        numberSequence.Clear(); 

        numberPositionInSequence = 0;
        playerInputInSequence = 0; 
        numberSequence.Add(numberSelect); 
        numberSelect = Random.Range(0, alpha.Length);
        alpha[numberSequence[numberPositionInSequence]].color = new Color
            (alpha[numberSequence[numberPositionInSequence]].color.r, 
            alpha[numberSequence[numberPositionInSequence]].color.g,
            alpha[numberSequence[numberPositionInSequence]].color.b,
            1f);
        numberSounds[numberSequence[numberPositionInSequence]].Play();
        stayLitCounter = stayLit;
        isLit = true; 

    }

    /// <summary>
    /// Reset the round back to default
    /// </summary>
    public void ResetMatch()
    {
        // No player has pressed yet
        player1TurnOver = false;
        player2TurnOver = false;

        // No player has correect answer yet
        player1Correct = false;
        player2Correct = false;
    }

    /// <summary>
    /// Player 1 Input method
    /// </summary>
    /// <param name="whichButtonForPlayerOne">The button they pressed</param>
    public void ButtonCheckerForPlayerOne(int whichButtonForPlayerOne)
    {
        // If player1 has already gone, return
        if (player1TurnOver) { return; }

        // If player 1 got the correct answer
        if (numberSequence[playerInputInSequence] == whichButtonForPlayerOne)
        {
            Debug.Log("Button Has been pressed by player 1");

            playerInputInSequence++;
            if (playerInputInSequence >= numberSequence.Count)
            {

                // The player got the full sequence
                player1TurnOver = true;
                player1Correct = true;


                numberPositionInSequence = 0;
                playerInputInSequence = 0;

                numberSequence.Add(numberSelect);
                numberSelect = Random.Range(0, alpha.Length);
                alpha[numberSequence[numberPositionInSequence]].color = new Color(alpha[numberSequence[numberPositionInSequence]].color.r, alpha[numberSequence[numberPositionInSequence]].color.g, alpha[numberSequence[numberPositionInSequence]].color.b, 1f);
                numberSounds[numberSequence[numberPositionInSequence]].Play();
                stayLitCounter = stayLit;
                isLit = true;
            }
        }
        else
        {
            Debug.Log("Wrong Button for Player One");

            // Player 1 hit a wrong button, their turn is now over
            player1TurnOver = true;
            player1Correct = false;
        }

        // If both players have now finished their turn, calculate the winner
        if (player1TurnOver && player2TurnOver)
        {
            FindWinner();
        }
    }

    /// <summary>
    /// Player 2 input method
    /// </summary>
    /// <param name="whichButtonForPlayerTwo">Which button they pressed</param>
    public void ButtonCheckerForPlayerTwo(int whichButtonForPlayerTwo)
    {
        // If player2 has already gone, return
        if (player2TurnOver) { return; }

        // If player 2 got the correct answer
        if (numberSequence[playerInputInSequence] == whichButtonForPlayerTwo)
        {
            Debug.Log("Button Has been pressed by player 2");

            playerInputInSequence++;
            if (playerInputInSequence >= numberSequence.Count)
            {

                // The player got the full sequence
                player2TurnOver = true;
                player2Correct = true;

                numberPositionInSequence = 1;
                playerInputInSequence = 1;


                numberSequence.Add(numberSelect);
                numberSelect = Random.Range(0, alpha.Length);
                alpha[numberSequence[numberPositionInSequence]].color = new Color(alpha[numberSequence[numberPositionInSequence]].color.r, alpha[numberSequence[numberPositionInSequence]].color.g, alpha[numberSequence[numberPositionInSequence]].color.b, 1f);
                numberSounds[numberSequence[numberPositionInSequence]].Play();
                stayLitCounter = stayLit;
                isLit = true;
            }
        }
        else
        {
            Debug.Log("Wrong Button for player two");

            // Player 2 hit a wrong button, their turn is now over
            player2TurnOver = true;
            player2Correct = false;
        }


        // If both players have now played, find the winner
        if (player1TurnOver && player2TurnOver)
        {
            FindWinner();
        }
    }

    /// <summary>
    /// Called once both players have had their turn, finds the winner and 
    /// then resets the match variables.
    /// </summary>
    private void FindWinner()
    {
        if (player1Correct && player2Correct)
        {
            // Draw - both correct
            playerOneWins.color = new Color(playerOneWins.color.r, playerOneWins.color.g, playerOneLoses.color.b, 1f);
            playerTwoLoses.color = new Color(playerTwoLoses.color.r, playerTwoLoses.color.g, playerTwoLoses.color.b, 1f);
            playSequence = false;
            Reset_Button.SetActive(true);
        }

        else if (player1Correct && !player2Correct)
        {
            // Player 1 wins
            playerOneWins.color = new Color(playerOneWins.color.r, playerOneWins.color.g, playerOneLoses.color.b, 1f);
            playerTwoLoses.color = new Color(playerTwoLoses.color.r, playerTwoLoses.color.g, playerTwoLoses.color.b, 1f);
            playSequence = false;
            Reset_Button.SetActive(true);
        }

        else if (!player1Correct && player2Correct)
        {
            // Player 2 wins
            playerTwoWins.color = new Color(playerTwoWins.color.r, playerTwoWins.color.g, playerTwoWins.color.b, 1f);
            playerOneLoses.color = new Color(playerOneLoses.color.r, playerOneLoses.color.g, playerOneLoses.color.b, 1f);
            playSequence = false;
            Reset_Button.SetActive(true);
        }

        else if (!player1Correct && !player2Correct)
        {
            // Draw - both wrong
            playerOneLoses.color = new Color(playerOneLoses.color.r, playerOneLoses.color.g, playerOneLoses.color.b, 1f);
            playerTwoLoses.color = new Color(playerTwoLoses.color.r, playerTwoLoses.color.g, playerTwoLoses.color.b, 1f);
            playSequence = false;
            Reset_Button.SetActive(true);
        }


        // Reset variables for the next match
        ResetMatch();
    }
}
