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
    public Text tc;
    public int dialIndex;
    public DialogManager dm;

    Color target = Color.red;
 
     void Awake()
     {
         tc = GetComponent<Text>();
     }
 
     void Update()
     {
         if (tc)
             tc.color = Vector4.MoveTowards(tc.color, target, Time.deltaTime * 10);
     }
 
     public void OnPointerClick(PointerEventData eventData) // 3
     {
         target = Color.blue;
         dm.TextLineClicked(dialIndex);
     }

     public void OnPointerEnter(PointerEventData eventData)
     {
         target = Color.green;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
         target = Color.red;
     }
 }