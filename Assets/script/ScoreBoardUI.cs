using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardUI : MonoBehaviour {
    [SerializeField]
    private Text _score;
    [SerializeField]
    private Text[] _saveOldLaddy = new Text[3];
    [SerializeField]
    private Text[] _dieOldLaddy = new Text[3];





    private void UpdateUI()
    {
        _score.text = GameManager.instance.Score.ToString();
    }
}
