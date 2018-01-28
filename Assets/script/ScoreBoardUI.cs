using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class ScoreBoardUI : MonoBehaviour {
    [SerializeField]
    private Text _score;
    [SerializeField]
    private Text[] _saveOldLaddy = new Text[3];
    [SerializeField]
    private Text[] _dieOldLaddy = new Text[3];

    private void Start()
    {
        GameManager.instance.updateUI += UpdateUI;
    }



    private void UpdateUI()
    {

        JSONObject _characterJSON=GameManager.instance.characterJSON;
        _score.text = GameManager.instance.Score.ToString();
        for (int i = 0; i < _saveOldLaddy.Count(); i++)
        {
           _saveOldLaddy[i].text = GameManager.instance.savedOldLaddyDic[_characterJSON.keys[i+1]].ToString();
        }

        for (int i = 0; i < _dieOldLaddy.Count(); i++)
        {
            _dieOldLaddy[i].text = GameManager.instance.savedOldLaddyDic[_characterJSON.keys[i + 1]].ToString();
        }


    }
}
