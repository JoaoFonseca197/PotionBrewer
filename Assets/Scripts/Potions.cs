using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potions", menuName = "Potions", order = 1)]
public class Potions : ScriptableObject, IAction
{
    [SerializeField] SerialController _serialController;
    [SerializeField] private List<ActionReaction> _actionReaction;

    //public Dictionary<char, string> Actions;


    
    


   
    // Start is called before the first frame update

    public IReadOnlyDictionary<char, string> Actions
    {
        
        get
        {
            Dictionary<char, string> supportDic = new Dictionary<char, string>();
            foreach (ActionReaction action in _actionReaction)
                supportDic.Add(action.Action, action.Reaction);
            return supportDic;
        }
    }


    void IAction.SetColor1(Color32 color)
    {
        _serialController.SendSerialMessage("1 255 0 0");
        
    }
    void IAction.SetColor2(Color32 color)
    {
        _serialController.SendSerialMessage("2 255 0 0");
    }
    void IAction.SetHalfColor(Color32 color)
    {
        _serialController.SendSerialMessage("3 255 0 0");
    }
    void IAction.SetAllColor(Color32 color)
    {
        _serialController.SendSerialMessage("4 255 0 0");
    }
    void IAction.Shake(Color32 Mixcolor)
    {
        _serialController.SendSerialMessage("5 " + Mixcolor.r + " " + Mixcolor.g + " " + Mixcolor.b);
    }
    void IAction.Wait(float time)
    {
        _serialController.SendSerialMessage("c 255 0 0");
    }

    [Serializable]
    public class ActionReaction
    {
        [SerializeField]  char _action;
        [SerializeField]  string _reaction;

        public char Action => _action;
        public string Reaction => _reaction;
    }
}
