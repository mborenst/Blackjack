using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    // Deck variables
    private List<Card> deck = new List<Card>();
    private string[] suits = { "Heart", "Diamond", "Spade", "Club" };

    // Graphic things
    //private GameObject go;
    private SpriteRenderer deckPile;
    public Sprite[] spades;
    public Sprite[] clubs;
    public Sprite[] diamonds;
    public Sprite[] hearts;
    public Sprite back;

    void Awake()
    {
        GameController.instance.deck = this;
    }

    void Start()
    {
        //go = new GameObject("Card Deck");
        this.name = "Card Deck";
        deckPile = GetComponent<SpriteRenderer>();
        deck = new List<Card>();
    }

    public void setImage(Sprite image)
    {
        back = image;
        foreach (Card card in deck)
        {
            card.back = image;
        }
        deckPile.sprite = image;
    }

    public SpriteRenderer getCardBack()
    {
        return deckPile;
    }

    public void NewShuffledDeck()
    {
        NewDeck();
        ShuffleDeck();
        // Destroy(new GameObject());
    }

    public void NewDeck()
    {
        deck = new List<Card>();
        foreach(string suit in suits)
        {
            for (int i = 1; i <= 13; i++)
            {
                deck.Add(new Card(suit, i, back, getCardFront(suit, i)));
            }
        }
    }

    Sprite getCardFront(string suit, int i)
    {
        if (suit.Equals("Club"))
        {
            return clubs[i];
        } else if (suit.Equals("Spade"))
        {
            return spades[i];
        }
        else if (suit.Equals("Diamond"))
        {
            return diamonds[i];
        } else // suit.Equals("Heart")
        {
            return hearts[i];
        }
    }

    internal void updateDeck()
    {
        throw new NotImplementedException();
    }

    public void NewShuffledDeck(List<Card> preexistingCards)
    {
        NewDeck(preexistingCards);
        foreach (Card card in preexistingCards)
        {
            deck.Remove(card);
        }
    }

    public void NewDeck(List<Card> preexistingCards)
    {
        NewDeck();
        foreach(Card card in preexistingCards)
        {
            deck.Remove(card);
        }
    }

    public void ShuffleDeck()
    {
        var rand = new System.Random();
        int m = deck.Count;
        while (m > 0)
        {
            m--;
            int i = rand.Next(m);
            Card temp = deck[m];
            deck[m] = deck[i];
            deck[i] = temp;
        }
    }

    public Card Draw()
    {
        Card temp = deck[0];
        deck.RemoveAt(0);
        return temp;
    }

    public int DeckSize()
    {
        return deck.Count;
    }
}
