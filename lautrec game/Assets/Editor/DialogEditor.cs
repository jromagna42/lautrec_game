using UnityEditor;
using UnityEngine;
using DialogNameSpace;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Dialogs))]
public class DialogEditor : Editor
{
    Dialogs d;

    bool showDialogs;
    bool showFlags;

    GUIStyle[] gStyle = new GUIStyle[4];
    GUIStyle plusStyle = new GUIStyle();
    GUIStyle minusStyle = new GUIStyle();
    bool StyleSet = true;

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];

        for (int i = 0; i < pix.Length; i++)
            pix[i] = col;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }

    // public static Texture2D textureFromSprite(Sprite sprite)
    // {
    //     Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
    //     Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
    //                                                  (int)sprite.textureRect.y,
    //                                                  (int)sprite.textureRect.width,
    //                                                  (int)sprite.textureRect.height);
    //     newText.SetPixels(newColors);
    //     newText.Apply();
    //     return newText;
    // }

    void SetDemStyle()
    {
        gStyle[0] = new GUIStyle();
        gStyle[0].margin = new RectOffset(0, 0, 10, 10);
        gStyle[0].normal.background = MakeTex(200, 1, new Color32(80, 80, 80, 255));

        gStyle[1] = new GUIStyle();
        gStyle[1].margin = new RectOffset(0, 0, 10, 10);
        gStyle[1].normal.background = MakeTex(200, 1, new Color32(110, 110, 110, 255));

        gStyle[2] = new GUIStyle();
        gStyle[2].normal.background = MakeTex(200, 1, new Color32(60, 60, 60, 255));

        gStyle[3] = new GUIStyle();
        gStyle[3].margin = new RectOffset(30, 30, 0, 0);

        // if (plusStyle == null)


        //     plusStyle = new GUIStyle();
        // if (minusStyle == null)
        //     plusStyle = new GUIStyle();
        // plusStyle.normal.background = textureFromSprite(d.plus);
        //minusStyle.normal.background = textureFromSprite(d.minus);
        StyleSet = false;
    }


    public void UpdateFlags()
    {
        for (int i = 0; i < d.DList.Count; i++)
        {
            Dialogs.DialogContainer tmpdc = d.DList[i];
            List<Dialogs.DialogFlag> tmpflag = new List<Dialogs.DialogFlag>(d.flagList);
            foreach (Dialogs.DialogFlag df in d.DList[i].flag)
            {
                int j = tmpflag.FindIndex(x => x.flagName == df.flagName);
                if (j != -1)
                {
                    // if (df.setFlag == true || df.useFlag == true)
                    // {
                    //     Debug.Log("copy les value de : " + j);
                    //     Debug.Log("name : " + df.flagName + "\n"
                    //     + " use : " + df.useFlag + "\n"
                    //     + " set : " + df.setFlag);
                    // }
                    tmpflag[j] = CopyFlagValue(tmpflag[j], df);
                }
            }
            tmpdc.flag = tmpflag;
            d.DList.RemoveAt(i);
            d.DList.Insert(i, tmpdc);
        }
    }

    Dialogs.DialogFlag CopyFlagValue(Dialogs.DialogFlag targetFlag, Dialogs.DialogFlag sourceFlag)
    {
        targetFlag.flagName = sourceFlag.flagName;
        targetFlag.flagActive = sourceFlag.flagActive;
        targetFlag.useFlag = sourceFlag.useFlag;
        targetFlag.setFlag = sourceFlag.setFlag;
        return targetFlag;
    }

    Dialogs.DialogFlag ShowSingleFlag(Dialogs.DialogFlag f, bool main)
    {
        GUILayout.BeginHorizontal();
        string tmp = f.flagName;
        if (main)
        {
            f.flagName = EditorGUILayout.TextField(f.flagName);
            if (tmp != f.flagName)
            {
                d.updatedFlag = false;
                UpdateFlags();
            }
        }
        else
            EditorGUILayout.LabelField(f.flagName, GUILayout.MaxWidth(60));
        if (main == true)
            f.delflag = GUILayout.Button("-", GUILayout.MaxWidth(20));
        if (main == false)
        {
            EditorGUIUtility.labelWidth = 30;
            f.useFlag = EditorGUILayout.Toggle("use:", f.useFlag);
            f.setFlag = EditorGUILayout.Toggle("set:", f.setFlag);

            GUILayout.FlexibleSpace();
            EditorGUIUtility.labelWidth = 0;
        }
        GUILayout.EndHorizontal();


        return f;
    }



    Dialogs.DialogContainer ShowSingleDialog(Dialogs.DialogContainer t)
    {
        bool addText = false;
        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 70;
        addText = GUILayout.Button("add textbox");
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        if (addText == true)
        {
            t.text.Add("");
            addText = false;
        }
        for (int j = 0; j < t.text.Count; j++)
        {
            GUILayout.BeginHorizontal();
            t.text[j] = EditorGUILayout.TextArea(/*"text" + j,*/ t.text[j], GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 65));
            t.delText = GUILayout.Button("-", GUILayout.MaxWidth(20));
            if (t.delText == true)
            {
                t.text.RemoveAt(j);
                t.delText = false;
                j = 0;
            }
            GUILayout.EndHorizontal();
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = 70;
        t.readOnce = EditorGUILayout.Toggle("read once:", t.readOnce);
        EditorGUIUtility.labelWidth = 0;
        EditorGUILayout.EndHorizontal();

        t.showFlag = EditorGUILayout.Foldout(t.showFlag, "flags");
        GUILayout.BeginVertical(gStyle[3]);
        if (t.showFlag)
            ShowSection_Flags(t.flag, false);
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();

        return t;
    }



    void ShowSection_Flags(List<Dialogs.DialogFlag> fl, bool main)
    {
        if (main)
        {
            bool addFlag = false;
            addFlag = GUILayout.Button("add flag");
            if (addFlag == true)
            {
                addFlag = false;
                d.flagList.Add(new Dialogs.DialogFlag());
                UpdateFlags();
                return;
            }
        }
        for (int i = 0; i < d.flagList.Count; i++)
        {
            Dialogs.DialogFlag tmpList = fl[i];
            GUILayout.BeginVertical(gStyle[2]);
            tmpList = ShowSingleFlag(tmpList, main);
            GUILayout.EndVertical();
            fl[i] = tmpList;
            if (main)
            {
                if (d.flagList[i].delflag == true)
                {
                    d.flagList.Remove(d.flagList[i]);
                    UpdateFlags();
                }
            }
        }
    }

    void ShowSection_Dialogs()
    {
        bool addDial = false;
        addDial = GUILayout.Button("add dialog");
        if (addDial == true)
        {
            addDial = false;
            d.DList.Add(d.NewDialogContainer());
            UpdateFlags();
        }
        for (int i = 0; i < d.DList.Count; i++)
        {
            Dialogs.DialogContainer tmpList = d.DList[i];
            GUILayout.BeginVertical(gStyle[i % 2]);

            bool delDial = false;
            EditorGUILayout.BeginHorizontal();
            tmpList.showDial = EditorGUILayout.Foldout(d.DList[i].showDial, "dialog : " + i);
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(300));
            tmpList.delDial = EditorGUILayout.Foldout(d.DList[i].delDial, "delete dialog");
            if (d.DList[i].delDial)
                delDial = GUILayout.Button("delete dialog");
            if (delDial == true)
            {
                delDial = false;
                d.DList.Remove(d.DList[i]);
                continue;
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            if (d.DList[i].showDial)
                tmpList = ShowSingleDialog(tmpList);
            GUILayout.EndVertical();
            d.DList[i] = tmpList;
        }
    }

    public override void OnInspectorGUI()
    {
        d = (Dialogs)target;

        if (StyleSet == true)
            SetDemStyle();
        if (d.updatedFlag == false)
        {
            UpdateFlags();
            d.updatedFlag = true;
        }
        base.OnInspectorGUI();
        GUILayout.BeginVertical(GUILayout.MinHeight(30));
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(d);
            AssetDatabase.SaveAssets();
        }
        StyleSet = GUILayout.Button("reset style");

        showFlags = EditorGUILayout.Foldout(showFlags, "FLAGS");
        GUILayout.BeginVertical(gStyle[3]);
        if (showFlags)
            ShowSection_Flags(d.flagList, true);
        GUILayout.EndVertical();

        showDialogs = EditorGUILayout.Foldout(showDialogs, "DIALOGS");
        GUILayout.BeginVertical(gStyle[3]);
        if (showDialogs)
            ShowSection_Dialogs();
        GUILayout.EndVertical();
    }
}

// if (d.DList[i].showDial)
// {
//     d.DList[i].showBlock = EditorGUILayout.Foldout(d.DList[i].showBlock, "Blocktag");
//     if (d.DList[i].showBlock)
//     {
//         for (int j = 0; j < d.DList[i].blockFlag.Length; j++)
//         {
//             EditorGUILayout.BeginHorizontal();
//             d.DList[i].blockFlag[j].tagName = EditorGUILayout.TextField(d.DList[i].blockFlag[j].tagName);
//             d.DList[i].blockFlag[j].tagSet = EditorGUILayout.Toggle("isActive", d.DList[i].blockFlag[j].tagSet);
//             EditorGUILayout.EndHorizontal();
//         }
//     }
// }