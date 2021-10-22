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
    public RectTransform winsCanvas;
    int playerWins;
    public Text playerWinCount;
    public RectTransform loseCanvas;
    int enemyWins;
    public Text enemyWinCount;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
        introTimer = introTime;
        playerWins = 0;
        enemyWins = 0;
    }

    private void Start()
    {
        playerHand = new Hand();
        cpuHand = new Hand(new Vector2(-6, 4));
    }

    public void setObject(Hand hand)
    {

    }

    public void setObject(Text hand)
    {

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

        }

        if (playerTurn && !playerHand.isTransitioning() && (playerHand.getScore() > 21 || !playerHand.canDrawCards()))
        {
            playerTurn = false;
        }

        if (!playerTurn && !playingGame && !discard.isMoving() && Input.GetKeyDown(KeyCode.Return))
        {
            discard.Add(addLists(playerHand.GetCards(), cpuHand.GetCards()));
            winsCanvas.gameObject.SetActive(false);
            loseCanvas.gameObject.SetActive(false);

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
                if (!cpuHand.isTransitioning() && cpuHand.canDrawCards() && shouldDraw())
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
                        evaluateOutcome();
                    }
                }
            }
        }
    }

    bool shouldDraw()
    {
        return cpuHand.HandCount() < 2 // Always draw at least two cards
            || (cpuHand.getScore() < 16 && !(playerHand.getScore() > 21))
            || (playerHand.getScore() <= 19 && cpuHand.getScore() < playerHand.getScore());
    }

    void evaluateOutcome()
    {
        if (playerHand.getScore() > 21 && cpuHand.getScore() <= 21)
        {
            loseCanvas.gameObject.SetActive(true);
            enemyWins++;
            enemyWinCount.text = "Wins: "+enemyWins;
        } else if (playerHand.getScore() > 21 && cpuHand.getScore() > 21)
        {
            loseCanvas.gameObject.SetActive(true);
        } else if (playerHand.getScore() <= 21 && cpuHand.getScore() > 21)
        {
            winsCanvas.gameObject.SetActive(true);
            playerWins++;
            playerWinCount.text = "Wins: " + playerWins;
        } else // if (playerHand.getScore() <= 21 && cpuHand.getScore() <= 21)
        {
            if (playerHand.getScore() > cpuHand.getScore())
            {
                winsCanvas.gameObject.SetActive(true);
                playerWins++;
                playerWinCount.text = "Wins: " + playerWins;
            } else
            {
                loseCanvas.gameObject.SetActive(true);
                enemyWins++;
                enemyWinCount.text = "Wins: " + enemyWins;
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
