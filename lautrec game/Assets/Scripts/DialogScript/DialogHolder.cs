using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 1

public class DialogHolder : MonoBehaviour, IPointerClickHandler
{

    public DialogNameSpace.Dialogs dialog;


    [Serializable]
    public struct talkerStruct
    {
        public Sprite image;
        public string name;

    }

    [HideInInspector]
    [SerializeField]
    public List<talkerStruct> talkerList = new List<talkerStruct>();

    CharPrefabScript cps;


    void Start()
    {
        cps = GetComponent<CharPrefabScript>();
    }

    public void OnPointerClick(PointerEventData eventData) // 3
    {
        Debug.Log("start dialog");
        StartDialog();
    }

    public void StartDialog()
    {
        DialogManager.Instance.gameObject.SetActive(true);
        DialogManager.Instance.DialogSetup(this);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
