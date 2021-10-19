using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayController : MonoBehaviour
{
    public static HowToPlayController instance;

    private int stage = 0;
    private int maxStages = 5;
    public Text titleText;
    public TMP_Text main;

    void Awake()
    {
        instance = this;
        stage = 0;
    }

    public void Reset()
    {
        stage = 0;
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
        if (stage > 0)
        {
            stage--;
        }
        updateTextToStep();
    }

    public void updateTextToStep()
    {
        titleText.text = "How To Play";
    }
}
