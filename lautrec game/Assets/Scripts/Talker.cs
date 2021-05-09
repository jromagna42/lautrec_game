using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class Talker : MonoBehaviour
{

    public enum TalkerState {Talker, Neutral, Listen};
    public TalkerState currentState = TalkerState.Neutral;

    Image image;
    RectTransform tr;

    public bool isRight = false;

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
    }


    StateValue listenValue = new StateValue(new Vector2(260, 360),new Vector2(300, 300), new Color(76, 76, 76, 255));
    StateValue neutralValue = new StateValue(new Vector2(260, 360),new Vector2(325, 325), new Color(255, 255, 255, 255));
    StateValue talkerValue = new StateValue(new Vector2(260, 360),new Vector2(350, 350),new Color(255, 255, 255, 255));

    void start()
    {
        image = GetComponent<Image>();
        tr = GetComponent<RectTransform>();
    }

    public void SetImage(Sprite sp)
    {
        image.sprite = sp;
    }

    void SetStateValue(StateValue val)
    {
        tr.position = val.pos;
        tr.sizeDelta = val.size;
        image.color = new Color32((byte)val.color.r, (byte)val.color.g, (byte)val.color.b, (byte)val.color.a);
    }


 // StateValue lv()
    // {
    //      return (new StateValue(new Vector2(260, 360),new Vector2(300, 300), new Color(76, 76, 76, 255));
    // }
    
 // StateValue nv()
    // {
    //     return (new StateValue(new Vector2(260, 360),new Vector2(325, 325), new Color(255, 255, 255, 255));
    // }

    // StateValue tv()
    // {
    //     return (new StateValue(new Vector2(260, 360),new Vector2(350, 350),new Color(255, 255, 255, 255));
    // }

   
   

    private void OnValidate() 
    {
        if (!tr)
            tr = GetComponent<RectTransform>();
        if (!image)
            image = GetComponent<Image>();
        ChangeState();
    }

    void ChangeState()
    {
        switch(currentState)
        {
            case TalkerState.Talker:
            {
                SetStateValue(talkerValue);
                break;
            }
            case TalkerState.Neutral:
            {
                SetStateValue(neutralValue);
                break;
            }
            case TalkerState.Listen:
            {
                SetStateValue(listenValue);
                break;
            }
            default:
                break;
        }
        // switch(currentState)
        // {
        //     case TalkerState.Talker:
        //     {
        //         SetStateValue(tv());
        //         break;
        //     }
        //     case TalkerState.Neutral:
        //     {
        //         SetStateValue(nv());
        //         break;
        //     }
        //     case TalkerState.Listen:
        //     {
        //         SetStateValue(lv());
        //         break;
        //     }
        //     default:
        //         break;
        // }
    }
}
