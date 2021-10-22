using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckAwakenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.setObject(this.GetComponent<CardDeck>());
    }
}
