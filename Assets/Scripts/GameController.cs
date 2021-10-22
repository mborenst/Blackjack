using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Static Instance
    public static GameController instance;

    // Intro timer
    public float introTime;
    private float introTimer;
    public SpriteRenderer introGraphic;
    public Text introText;

    // BlackJack things
    internal bool playingGame = false;
    public DiscardPile discard;
    public CardDeck deck;
    private Hand playerHand;
    private Hand cpuHand;
    private bool playerTurn;
    public Text playerScore;
    public Text enemyScore;
    bool drawingInitialCards;


    // Animation Things

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
        introTimer = introTime;
    }

    private void Start()
    {
        playerHand = new Hand();
        cpuHand = new Hand(new Vector2(-6, 4));
        // Debug.Log(playerHand);
    }

    public void beginNewGame(Sprite image)
    {
        deck.setImage(image);
        deck.NewShuffledDeck();
        deck.transform.position = new Vector2(-8, 1.75f);
        playerHand = new Hand();
        cpuHand = new Hand(new Vector2(-6, 4));
        playerTurn = true;
        playingGame = true;
        playerScore.text = "Player Score: \n" + playerHand.getScore();
        drawingInitialCards = true;
        playerHand.add(deck.Draw());
        cpuHand.add(deck.Draw());
    }

    internal SpriteRenderer getCardBack()
    {
        return deck.getCardBack();
    }

    // Update is called once per frame
    void Update()
    {
        if (introTimer > 0)
        {
            introUpdate();
        }
        if (playerTurn && Input.GetKeyDown(KeyCode.Space) && !playerHand.isTransitioning())
        {
            if (deck.CanDraw())
                playerHand.add(deck.Draw());
            else
            {
                deck.NewShuffledDeck(playerHand.GetCards());
                discard.DestroyCards();
            }
        }
        if (playerTurn && Input.GetKeyDown(KeyCode.Return) && !playerHand.isTransitioning())
        {
            playerTurn = false;
            playerScore.text = "Player Score: \n" + playerHand.getScore(); 
            Debug.Log("You Stopped!");
            Debug.Log("It is no longer the Player's turn...");

        }

        if (playerTurn && !playerHand.isTransitioning() && (playerHand.getScore() > 21 || !playerHand.canDrawCards()))
        {
            playerTurn = false;
            if (playerHand.getScore() > 21)
            {
                Debug.Log("Disaster! You lost!");
            }
            Debug.Log("It is no longer the Player's turn...");
        }

        if (!playerTurn && !playingGame && !discard.isMoving() && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("New Game Begin");
            discard.Add(addLists(playerHand.GetCards(), cpuHand.GetCards()));
            // discard.DestroyCards();

            playerHand = new Hand();
            cpuHand = new Hand(new Vector2(-6, 4));
            playerTurn = true;
            playerScore.text = "Player Score: \n" + playerHand.getScore();
            enemyScore.text = "Enemy Score: \n" + cpuHand.getScore();
            drawingInitialCards = true;
            playerHand.add(deck.Draw());
            cpuHand.add(deck.Draw());
        }
    }

    private void FixedUpdate()
    {
        if (discard.isMoving())
        {
            discard.UpdateCards();
            if (!discard.isMoving())
            {
                playerTurn = true;
                playingGame = true;
            }
        } else {
            //deck.updateDeck();
            if (drawingInitialCards && !playerHand.isTransitioning())
            {
                playerHand.add(deck.Draw());
                drawingInitialCards = false;
            }
            playerHand.updateHand();
            if (playerTurn && !playerHand.isTransitioning())
            {
                playerScore.text = "Player Score: \n" + playerHand.getScore();
            }
            cpuHand.updateHand();
            if (!cpuHand.isTransitioning())
                enemyScore.text = "Enemy Score: \n" + cpuHand.getScore();
            if (!playerTurn && playingGame)
            {
                if (!cpuHand.isTransitioning() && cpuHand.canDrawCards() && (cpuHand.HandCount() < 2 || (cpuHand.getScore() < 16 && !(playerHand.getScore() > 21))))
                {
                    if (deck.CanDraw())
                        cpuHand.add(deck.Draw());
                    else
                    {
                        deck.NewShuffledDeck(addLists(playerHand.GetCards(), cpuHand.GetCards()));
                        discard.DestroyCards();
                    }
                }
                else
                {
                    if (!cpuHand.isTransitioning())
                    {
                        playingGame = false;
                        //evaluateOutcome();
                        Debug.Log("Game Ended!");
                    }
                }
            }
        }
    }

    private List<Card> addLists(List<Card> cards1, List<Card> cards2)
    {
        cards1.AddRange(cards2);
        return cards1;
    }

    void introUpdate()
    {
        introTimer -= 1 / 60f;
        if (introTimer <= introTime/2f)
        {
            introGraphic.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, introTimer / 2.5f);
            introText.color = new Color(255, 255, 255, introTimer / 2.5f);
        }
        if (introTimer <= 0)
        {
            SceneManager.LoadScene("BlackJack");
            // Camera.current.targetDisplay = 2;
            // SceneManager.LoadScene("Menu"); 
            // Actual Command, temporary
        }
    }

    // Changes Scene to How To Play Scene
    void startHowToPlay()
    {

    }

    // Changes Scene to Blackjack Scene
    void startNewGame()
    {
        
    }

    // Changes Scene to Menu Scene
    void startMenu()
    {

    }
}
