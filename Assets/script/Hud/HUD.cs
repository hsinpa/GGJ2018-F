using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private float upDis=30;
    public Text _text;


    public void init(string message,Color _color =default(Color))
    {
        ///_text.color = _color;
        RectTransform _rectTramsform = GetComponent<RectTransform>();

        _rectTramsform.DOLocalMoveY(_rectTramsform.position.y+ upDis, 2).OnComplete(()=>Destroy(gameObject));
        _text.text = message;
    }


}
	
