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
    internal bool gameStarted;
    internal bool playingGame = false;
    private DiscardPile discard;
    private CardDeck deck;
    private Hand playerHand;
    private Hand cpuHand;
    private bool playerTurn;
    private Text playerScore;
    private Text enemyScore;
    bool drawingInitialCards;
    private RectTransform winsCanvas;
    int playerWins;
    private Text playerWinCount;
    private RectTransform loseCanvas;
    int enemyWins;
    private Text enemyWinCount;
    Canvas cardSelect;

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
        gameStarted = false;
    }

    public void setObject(Canvas canvas)
    {
        cardSelect = canvas;
    }

    public void setObject(RectTransform panel, bool win)
    {
        if (win)
            this.winsCanvas = panel;
        else
            this.loseCanvas = panel;
    }

    public void setObject(Text text, int value)
    {
        if (value == 0)
            playerScore = text;
        else if (value == 1)
            enemyScore = text;
        else if (value == 2)
            playerWinCount = text;
        else //if (value == 2)
            enemyWinCount = text;
    }

    public void setObject(CardDeck carddeck)
    {
        this.deck = carddeck;
    }

    public void setObject(DiscardPile discardPile)
    {
        this.discard = discardPile;
    }

    public void beginNewGame(Sprite image)
    {
        if (gameStarted)
        {
            deck.setImage(image);
            /*
            deck.NewShuffledDeck();
            deck.transform.position = new Vector2(-8, 1.75f);
            // Change Here
            discard.Add(addLists(playerHand.GetCards(), cpuHand.GetCards()));
            discard.DestroyCards();
            playerHand = new Hand();
            cpuHand = new Hand(new Vector2(-6, 4));
            // Change Ends Here
            playerTurn = true;
            playingGame = true;
            playerScore.text = "Player Score: \n" + playerHand.getScore();
            drawingInitialCards = true;
            playerHand.add(deck.Draw());
            cpuHand.add(deck.Draw());
            gameStarted = true;
            */
        } else {
            deck.setImage(image);
            deck.NewShuffledDeck();
            deck.transform.position = new Vector2(-8, 1.75f);
            playerHand = new Hand();
            cpuHand = new Hand(new Vector2(-6, 4));
            playerTurn = true;
            playingGame = true;
            playerScore.text = "Player Score: \n" + playerHand.getScore();
            enemyScore.text = "Enemy Score: \n" + cpuHand.getScore();
            drawingInitialCards = true;
            playerHand.add(deck.Draw());
            cpuHand.add(deck.Draw());
            playerWins = 0;
            enemyWins = 0;
            gameStarted = true;
        }
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
        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Blackjack")))
        {
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

            if (playerTurn && Input.GetKeyDown(KeyCode.Escape))
            {
                if (cardSelect.gameObject.activeSelf) {
                    gameStarted = false;
                    SceneManager.LoadScene("Menu");
                }
                else
                    cardSelect.gameObject.SetActive(true);
            }
        }
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Blackjack")))
        {
            if (discard.isMoving())
            {
                discard.UpdateCards();
                if (!discard.isMoving())
                {
                    playerTurn = true;
                    playingGame = true;
                }
            }
            else
            {
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
    }

    bool shouldDraw()
    {
        return cpuHand.HandCount() < 2 // Always draw at least two cards
            || (cpuHand.getScore() < 16 && playerHand.getScore() > cpuHand.getScore() && !(playerHand.getScore() > 21))
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
            SceneManager.LoadScene("Menu");
        }
    }

    // Changes Scene to Blackjack Scene
    public void startNewGame()
    {
        SceneManager.LoadScene("Blackjack");
    }

    // Changes Scene to Menu Scene
    public void startMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
