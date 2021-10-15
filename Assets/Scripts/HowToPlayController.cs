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

    public void Reset()
    {
        stage = 0;
        casinoMan = casinoTutorialPictures[stage];
    }

    // Update is called once per frame
    public void NextStep()
    {
        if (stage < casinoTutorialPictures.Length-1)
        {
            stage++;
        }
        casinoMan = casinoTutorialPictures[stage];
        // Other Stuff
    }

    // Update is called once per frame
    public void LastStep()
    {
        if (stage > 0)
        {
            stage--;
        }
        casinoMan = casinoTutorialPictures[stage];
        // Other Stuff
    }
}
