using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    private List<Card> deck;
    private string[] suits = { "Heart", "Diamond", "Spade", "Club" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NewShuffledDeck()
    {
        NewDeck();
        ShuffleDeck();
    }

    public void NewDeck()
    {
        deck = new List<Card>();
        foreach(string suit in suits)
        {
            for (int i = 1; 1 < 13; i++)
            {
                deck.Add(new Card(suit, i));
            }
        }
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
