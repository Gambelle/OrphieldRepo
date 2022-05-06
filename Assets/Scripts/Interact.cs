using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    //Final Level Switches
    public GameObject greenR;//the gameobject you want to disable in the scene
    public GameObject greenL;
    public GameObject redR;
    public GameObject redL;
    public Image oldBG;
    public Image newBG;
    public AudioSource alarm;
    public AudioSource switchS;

    private int hitTimes = 0;
    public Animator transition;
    public float transitionTime = 2f;
    private bool inTrigger = false;


    private static readonly int TRIG2_PROPERTY = Animator.StringToHash("Trigger2");
    [Header("Relations")]
    [SerializeField]
    private Animator animator = null;

    private static readonly int DEFEAT_PROPERTY = Animator.StringToHash("Defeat");
    [SerializeField]
    private Animator animator2 = null;



    public Transform detectInteract;
    public Transform objectHolder;
    public Transform doorDetect;
    public float rayDist;

    private bool isOnR; 
    private bool isOnL; 

    public AudioClip doorOpen;
    public AudioClip pullingAudio;
    AudioSource audioSrc;
    public bool doorOpenPlaying;
    public bool pullingAudioPlaying;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        doorOpenPlaying = false;
        isOnR = true;
        isOnL = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D interactCheck = Physics2D.Raycast(detectInteract.position, Vector2.right * transform.localScale, rayDist);
        RaycastHit2D doorCheck = Physics2D.Raycast(doorDetect.position, Vector2.left * transform.localScale, rayDist);
        
        if (interactCheck.collider != null && interactCheck.collider.tag == "interactableObject")
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (!pullingAudioPlaying)
                {
                    pullingAudioPlaying = true;
                    audioSrc.Stop();
                    audioSrc.loop = false;
                    audioSrc.clip = pullingAudio;
                    audioSrc.Play();
                    StartCoroutine(WaitForSound2(pullingAudio));
                }
                interactCheck.collider.gameObject.transform.parent = objectHolder;
                interactCheck.collider.gameObject.transform.position = objectHolder.position;
                interactCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else
            {
                pullingAudioPlaying = false;
                interactCheck.collider.gameObject.transform.parent = null;
                interactCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
        else if (doorCheck.collider != null && doorCheck.collider.tag == "door")
        {
            if (Input.GetKey(KeyCode.E))
            {
                Debug.Log("TEST 1");
                if (doorOpen == null)
                {
                    Debug.Log("TEST 2");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    if (!doorOpenPlaying)
                    {
                        Debug.Log("TEST 3");
                        Debug.Log(audioSrc.clip);
                        doorOpenPlaying = true;
                        audioSrc.Stop();
                        audioSrc.loop = false;
                        audioSrc.pitch = 1f;
                        audioSrc.clip = doorOpen;
                        audioSrc.Play();
                        Debug.Log(audioSrc.clip);
                        Debug.Log("TEST 4");
                        StartCoroutine(WaitForSound1(doorOpen));
                        Debug.Log(audioSrc.clip);
                        Debug.Log("TEST 5");
                    }
                }
            }
        }
        else if (doorCheck.collider != null && doorCheck.collider.tag == "switchR" && !isOnR)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOnR = !isOnR;
                isOnL = !isOnL;
                switchS.Play();

                greenL.SetActive(isOnL); //set the object to active
                redL.SetActive(!isOnL);

                greenR.SetActive(isOnR);//set the object to disable
                redR.SetActive(!isOnR);

            }
        }
        else if (doorCheck.collider != null && doorCheck.collider.tag == "switchL" && !isOnL)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOnR = !isOnR;
                isOnL = !isOnL;
                alarm.Play();
                switchS.Play();
                hitTimes++;

                greenL.SetActive(isOnL); //set the object to active
                redL.SetActive(!isOnL);

                greenR.SetActive(isOnR);//set the object to disable
                redR.SetActive(!isOnR);
                if(animator!=null)
                {
                    animator.enabled = true;
                    animator.SetBool(TRIG2_PROPERTY, true);
                }
                

            }
        }
        if (hitTimes >= 2)
        {
            oldBG.enabled = false;
            newBG.enabled = true;
            if (animator2 != null)
            {
                animator2.SetBool(DEFEAT_PROPERTY, true);
            }
            StartCoroutine(Credits());

        }
        

        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("donedone"))
        {
            animator.enabled = false;
        }

        // add if statement for door by tag 
    }

    //Wait for door opening sound effect to finish
    public IEnumerator WaitForSound1(AudioClip Sound)
    {
        Debug.Log(audioSrc.clip);
        Debug.Log("TEST 7");
        yield return new WaitUntil(() => audioSrc.isPlaying == false);
        doorOpenPlaying = false;
        Debug.Log(audioSrc.clip);
        Debug.Log("TEST 6");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public IEnumerator WaitForSound2(AudioClip Sound)
    {
        yield return new WaitUntil(() => audioSrc.isPlaying == false);
        //pullingAudioPlaying = false;
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator Credits()
    {
        yield return new WaitForSeconds(15.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



}