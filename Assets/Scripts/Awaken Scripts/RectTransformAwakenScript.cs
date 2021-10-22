using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformAwakenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.setObject(this.GetComponent<RectTransform>(), this.gameObject.name.Equals("Win Panel"));
        this.gameObject.SetActive(false);
    }
}
