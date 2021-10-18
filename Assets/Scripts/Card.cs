using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Outside Variables
    private Rigidbody2D self;
    private SpriteRenderer face;
    private SpriteRenderer back;
    // Inside Variables
    public int number;
    public string suit;
    public bool faceUp = false;

    public Card(string suit, int number)
    {
        this.suit = suit;
        this.number = number;
    }

    public Card()
    {
        number = 1;
        suit = "Spade";
    }

    public int I { get; }

    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Rigidbody2D>();
        face = GetComponent<SpriteRenderer>();
        back = GameController.instance.getCardBack();
    }

    void setCard(string suit, int number)
    {
        this.number = number;
        this.suit = suit;
        // face = GameController.getFace(cardName());
    }

    void setCard(string suit, string number)
    {
        this.suit = suit;
        this.number = getNumber(number);
    }

    private int getNumber(string number)
    {
        if (number.Equals("Ace"))
            return 1;
        else if (number.Equals("Jack"))
            return 11;
        else if (number.Equals("Queen"))
            return 12;
        else if (number.Equals("King"))
            return 13;
        else
        {
            try
            {
                return Int32.Parse(number);
            }
            catch (FormatException)
            {
                return -1;
            }
        }
    }

    public string FormalCardName()
    {
        return getSuit() + " " + getNumber();
    }

    public string UserCardName()
    {
        return getNumber() + " of " + getSuit() + "s";
    }

    public string getSuit()
    {
        return suit;
    }

    public string getNumber()
    {
        if (number == 1)
            return "Ace";
        else if (number == 11)
            return "Jack";
        else if (number == 12)
            return "Queen";
        else if (number == 13)
            return "King";
        else
            return number + "";
    }

    public override bool Equals(object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Card card = (Card)obj;
            return suit.Equals(card.getSuit()) && number == card.number;
        }
    }
}
