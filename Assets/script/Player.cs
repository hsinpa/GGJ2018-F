using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {



    public float speed=0.3f;
   
    public OldLaddy _oldLaddy;
    /// <summary>
    /// 有沒有在扶老太太
    /// </summary>
    public bool ishelp=false; 
	// Use this for initialization
	void Start () {
		
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        _oldLaddy = collision.gameObject.GetComponent<OldLaddy>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _oldLaddy = null;
    }



    public void help()
    {

        if (null != _oldLaddy)
        {
            if (ishelp)
            {

                speed = speed * 2 - _oldLaddy.speed;
                _oldLaddy.gameObject.transform.SetParent(null);
                ishelp = false;

            }
            else
            {
                speed = (speed  + _oldLaddy.speed)/2;
                _oldLaddy.gameObject.transform.parent = transform;
                ishelp = true;

            }

        }


    }

    public void move(Vector2 direct)
    {
        gameObject.transform.position += new Vector3(direct.x, direct.y, 0) * speed * Time.deltaTime;
    }




}
