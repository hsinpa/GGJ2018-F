using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBase : MonoBehaviour {

    public float speed = 5f;
    public Vector2 direct; 

	// Use this for initialization
	void Start () {
		
	}

    public void init(Vector2 _direct)
    {
        direct = _direct;
        transform.localScale = new Vector2((_direct.x == 1) ? -1 : 1 , 1 );
    }


	// Update is called once per frame
	void Update () {
        move();	
	}

    public void move()
    {
        gameObject.transform.position += new Vector3(direct.x, direct.y, 0) * speed * Time.deltaTime;
    }



}
