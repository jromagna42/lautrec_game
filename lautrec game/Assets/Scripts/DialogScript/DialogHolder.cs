using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 1

public class DialogHolder : MonoBehaviour
{

    public DialogNameSpace.Dialogs dialog;


    [Serializable]
    public struct talkerStruct
    {
        public Sprite image;
        public string name;

    }

    [HideInInspector]
    [SerializeField]
    public List<talkerStruct> talkerList = new List<talkerStruct>();

    CharPrefabScript cps;


    void Start()
    {
        cps = GetComponent<CharPrefabScript>();
    }

    private void OnMouseEnter()
    {
        // Debug.Log("souris rentres");
        Cursor.SetCursor(GameManager.Instance.dialogMouse, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        // Debug.Log("souris sortir");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
    private void OnMouseDown()
    {
        Debug.Log("start dialog");
        if (DialogManager.Instance.dialogActive == false)
        {
            StartDialog();
            GameManager.Instance.Player.GetComponent<MainCharController>().isTalking = true;
        }
    }

    public void StartDialog()
    {
        if (!dialog.player)
            dialog.player = GameManager.Instance.Player;
        DialogManager.Instance.gameObject.SetActive(true);
        DialogManager.Instance.dialogActive = true;
        DialogManager.Instance.DialogSetup(this);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
