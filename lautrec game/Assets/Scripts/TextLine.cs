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
     Text text;
     Color target = Color.red;
 
     void Awake()
     {
         text = GetComponent<Text>();
         Debug.Log("text vie");
     }
 
     void Update()
     {
         if (text)
             text.color = Vector4.MoveTowards(text.color, target, Time.deltaTime * 10);
     }
 
     public void OnPointerClick(PointerEventData eventData) // 3
     {
         print("I was clicked");
         target = Color.blue;
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