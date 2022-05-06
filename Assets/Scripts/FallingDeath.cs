using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FallingDeath : MonoBehaviour
{
    private static readonly int DEATH_PROPERTY = Animator.StringToHash("Dead");
    [SerializeField]
    public Animator transition;
    public float transitionTime = .25f;
    private bool inTrigger = false;
    private bool die = false;

    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("DeathZone"))
        {
            Debug.Log("You Died");
            die = true;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
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

        if (collider.gameObject.CompareTag("DeathZone"))
        {
            Debug.Log("You Died");
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        }
    }
    
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}

