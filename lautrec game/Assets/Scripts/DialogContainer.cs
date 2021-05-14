using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogContainer : ScriptableObject
{
    public List<string> text = new  List<string>();
    public List<Dialogs.DialogTag> blockFlag = new List<Dialogs.DialogTag>();
    public List<Dialogs.DialogTag> setFlag = new List<Dialogs.DialogTag>();
    public bool readOnce;
    public bool alreadyRead;
    public bool showDial;
    public bool showBlock;
    public bool showSet;


    private void Start() {
        Debug.Log("im born dialog container");
    }
}
