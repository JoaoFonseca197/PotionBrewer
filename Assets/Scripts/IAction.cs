using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction 
{
    void SetColor1(Color32 color);
    void SetColor2(Color32 color);
    void SetHalfColor(Color32 color);
    void SetAllColor(Color32 color);
    void Shake(Color32 MixColor);
    void Wait(float time);
}
