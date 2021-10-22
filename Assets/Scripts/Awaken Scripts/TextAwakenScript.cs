using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAwakenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        if (this.gameObject.name.Equals("Player Score"))
        {
            index = 0;
        }
        else if (this.gameObject.name.Equals("Enemy Score"))
        {
            index = 1;
        }
        else if (this.gameObject.name.Equals("Player Win Count"))
        {
            index = 2;
        }
        else // if (this.gameObject.name.Equals("Enemy Win Count"))
        {
            index = 3;
        }
        GameController.instance.setObject(this.GetComponent<Text>(), index);
    }
}
