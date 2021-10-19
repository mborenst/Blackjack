using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand 
{
    List<Card> cards;
    float drawingCard = 0;
    Vector2 pos;

    public Hand()
    {
        this.Start();
    }

    public Hand(int i)
    {
        this.Start();
    }

    void Start()
    {
        cards = new List<Card>();
        pos = new Vector2(-5, -1);
    }

    public void add(Card card, bool isPlayerDraw)
    {
        if (cards == null)
            cards = new List<Card>{card};
        else
            cards.Add(card);
        if (isPlayerDraw)
            drawingCard = 3;
        else
            drawingCard = 2;
        card.show();
        card.setPosition(new Vector2(pos.x + (cards.Count * 2), pos.y));
        card.flip();
    }

    public int getScore()
    {
        int sum = 0;
        List<Card> aces = new List<Card>();
        if (cards == null)
            return 0;
        foreach (Card card in cards)
        {
            if (!card.getNumber().Equals("Ace"))
                sum += card.getScore();
            else
                aces.Add(card);
        }
        foreach (Card card in aces)
        {
            if (aces.Count > 1)
            {
                if (sum + 11 > 20)
                {
                    sum++;
                }
                else
                {
                    sum += 11;
                }
            }
            else
            {
                if (sum + 11 > 21)
                {
                    sum++;
                }
                else
                {
                    sum += 11;
                }
            }
        }
        return sum;
    }

    internal void updateHand()
    {
        throw new NotImplementedException();
    }
}
