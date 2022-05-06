using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    Movement m;
    bool fading;
    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        m = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<Movement>();
        rend = GetComponent<SpriteRenderer>();
        fading = false;

        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
    }

    public IEnumerator FadeIn()
    {
        Debug.Log("FADE IN");
        fading = true;
        for(float f = 0.05f; f <= 1; f += 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        fading = false;
    }

    public IEnumerator FadeOut()
    {
        Debug.Log("FADE OUT");
        fading = true;
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        fading = false;
    }


    public void startFadingIn()
    {
        StartCoroutine("FadeIn");
    }
    
    public void startFadingOut()
    {
        StartCoroutine("FadeOut");
    }
    

    void Update()
    {
        Color c = rend.material.color;
        //Fade in tutorial text if player is close enough
        if (m.getCurrentXPosition() >= (transform.position.x - 6.0f) && m.getCurrentXPosition() <= (transform.position.x + 1.0f) && fading == false && c.a <= 0f)
        {
            //Debug.Log("fading in");
            startFadingIn();
        }
        //Fade out tutorial text if player is far enough
        else if ((m.getCurrentXPosition() < (transform.position.x - 6.0f) || m.getCurrentXPosition() > (transform.position.x + 1.0f)) && fading == false && c.a > 0f)
        {
            //Debug.Log("fading out");
            startFadingOut();
        }
    }

}
