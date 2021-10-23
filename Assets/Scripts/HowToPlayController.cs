using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayController : MonoBehaviour
{
    public static HowToPlayController instance;

    private int stage = 0;
    private int maxStages = 6;
    public Text titleText;
    public TMP_Text main;

    void Start()
    {
        instance = this;
        stage = 0;
    }

    public void Reset()
    {
        stage = 1;
        updateTextToStep();
    }

    // Update is called once per frame
    public void NextStep()
    {
        if (stage < maxStages)
        {
            stage++;
        }
        updateTextToStep();
    }

    // Update is called once per frame
    public void LastStep()
    {
        if (stage > 1)
        {
            stage--;
        }
        updateTextToStep();
    }

    public void updateTextToStep()
    {
        titleText.text = "How To Play\n("+stage+"/"+maxStages+")";
        if (stage == 1)
            main.text = "Blackjack is a fun game where you chicken your way to 21 without going over. " +
                "You start out with 2 cards and can only see one of the House's hand";
        else if (stage == 2)
            main.text = "The face cards count as 10 points. " +
                "If the card is an Ace, it can count as either an 11 or 1 depending on what's best. " +
                "Otherwise, the card is worth its number in points.";
        else if (stage == 3)
            main.text = "You hit the spacebar to Hit. When you Hit, you draw a card and add it to your score. " +
                "If you go over, you're screwed. Otherwise, you can hit again or pass.";
        else if (stage == 4)
            main.text = "You hit the Return/Enter Key to Pass. When you pass, you commit to your score and see if the house can beat your score. " +
                "When the House passes, your scores are evaluated.";
        else if (stage == 5)
            main.text = "If your score is higher than 21, then you lose by default. " +
                "\nIf the house has more than 21 and you don't you win, again by default." +
                "\nOtherwise if you both have scores of 21 or lower the House matches or beats your score you lose.";
        else if (stage == 6)
            main.text = "If you want to change the card back, when it's your turn hit the escape key and select the card color. " +
                "\nPress escape again in the menu to quit back to the Menu. " +
                "\nThat should be everything! \nGood luck!";
        else
            main.text = "ERROR \nERROR \nERROR";
    }

    public void newGame()
    {
        GameController.instance.startNewGame();
    }
}
