using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    List<Card> cards;
    int cardsToMove;
    List<Vector2> start;
    List<Vector2> end;
    int movingTime;

    void Start()
    {
        cards = new List<Card>();
        start = new List<Vector2>();
        end = new List<Vector2>();
        cardsToMove = 0;
        movingTime = 0;
    }

    public void Add(List<Card> newCards)
    {
        cards.AddRange(newCards);
        cardsToMove = newCards.Count;
        movingTime = 60;
        var rand = new System.Random();
        Vector2 pos = this.transform.position;
        for (int i = 0; i < cardsToMove; i++)
        {
            end.Add(new Vector2(pos.x + ((rand.Next(100) / 100f) - .5f), 
                pos.y + ((rand.Next(100) / 100f) - .5f)));
            start.Add(newCards[i].getPosition());
            newCards[i].deValue();
        }
    }

    void clearCacheCards()
    {
        for (int i = 0; i < cards.Count-15; i++)
        {
            if (cards[i].GetGameObject() != null)
            {
                Destroy(cards[i].GetGameObject());
            }
        }
    }

    public void UpdateCards()
    {
        float t = movingTime / 60f;
        for (int i = cards.Count-cardsToMove; i < cards.Count; i++)
        {
            int place = i - (cards.Count - cardsToMove);
            Vector2 trans = new Vector2(
                end[place].x - ((end[place].x - start[place].x) * t),
                end[place].y - ((end[place].y - start[place].y) * t));
            // 
            // 
            cards[i].setPosition(trans);
            // cards[i].setPosition(new Vector2(7,1.5f));
        }
        movingTime--;
        if (movingTime <= 0)
        {
            start = new List<Vector2>();
            end = new List<Vector2>();
            if (cards.Count > 15)
                clearCacheCards();
        }
    }

    public bool isMoving()
    {
        return movingTime > 0;
    }

    public void DestroyCards()
    {
        Debug.Log("Destroy Method Called");
        foreach(Card card in cards)
        {
            Destroy(card.GetGameObject());
        }
    }
}
