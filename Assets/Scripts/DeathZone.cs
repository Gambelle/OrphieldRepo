using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DeathZone : MonoBehaviour
{
    private static readonly int DEATH_PROPERTY = Animator.StringToHash("Dead");
    [SerializeField]
    private Animator animator = null;

    bool isProtected = false;
    bool isUnsafe = false;
    public Animator transition;
    public float transitionTime = 2f;
    private bool inTrigger = false;
    private bool die = false;

    public AudioClip gunshot;
    public AudioSource audioSrc;
    public bool gunShotPlaying = false;
    



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("door"))
        {
            inTrigger = true;
        }

        if (collider.gameObject.CompareTag("SafeZone"))
        {
            isProtected = true;
            Debug.Log("Protected");
        }
        if (collider.gameObject.CompareTag("UnsafeZone"))
        {
            isUnsafe = true;
        }
        if (collider.gameObject.CompareTag("DeathZone"))
        {
                     
            if (isProtected && !isUnsafe)
                Debug.Log("Protected");
            else
            {
                Debug.Log("You Died");
                die = true;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                animator.SetBool(DEATH_PROPERTY, die);
                if (!gunShotPlaying)
                {
                    gunShotPlaying = true;
                    audioSrc.Stop();
                    audioSrc.loop = false;
                    audioSrc.clip = gunshot;
                    audioSrc.Play();
                    StartCoroutine(WaitForSound(gunshot));
                }
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));

            }
                

        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (inTrigger)
        {
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (collider.gameObject.CompareTag("SafeZone"))
        {
            isProtected = true;
        }

        if (collider.gameObject.CompareTag("DeathZone"))
        {
            if (isProtected && !isUnsafe)
                Debug.Log("Protected");
            else
            {
                
                if (!gunShotPlaying)
                {
                    gunShotPlaying = true;
                    audioSrc.Stop();
                    audioSrc.loop = false;
                    audioSrc.clip = gunshot;
                    audioSrc.Play();
                    StartCoroutine(WaitForSound(gunshot));
                }
                
                
                Debug.Log("You Died");
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
                //Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("door"))
        {
            inTrigger = false;
            Debug.Log("out");

        }

        if (collider.gameObject.CompareTag("SafeZone"))
        {
            isProtected = false;
            Debug.Log("Not Protected");
        }
        if (collider.gameObject.CompareTag("UnsafeZone"))
        {
            isUnsafe = false;
        }
    }


        
 
   
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator WaitForSound(AudioClip Sound)
    {
        yield return new WaitUntil(() => audioSrc.isPlaying == false);
        gunShotPlaying = false;
    }
}

