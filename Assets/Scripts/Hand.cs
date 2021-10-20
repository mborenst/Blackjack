using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand 
{
    private Card[] cards;
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
        this.cards = new Card[5];
        pos = new Vector2(-5, -1);
    }

    int HandCount()
    {
        int length = 0;
        while (cards[length] != null)
        {
            length++;
        }
        return length;
    }

    void addCard(Card card)
    {
        cards[HandCount()] = card;
    }

    public void add(Card card, bool isPlayerDraw)
    {
        if (isPlayerDraw)
            drawingCard = 3;
        else
            drawingCard = 2;
        card.activate();
        card.show();
        card.setPosition(new Vector2(pos.x + ((HandCount()+ 1) * 2), pos.y));
        card.flip();
        Debug.Log("Beforehand: " + HandCount());
        addCard(card);
        //cards.AddRange(new List<Card> { card });
        Debug.Log("Afterhand: " + HandCount());
    }

    public int getScore()
    {
        int sum = 0;
        List<Card> aces = new List<Card>();
        if (HandCount() == 0)
            return 0;
        foreach (Card card in cards)
        {
            if (card == null)
                sum += 0;
            else if (!card.getNumber().Equals("Ace"))
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
