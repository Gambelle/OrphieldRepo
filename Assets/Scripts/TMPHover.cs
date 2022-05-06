using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TMPHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private Text myText;

    void Start()
    {
        myText = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        myText.fontSize = 250;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        myText.fontSize = 180;
    }
}
