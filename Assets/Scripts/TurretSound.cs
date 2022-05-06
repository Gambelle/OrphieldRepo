using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TurretSound : MonoBehaviour
{
    public GameObject playerObj;
    bool audioPlayedOnce;
    AudioSource audioSrc;
    public AudioClip turretSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        audioPlayedOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioPlayedOnce) {
            if (playerObj.transform.position.x >= 28.9)
            {
                audioPlayedOnce = true;
                audioSrc.clip = turretSound;
                audioSrc.Play();
            }
        }
    }
}
