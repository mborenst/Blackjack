using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    // Outside Variables
    private Rigidbody2D self;
    private GameObject go;
    private SpriteRenderer sprite;
    private Sprite face;
    public Sprite back;

    // Inside Variables
    public int number;
    public string suit;
    public bool faceUp = false;

    public Card(string suit, int number, Sprite back, Sprite front)
    {
        this.suit = suit;
        this.number = number;
        this.back = back;
        this.face = front;
    }

    internal void setPosition(Vector2 pos)
    {
        sprite.transform.position = pos;
    }

    internal Vector2 getPosition()
    {
        return sprite.transform.position;
    }

    public void deValue()
    {
        sprite.sortingLayerName = "Deck and Frames";
    }

    internal int getScore()
    {
        if (number > 10)
            return 10;
        return number;
    }

    public Card()
    {
        number = 1;
        suit = "Spade";
    }

    public void activate()
    {
        go = new GameObject(FormalCardName());
        sprite = go.AddComponent<SpriteRenderer>();
        if (faceUp)
            sprite.sprite = face;
        else
            sprite.sprite = this.back;
        sprite.sortingLayerName = "Cards";
        sprite.sortingOrder = 1;
        sprite.transform.position = new Vector2(-8, 2);
        sprite.transform.localScale = new Vector3(.8f, .8f, 1);
        go.SetActive(false);
    }

    public void setScale(Vector3 scale)
    {
        sprite.transform.localScale = scale;
    }

    public GameObject GetGameObject()
    {
        return go;
    }

    public void show()
    {
        go.SetActive(true);
        sprite.sortingLayerName = "Cards";
    }

    public void hide()
    {
        go.SetActive(false);
    }

    public void flip()
    {
        faceUp = !faceUp;
        if (faceUp)
            sprite.sprite = face;
        else
            sprite.sprite = back;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void setCard(string suit, int number)
    {
        this.number = number;
        this.suit = suit;
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
