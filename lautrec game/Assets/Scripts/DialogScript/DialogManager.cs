using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogNameSpace;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogManager : MonoBehaviour
{
    public enum dialogState { multi, single };

    public GameObject talkerBoxPrefab;
    public GameObject textLinePrefab;
    public RectTransform textLinePrefabRect;
    public RectTransform textBound;
    public Dialogs dialog;

    public GameObject talkerMaster;

    public bool upDial = true;

    [HideInInspector]
    public dialogState currentState = dialogState.multi;
    List<Dialogs.DialogContainer> chosenDialog;
    List<GameObject> TextLineList;

    int singleDialIndex = 0;

    GameObject playerTalker;
    List<GameObject> NPCtalker = new List<GameObject>();


    public static DialogManager Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null) { Instance = this; } else { Debug.Log("Warning: multiple " + this + " in scene!"); }
        DontDestroyOnLoad(this);
    }

    public void DialogSetup(DialogHolder dc)
    {
        if (!playerTalker)
            playerTalker = SpawnMainTalker(dialog.player);
        NPCtalker.Clear();
        for (int i = 0; i < dc.talkerList.Count; i++)
        {
            NPCtalker.Add(SpawnTalker(dc, i));
        }
    }
    // CharPrefabScript MCCtmp = source.GetComponent<CharPrefabScript>();
    GameObject SpawnTalker(DialogHolder source, int i)
    {
        GameObject go;
        go = Instantiate(talkerBoxPrefab);
        
        Talker Ttmp = go.GetComponent<Talker>();
        if (source.talkerList[i].image)
            Ttmp.image.sprite = source.talkerList[i].image;
        else
            Ttmp.image.sprite = source.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
        if (source.talkerList[i].name != "")
            Ttmp.nameText.text = source.talkerList[i].name;
        else
            Ttmp.nameText.text = source.gameObject.name;
        return go;
    }

    GameObject SpawnMainTalker(GameObject source)
    {
        GameObject go;
        go = Instantiate(talkerBoxPrefab);
        MainCharController MCCtmp = source.GetComponent<MainCharController>();
        Talker Ttmp = go.GetComponent<Talker>();
        Ttmp.image.sprite = MCCtmp.dialogImage;
        Ttmp.nameText.text = MCCtmp.dialogName;
        return go;
    }

    void SetNewFlags()
    {
        for (int i = 0; i < chosenDialog[0].flag.Count; i++)
        {
            Dialogs.DialogFlag tmpflag = new Dialogs.DialogFlag();
            tmpflag = dialog.flagList[i];
            tmpflag.flagActive = chosenDialog[0].flag[i].setFlag;
            dialog.flagList[i] = tmpflag;
            // dialog.flagList.RemoveAt(i);
            // dialog.flagList.Insert(i, tmpflag);
        }
    }

    bool CheckUseFlag(List<Dialogs.DialogFlag> dialFlag, List<Dialogs.DialogFlag> mainFlag)
    {
        for (int i = 0; i < mainFlag.Count; i++)
        {
            if (mainFlag[i].flagActive == false)
                continue;
            if (dialFlag[i].useFlag == true)
                return true;
        }
        return false;
    }

    List<Dialogs.DialogContainer> GetDialog()
    {
        List<Dialogs.DialogContainer> strList = new List<Dialogs.DialogContainer>();
        foreach (Dialogs.DialogContainer dc in dialog.DList)
        {
            if (CheckUseFlag(dc.flag, dialog.flagList) == true)
                strList.Add(dc);
        }
        return strList;
    }

    void DestroyOldTextLines()
    {
        foreach (GameObject go in TextLineList)
        {
            Destroy(go);
        }
        if (TextLineList.Count != 0)
            TextLineList.Clear();
    }

    void CreateTextLines(int i)
    {
        int j = 0;
        float yStartPos;
        float yOffset;

        yStartPos = textBound.position.y + textBound.sizeDelta.y / 2 - textLinePrefabRect.sizeDelta.y / 2;
        yOffset = textBound.sizeDelta.y / i;

        while (j < i)
        {
            Vector3 textPos = new Vector3(textBound.position.x, yStartPos - j * yOffset, textBound.position.z);
            GameObject tmp = Instantiate(textLinePrefab, textPos, textBound.rotation, textBound);
            TextLineList.Add(tmp);
            j++;
        }
    }

    void FillTextLines_Multi()
    {
        for (int i = 0; i < TextLineList.Count; i++)
        {
            TextLine tl;
            tl = TextLineList[i].GetComponent<TextLine>();
            tl.tc.text = chosenDialog[i].text[0];
            tl.dialIndex = i;
            tl.dm = this;
        }
    }

    public void TextLineClicked(int i)
    {
        if (currentState == dialogState.multi)
        {
            chosenDialog.RemoveAll(x => chosenDialog.IndexOf(x) != i);
            currentState = dialogState.single;
            singleDialIndex = 0;
            upDial = true;
            SetNewFlags();
        }
        if (currentState == dialogState.single)
        {
            singleDialIndex++;
            upDial = true;
            if (singleDialIndex >= chosenDialog[0].text.Count)
            {
                currentState = dialogState.multi;
                singleDialIndex = 0;
            }
        }
    }

    void FillTextLines_Single()
    {
        TextLine tl;
        tl = TextLineList[0].GetComponent<TextLine>();
        tl.tc.text = chosenDialog[0].text[singleDialIndex];
        tl.dialIndex = 0;
        tl.dm = this;
    }

    void UpdateDialog_Single()
    {
        DestroyOldTextLines();
        CreateTextLines(chosenDialog.Count);
        FillTextLines_Single();
    }

    void UpdateDialog_Multi()
    {
        chosenDialog = GetDialog();
        DestroyOldTextLines();
        CreateTextLines(chosenDialog.Count);
        FillTextLines_Multi();
    }

    private void Update()
    {
        if (upDial == true)
        {
            switch (currentState)
            {
                case dialogState.single:
                    {
                        UpdateDialog_Single();
                        upDial = false;
                        break;
                    }
                case dialogState.multi:
                    {
                        UpdateDialog_Multi();
                        upDial = false;
                        break;
                    };
                default:
                    { break; }
            }

        }
    }
}
