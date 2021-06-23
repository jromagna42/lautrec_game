using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 1
using UnityEngine.UI;

public class TextLine : MonoBehaviour
    , IPointerClickHandler // 2
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public Text textComponent;
    public int dialIndex;
    public DialogManager dm;

    Color targetColor;
    public Color baseColor;
    public Color overColor;
    public Color clickColor;
    

    private void Start() {
        targetColor = baseColor;
    }

    void Update()
    {
        if (textComponent)
            textComponent.color = Vector4.MoveTowards(textComponent.color, targetColor, Time.deltaTime * 10);
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        targetColor = clickColor;
        dm.TextLineClicked(dialIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetColor = overColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetColor = baseColor;
    }
}