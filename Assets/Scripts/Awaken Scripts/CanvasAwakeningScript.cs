using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAwakeningScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.setObject(this.GetComponent<Canvas>());
    }
}
