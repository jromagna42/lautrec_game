using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogNameSpace;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DialogManager : MonoBehaviour
{
    public GameObject talkerBoxPrefab;
    public GameObject textLinePrefab;
    public RectTransform textLinePrefabRect;

    public RectTransform textBound;

    public Dialogs dialog;

    public bool upDial = true;

    public List<Dialogs.DialogContainer> chosenDialog;
    public List<GameObject> TextLineList;

    private void OnEnable()
    {

    }

    bool compareFlag(List<Dialogs.DialogFlag> dialFlag, List<Dialogs.DialogFlag> mainFlag)
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
            if (compareFlag(dc.flag, dialog.flagList) == true)
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
        TextLineList.RemoveAll(x => x);
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
            Vector3 textPos = new Vector3(textBound.position.x,yStartPos - j * yOffset, textBound.position.z);
            GameObject tmp = Instantiate(textLinePrefab, textPos, textBound.rotation, textBound);
            TextLineList.Add(tmp);
            j++;
        }
    }

    void FillTextLines()
    {
        for (int i = 0; i < TextLineList.Count; i++)
        {
            TextLineList[i].GetComponent<Text>().text = chosenDialog[i].text[0];
        }
    }

    void updateDialog()
    {
        chosenDialog = GetDialog();
        DestroyOldTextLines();
        CreateTextLines(chosenDialog.Count);
        FillTextLines();
    }

    private void Update()
    {
        if (upDial == true)
        {
            updateDialog();
            upDial = false;
        }
    }
}
