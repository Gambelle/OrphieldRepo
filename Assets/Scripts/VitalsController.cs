using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalsController : MonoBehaviour
{
    public Text text;
    public Image image;
    private float maxSanity=100f;
    private float currentSanity = 100f;
    private float sanityGain= 10f;
    private float sanityLost = 10f;
    private int sanityFallRate = 10;

    // Update is called once per frame
    void Update()
    {
        if(currentSanity >=maxSanity)
        {
            currentSanity = maxSanity;
            UpdateUI();

        }
        else if (currentSanity<=0)
        {
            currentSanity = 0;
            UpdateUI();
            print("player has died");
        }
        
        currentSanity-= Time.deltaTime / sanityFallRate * 10;
        UpdateUI();

    }
    public void Sanity()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.gameObject.tag == "Gain")
            {
                currentSanity += sanityGain;
                UpdateUI();
                print("Gained");
            }
            else if (hit.collider.gameObject.tag=="Loss")
            {
                currentSanity -= sanityLost;
                UpdateUI();
                print("Lost");
            }
            
        }

    }
    public void UpdateUI()
    {
        text.text = Mathf.RoundToInt(currentSanity).ToString();
        image.fillAmount = currentSanity / 100;

    }
}
