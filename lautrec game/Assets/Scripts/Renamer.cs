using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Renamer : MonoBehaviour
{
    public SpriteRenderer sr;
    
    public void changeName()
    {
        name = sr.sprite.name;
    }

    void Start()
    {
        if (name != sr.sprite.name)
            changeName();
    }
}
