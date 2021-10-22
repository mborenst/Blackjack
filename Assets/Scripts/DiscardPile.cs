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
            end.Add(new Vector2(pos.x + (rand.Next() -.5f), pos.y + (rand.Next()-.5f)));
        }
    }

    public void UpdateCards()
    {
        float t = movingTime / 60f;
        for (int i = cards.Count-cardsToMove; i < cards.Count; i++)
        {
            int place = i - (cards.Count - cardsToMove);
            Vector2 trans = new Vector2(end[place].x - ((end[place].x - start[place].x) * t),
                end[place].y - ((end[place].y - start[place].y) * t));
            // move cards
        }
    }

    public bool isMoving()
    {
        return movingTime > 0;
    }

    public void DestroyCards()
    {
        foreach(Card card in cards)
        {
            Destroy(card.GetGameObject());
        }
    }
}
