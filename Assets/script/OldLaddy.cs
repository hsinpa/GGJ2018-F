using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OldLaddy : MonoBehaviour,IroadObject {

    public float speed =0.4f;



    public void init(Vector3 position)
    {
        transform.DOMove(position, 5);
    }



  


    public void die()
    {
        
        HUDManager.instance.ShowHUD("Die", transform.position);
        Destroy(gameObject);

    }

    public void onHit(CarBase _car)
    {
        Debug.Log("HIT");
        die();
    }

    public void onDetect(CarBase _car)
    {
        
    }
}
