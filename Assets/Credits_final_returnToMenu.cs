using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits_final_returnToMenu : MonoBehaviour
{
    void Start()
    {
        Invoke("MyLoadingFunction", 35f);
    }
    void MyLoadingFunction()
    {
        SceneManager.LoadScene(0);
    }

}



