using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayController : MonoBehaviour
{
    public static HowToPlayController instance;
    public SpriteRenderer[] casinoTutorialPictures;

    private int stage;
    private SpriteRenderer casinoMan;
    

    void Awake()
    {
        instance = this;
    }

    void Reset()
    {
        stage = 0;
        casinoMan = casinoTutorialPictures[stage];
    }

    // Update is called once per frame
    void nextStep()
    {
        stage++;
        casinoMan = casinoTutorialPictures[stage];
        // Other Stuff
    }

    // Update is called once per frame
    void lastStep()
    {
        stage--;
        casinoMan = casinoTutorialPictures[stage];
        // Other Stuff
    }
}
