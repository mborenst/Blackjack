using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public void doStuff(Sprite image)
    {
        GameController.instance.beginNewGame(image);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
