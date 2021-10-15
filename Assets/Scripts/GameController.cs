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

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(instance);
        introTimer = introTime;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (introTimer > 0)
        {
            introUpdate();
        }
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
