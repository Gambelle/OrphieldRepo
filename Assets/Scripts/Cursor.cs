using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public Texture2D buttonTexture;
    public Texture2D defaultTexture;
    public CursorMode curMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultTexture, hotspot, curMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseOver()
    {
        if(gameObject.tag == "button")
        {
            Cursor.SetCursor(buttonTexture, hotspot, curMode);
        }
    }
    void OnMouseExit()
    {
        Cursor.SetCursor(defaultTexture, hotspot, curMode);
    }
}
