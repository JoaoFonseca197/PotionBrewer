using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] List<Potions>      _potions;
    [SerializeField] SerialController   _serialController;
    [Range(0, 1)][SerializeField] float _cyan;
    [Range(0, 1)][SerializeField] float _yellow;
    [Range(0, 1)][SerializeField] float _magenta;
    [SerializeField] private Color _mixedColor;
    [Range(0, 1)][SerializeField] float _red;
    [Range(0, 1)][SerializeField] float _green;
    [Range(0, 1)][SerializeField] float _blue;

    [SerializeField] float _maxTimeToWait;
    [SerializeField] bool   _blink;

    private int     _colorNumber;

    private float     _numberMagenta;
    private float     _numberYellow;
    private float     _numberCyan;

    //private float   _magenta;
    //private float   _yellow;
    //private float   _cyan;
    private int     _actionNumber;
    private char    _actionChar;
    private Potions _finalPotion;
    [SerializeField]
    private List<Color> _potionColors;

    [SerializeField]
    private float       _timeToWait;
    [SerializeField]
    private float       _timer;
    [SerializeField]
    private bool        _goodWaitTime;

    private void Awake()
    {
        _timer = 0;
        _numberMagenta = 0;
        _numberYellow = 0;
        _numberCyan = 0;
        _colorNumber = 0;
        _actionNumber = 0;
        _potionColors = new List<Color>();
        
    }
    [Button]
    public void Clear()
    {
        _numberMagenta = 0;
        _numberYellow = 0;
        _numberCyan = 0;

        _cyan = 0;
        _magenta = 0;
        _yellow = 0;

        _red = 0;
        _green = 0;
        _blue = 0;
        _mixedColor = Color.black;

        _potionColors.Clear();
    }
    [Button]
    public void AddColorMagenta()
    {
        _actionChar = 'm';
        //_potionColors[_colorNumber] = Color.magenta;

        _numberMagenta++;

        _potionColors.Add(Color.magenta);

        CMYKToRGB();

        // _colorNumber++;


    }
    [Button]
    public void AddColorYellow()
    {
        _actionChar = 'y';

        _potionColors.Add( Color.yellow);
        _numberYellow++;
        

        CMYKToRGB();
    }
    [Button]
    public void AddColorCyan()
    {
        
        _actionChar = 'c';
        _potionColors.Add(Color.cyan);
        _numberCyan++;
        

        CMYKToRGB();

        CheckForPossiblePotions();

    }

    public void Wait()
    {
        _actionChar = 'w';
        CheckForReactions();
        CheckForPossiblePotions();
    }
    [Button]
    public void Shake()
    {
        _actionChar = 's';
        Blend();
        //CheckForReactions();
        //CheckForPossiblePotions();
    }



    public void Done()
    {
        _actionChar = 'd';
        CheckForPossiblePotions();
        print(_finalPotion);
    }
    /// <summary>
    /// 
    /// </summary>
    private void CheckForReactions()
    {
        foreach(Potions potion in _potions)
        {
            if (potion.Actions.Keys.ToArray()[_actionNumber] == 's')
                Blend();
            if (potion.Actions.Keys.ToArray()[_actionNumber] == 'w')
                CountTimeToWait(potion.Actions['w']);
        }
            
    }
    private void Blend()
    {
        for(int i = 0; i < _potionColors.Count; i++)
        {
            _potionColors[i] = _mixedColor;
        }
            
         //_mixedColor = _potionColors[0] + _potionColors[1];
        _potionColors[0] = _mixedColor;
        CMYKToRGB();
    }
    [Button]
    private void CMYKToRGB()
    {
        float maxNum = Mathf.Max(_numberMagenta, Mathf.Max(_numberCyan, _numberYellow));
        _cyan = _numberCyan / maxNum;
        _magenta = _numberMagenta / maxNum;
        _yellow = _numberYellow / maxNum;


        //Color colorMix = new Color(255 * (1 - _cyan), 255 * (1 - _magenta), 255 * (1-_yellow));
        _red = (1 - _cyan);
        _green = (1 - _magenta);
        _blue =  (1 - _yellow);
        _mixedColor = new Color(_red, _green, _blue);

    }


    private void CountTimeToWait(string potionAction)
    {
        _timeToWait = int.Parse(potionAction);
    }
    private void CheckForPossiblePotions()
    {
        if(_potions.Count == 1)
        {
            _finalPotion = _potions[0];
            return;
        }
            
        List<Potions> possiblePotions = new List<Potions>();
        foreach(Potions potion in _potions)
        {       
            if (potion.Actions.Keys.ToArray()[_actionNumber] == _actionChar)
                possiblePotions.Add(potion);
        }
        _actionNumber++;
        _potions = possiblePotions;
    }
    [Button]
    public void CheckForWaitTime()
    {
        _timeToWait = 0;
        if (_goodWaitTime)
        {
            if(_potionColors.Count == 2)
                _potionColors.RemoveAt(1);
            _blink = true;

        }
        else
        {
            for (int i = 0; i < _potionColors.Count; i++)
                _potionColors[i] = Color.black;
        }

    }

    


    public void ClearLeds()
    {
        //_serialController.SendSerialMessage("b");
    }

    private void Blink()
    {
        if (_potionColors[0] == Color.black)
        {
            for (int i = 0; i < _potionColors.Count; i++)
                _potionColors[i] = _mixedColor;
        }
        else
        {
            for (int i = 0; i < _potionColors.Count; i++)
                _potionColors[i] = Color.black;
        }
    }

    private void Update()
    {
        if(_timeToWait != 0)
            _timer += Time.deltaTime;

        if(_timer > _timeToWait && _timer < _timeToWait + _maxTimeToWait)
        {
            _goodWaitTime = true;
        }
        else if(_timer > _timeToWait + _maxTimeToWait)
        {
            _goodWaitTime = false;
        }

        if(_blink)
            Blink();

    }

}
