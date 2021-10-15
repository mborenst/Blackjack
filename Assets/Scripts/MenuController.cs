using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    public Canvas mainmenu;
    public SpriteRenderer title;
    public Canvas controls;
    public Canvas howToPlay;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        // mainmenu = active
        // title = active
        // controls = inactive
        // howToPlay = inactive

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
