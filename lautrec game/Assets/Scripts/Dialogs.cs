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

        [HideInInspector]
        public bool updatedFlag = false;

        [Serializable]
        public struct DialogFlag
        {
            public string flagName;
            public bool flagActive;
            
            [HideInInspector]
            public bool useFlag;
            [HideInInspector]
            public bool setFlag;


            [HideInInspector]
            public bool delflag;
            [HideInInspector]
            public bool showflag;

        }

        [HideInInspector]
        [SerializeField]
        public List<DialogFlag> flagList = new List<DialogFlag>();

        [HideInInspector]
        [SerializeField]
        public List<DialogContainer> DList = new List<DialogContainer>();

        [Serializable]
        public struct DialogContainer
        {
            public List<string> text;
            public List<Dialogs.DialogFlag> flag;

            public bool readOnce;

            [HideInInspector]
            public bool alreadyRead;

            // EDITOR VALUE
            public bool showDial;
            public bool delDial;

            public bool showFlag;

            public bool showText;
            public bool delText;
        }


        public DialogContainer NewDialogContainer()
        {
            DialogContainer tmp = new DialogContainer();

            tmp.text = new List<string>();
            tmp.flag = new List<Dialogs.DialogFlag>();
            return tmp;
        }

    }
}
