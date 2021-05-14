using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Dialogs))]
public class DialogEditor : Editor
{

    public override void OnInspectorGUI()
    {
        Debug.Log("gui update start");
        base.OnInspectorGUI();

        Dialogs d = (Dialogs)target;

        for (int i = 0; i < d.DList.Length; i++)
        {
            if (!d.DList[i])
            {
                Debug.Log("new dialog container");
                d.DList[i] = ScriptableObject.CreateInstance("DialogContainer") as DialogContainer;
            }
            bool delDial = false;
            EditorGUILayout.BeginHorizontal();
            delDial = GUILayout.Button("delete dialog",);
            if (delDial == true)
            {
                delDial = false;
                d.DList.
            }
            EditorGUILayout.EndHorizontal();
            d.DList[i].showDial = EditorGUILayout.Foldout(d.DList[i].showDial, "dialog : " + i);
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