using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand 
{
    private Card[] cards;
    float drawingCard = 0;
    Vector2 pos;
    bool showCard;
    Vector2 startPlace;
    Vector2 endPlace;

    public Hand()
    {
        pos = new Vector2(-6, -1);
        this.Start();
    }

    public Hand(Vector2 pos)
    {
        this.pos = pos;
        this.Start();
    }

    public List<Card> GetCards()
    {
        List<Card> ret = new List<Card>();
        foreach (Card card in cards) {
            if (card != null)
            {
                ret.Add(card);
            }
        }
        return ret;
    }

    void Start()
    {
        this.cards = new Card[6];
        startPlace = new Vector2(-8, 1.75f);
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

    public void add(Card card)
    {
        drawingCard = 2;
        card.activate();
        card.show();
        card.setPosition(startPlace);
        endPlace = new Vector2(pos.x + ((HandCount() + 1) * 2), pos.y);
        addCard(card);
        //card.flip();
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

    public bool canDrawCards()
    {
        return HandCount() < 5;
    }

    internal void updateHand()
    {
        if (drawingCard > 1)
        {
            float t = drawingCard - 1;
            Vector2 trans = new Vector2(endPlace.x - ((endPlace.x - startPlace.x)*t),
                endPlace.y - ((endPlace.y- startPlace.y)*t));
            cards[HandCount()-1].setPosition(trans);
            if (drawingCard - 1 / 60f <= 1)
            {
                cards[HandCount() - 1].setPosition(endPlace);
            }
        }
        if (drawingCard > .5 && drawingCard <= 1)
        {
            float t = (drawingCard - .5f) * 2;
            cards[HandCount() - 1].setScale(new Vector3(t*.8f, .8f, 1));
            cards[HandCount() - 1].setPosition(new Vector2(endPlace.x, endPlace.y));
            if (drawingCard - 1/60f <= .5)
            {
                cards[HandCount() - 1].flip();
            }
        }
        if (drawingCard > 0 && drawingCard <= .5)
        {
            float t = drawingCard * 2;
            t = 1 - t;
            cards[HandCount() - 1].setScale(new Vector3(t * 1.6f, 1.6f, 1));
            cards[HandCount() - 1].setPosition(new Vector2(endPlace.x, endPlace.y));
            if (drawingCard - 1 / 60f <= 0)
            {
                cards[HandCount() - 1].setPosition(endPlace);
                cards[HandCount() - 1].setScale(new Vector3(1.6f, 1.6f, 1));
            }
        }
        drawingCard -= 1 / 60f;
    }

    public bool isTransitioning()
    {
        return drawingCard > 0;
    }
}
