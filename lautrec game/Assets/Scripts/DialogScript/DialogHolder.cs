using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogHolder : MonoBehaviour
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


    public void StartDialog()
    {

    }
    // Update is called once per frame
    void Update()
    {

    }
}
