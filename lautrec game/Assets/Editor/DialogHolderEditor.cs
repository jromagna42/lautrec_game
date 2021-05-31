using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(DialogHolder))]
public class DialogHolderEditor : Editor
{

    DialogHolder d;

    DialogHolder.talkerStruct emptyDH = new DialogHolder.talkerStruct();

    public void DeleteTalker(int i)
    {
        d.talkerList.RemoveAt(i);
    }

    public override void OnInspectorGUI()
    {
        //Debug.Log("gui update start");
        base.OnInspectorGUI();
        d = (DialogHolder)target;

        if (GUILayout.Button("add talker"))
        {
            DialogHolder.talkerStruct newTalker = new DialogHolder.talkerStruct();
            newTalker.name = "";
            d.talkerList.Add(newTalker);

        }

        for (int i = 0; i < d.talkerList.Count; i++)
        {
            
            DialogHolder.talkerStruct tmp = emptyDH;
            tmp = d.talkerList[i];
            EditorGUILayout.BeginHorizontal();
            tmp.name = EditorGUILayout.TextField("name", tmp.name);
            if (GUILayout.Button("-", GUILayout.MaxWidth(30)))
            {
                d.talkerList.RemoveAt(i);
                continue;
            }
            EditorGUILayout.EndHorizontal();
            tmp.image = (Sprite)EditorGUILayout.ObjectField("image", tmp.image, typeof(Sprite), allowSceneObjects: true);

            d.talkerList[i] = tmp;
        }
        EditorUtility.SetDirty(d);
        AssetDatabase.SaveAssets();
    }
}
