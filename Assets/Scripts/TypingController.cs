using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TypingController : MonoBehaviour
{
    public float delay = 0.1f;
    private string fullText = "Before the silence came...\nThey were greedy.\n\nThey locked up all the forest creatures,\nThey played with forces they didn't understand\n\nThey tried to make a sound weapon with the power of God,\none that would make their army unstoppable...\n\nAnd that weapon, eventually...";
    private string endText = "Broke\nLoose";
    private string currentText = "";
    private string currentEndText = "";

    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            //fullText.Replace(";", "\n");
            currentText = fullText.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(delay);
        }

        for (int i = 0; i <= 1; i++)
        {
            yield return new WaitForSeconds(1);
        }

        for (int i = 0; i <= endText.Length; i++)
        {
            this.GetComponent<Text>().color = Color.red;
            this.GetComponent<Text>().fontSize = 30;
            currentEndText = endText.Substring(0, i);
            this.GetComponent<Text>().text = currentEndText;
            yield return new WaitForSeconds(delay);
        }

        for (int i = 0; i <= 1; i++)
        {
            yield return new WaitForSeconds(1);
        }

        Application.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
