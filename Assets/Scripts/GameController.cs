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
    public CardDeck deck;
    private Hand playerHand;
    private Hand cpuHand;
    private bool playerTurn;


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
        cpuHand = new Hand();
        // Debug.Log(playerHand);
    }

    public void beginNewGame(Sprite image)
    {
        deck.setImage(image);
        deck.NewShuffledDeck();
        deck.transform.position = new Vector2(-8, 1.75f);
        playerHand = new Hand();
        cpuHand = new Hand();
        playerTurn = true;
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
        if (playerTurn && Input.GetKeyDown(KeyCode.Space))
        {
            playerHand.add(deck.Draw(), true);
        }

        if (playerTurn && (playerHand.getScore() > 21 || !playerHand.canDrawCards()))
        {
            playerTurn = false;
            Debug.Log("The Player is Ruined!! It is no longer the Player's turn...");
        }
    }

    private void FixedUpdate()
    {
        //deck.updateDeck();
        //playerHand.updateHand();
        //cpuHand.updateHand();
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
