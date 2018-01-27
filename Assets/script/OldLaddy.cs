using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldLaddy : MonoBehaviour {

    public float speed =0.4f;



    public void init(Vector3 position)
    {
        transform.DOMove(position, 5);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("car"))
        {
            Debug.Log("HIT");
            die();
        }
    }


    public void die()
    {
        
        HUDManager.instance.ShowHUD("Die", transform.position);
        Destroy(gameObject);

    }

}
