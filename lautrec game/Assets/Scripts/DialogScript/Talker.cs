using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class Talker : MonoBehaviour
{

    public enum TalkerState { Talker, Neutral, Listen };
    public TalkerState currentState = TalkerState.Neutral;

    public Text nameText;

    public bool baseSpriteIsRight = true;
    public bool isRight = false;
    public Image image;
    public RectTransform imageTr;
    RectTransform tr;



    public float lowerBound = 210;
    public float LateralPos = 210;
    float yPos;
    float xPos;

    public bool needUpdate = true;

    public struct StateValue
    {
        public Vector2 pos;
        public Vector2 size;
        public Color color;

        public StateValue(Vector2 i, Vector2 j, Color c)
        {
            this.pos = i;
            this.size = j;
            this.color = c;
        }
        public static StateValue listen = new StateValue(new Vector2(0, 0), new Vector2(300, 300), new Color(76, 76, 76, 255));
        public static StateValue neutral = new StateValue(new Vector2(0, 0), new Vector2(325, 325), new Color(255, 255, 255, 255));
        public static StateValue talker = new StateValue(new Vector2(0, 0), new Vector2(350, 350), new Color(255, 255, 255, 255));
    }

    void start()
    {
        tr = GetComponent<RectTransform>();
        ChangeState();
    }

    public void SetImage(Sprite sp)
    {
        image.sprite = sp;
    }

    void CalcPos()
    {
        yPos = lowerBound + imageTr.sizeDelta.y / 2;
        if (isRight == true)
            xPos = Screen.currentResolution.width - LateralPos;
        else
            xPos = LateralPos;
    }

    void DirectionSprite()
    {
        float rotaY = 0;
        if (baseSpriteIsRight == false)
            rotaY = 180;
        if (isRight == true)
            rotaY += 180;
        imageTr.rotation = Quaternion.Euler(0, rotaY, 0);
    }

    void SetStateValue(StateValue val)
    {
        DirectionSprite();
        imageTr.sizeDelta = val.size;
        CalcPos();
        val.pos = new Vector2(xPos, yPos);
        tr.position = val.pos;
        image.color = new Color32((byte)val.color.r, (byte)val.color.g, (byte)val.color.b, (byte)val.color.a);
    }

    private void OnValidate()
    {
        if (!tr)
            tr = GetComponent<RectTransform>();
        if (!image)
            image = GetComponent<Image>();
        ChangeState();
    }

    public void UpdatePos()
    {
        if (!tr)
            tr = GetComponent<RectTransform>();
        if (!image)
            image = GetComponent<Image>();
        ChangeState();
    }

    void ChangeState()
    {

        switch (currentState)
        {
            case TalkerState.Talker:
                {
                    SetStateValue(StateValue.talker);
                    break;
                }
            case TalkerState.Neutral:
                {
                    SetStateValue(StateValue.neutral);
                    break;
                }
            case TalkerState.Listen:
                {
                    SetStateValue(StateValue.listen);
                    break;
                }
            default:
                break;
        }

    }
}
