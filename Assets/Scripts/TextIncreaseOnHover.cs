using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextIncreaseOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Text myText;

    void Start()
    {
        myText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myText.fontSize = 70;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myText.fontSize = 50;
    }
}
