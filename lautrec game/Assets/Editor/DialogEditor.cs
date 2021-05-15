using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dialogs))]
public class DialogEditor : Editor
{

    GUIStyle[] gStyle = new GUIStyle[2];
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

    void SetDemStyle()
    {
        gStyle[0] = new GUIStyle();
        gStyle[1] = new GUIStyle();
        gStyle[0].margin = new RectOffset(0, 0, 20, 20);
        gStyle[1].margin = new RectOffset(0, 0, 20, 20);
        gStyle[0].normal.background = MakeTex(200, 1, Color.grey);
        gStyle[1].normal.background = MakeTex(200, 1, new Color32(80, 80, 80, 255));
        StyleSet = false;
    }

    void ShowSingleDialog(Dialogs d, int i)
    {
        for (int j = 0; j < d.DList[i].text.Count; j++)
        {
            d.DList[i].text[j] = EditorGUILayout.TextField("text" + j, d.DList[i].text[j]);
        }
        EditorGUILayout.BeginHorizontal();
        d.DList[i].readOnce = EditorGUILayout.Toggle("read once:", d.DList[i].readOnce, GUILayout.MaxWidth(300));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        d.DList[i].showBlock = EditorGUILayout.Toggle("showBlock:", d.DList[i].showBlock, GUILayout.MaxWidth(300));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        d.DList[i].showSet = EditorGUILayout.Toggle("showSet:", d.DList[i].showSet, GUILayout.MaxWidth(300));
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StyleSet = GUILayout.Button("reset style");
        if (StyleSet == true)
            SetDemStyle();
        Dialogs d = (Dialogs)target;
        bool addDial = false;
        addDial = GUILayout.Button("add dialog");
        if (addDial == true)
        {
            addDial = false;
            d.DList.Add(ScriptableObject.CreateInstance("DialogContainer") as DialogContainer);
        }
        for (int i = 0; i < d.DList.Count; i++)
        {
            //Color[] GUIcolor = new Color[]{Color.red, Color.grey};//{new Color(80f, 80f, 80f, 255f), new Color(100f, 100f, 100f, 255f)};
            if (!d.DList[i])
                d.DList[i] = ScriptableObject.CreateInstance("DialogContainer") as DialogContainer;

            if (i % 2 == 0)
                GUILayout.BeginVertical(gStyle[0]);
            else
                GUILayout.BeginVertical(gStyle[1]);

            // GUI.backgroundColor = GUIcolor[i % 2];
            // GUI.color = GUIcolor[i % 2];

            bool delDial = false;
            EditorGUILayout.BeginHorizontal();
            d.DList[i].showDial = EditorGUILayout.Foldout(d.DList[i].showDial, "dialog : " + i);
            EditorGUILayout.BeginVertical(GUILayout.MaxWidth(300));
            d.DList[i].delDial = EditorGUILayout.Foldout(d.DList[i].delDial, "delete dialog");
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
                ShowSingleDialog(d, i);
            GUILayout.EndVertical();
        }

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