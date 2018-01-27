using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    private float upDis=5;
    public Text _text;


    public void init(string message)
    {
        transform.DOMoveY(transform.position.y + upDis, 2).OnComplete(()=>Destroy(gameObject));
        _text.text = message;
    }


}
	
