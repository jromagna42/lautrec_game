using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenuAttribute(fileName = "NewDialog", menuName = "Dialog")]
public class Dialogs : ScriptableObject
{
    public Sprite plus;
    public Sprite minus;

    public GameObject player;
    public GameObject[] speakers = new GameObject[2];

    [Serializable]
    public struct DialogTag
    {
        public string tagName;
        public bool tagSet;
    }

    [SerializeField]
    public List<DialogTag> tagList = new List<DialogTag>();

    [HideInInspector]
    public List<DialogContainer> DList = new List<DialogContainer>();

    // public struct dialBox
    // {
    //     public string[] text;
    //     public DialogTag[] blockFlag;
    //     public DialogTag[] setFlag;
    //     public bool readOnce;
    //     public bool alreadyRead;
    //     public bool showDial;
    //     public bool showBlock;
    //     public bool showSet;


    //     public dialBox(string[] i,DialogTag[] j,DialogTag[] k,bool l,bool m, bool n, bool o, bool p)
    //     {
    //         this.text = i;
    //         this.blockFlag = j;
    //         this.setFlag = k;
    //         this.readOnce = l;
    //         this.alreadyRead = m;
    //         this.showDial = n;
    //         this.showBlock = o;
    //         this.showSet = p;
    //     }

    // }


}
