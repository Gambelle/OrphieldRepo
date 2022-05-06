using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickerLights : MonoBehaviour
{
    public bool isFlickering = false;
    public float timeDelay;
    public float lowSpRange;
    public float highSpRange;



    // Update is called once per frame
    void Update()
    {
        if(isFlickering ==false)
        {
            StartCoroutine(FlickeringLight());
        }
        
    }
    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light2D>().enabled = false;
        timeDelay = Random.Range(lowSpRange, highSpRange);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light2D>().enabled = true;
        timeDelay = Random.Range(lowSpRange, highSpRange);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
