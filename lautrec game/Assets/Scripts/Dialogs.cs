using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DialogNameSpace
{
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

        [HideInInspector][SerializeField]
        public List<DialogContainer> DList = new List<DialogContainer>();

        [Serializable]
        public struct DialogContainer
        {
            public List<string> text;
            public List<Dialogs.DialogTag> blockFlag;
            public List<Dialogs.DialogTag> setFlag;

            public bool readOnce;

            [HideInInspector]
            public bool alreadyRead;

            // EDITOR VALUE
            public bool showDial;
            public bool delDial;

            public bool showBlock;
            public bool showSet;
            public bool showText;
            public bool delText;
        }

        public DialogContainer NewDialogContainer()
        {
            DialogContainer tmp = new DialogContainer();

            tmp.text = new List<string>();
            tmp.blockFlag = new List<Dialogs.DialogTag>();
            tmp.setFlag = new List<Dialogs.DialogTag>();
            return tmp;
        }

    }
}
