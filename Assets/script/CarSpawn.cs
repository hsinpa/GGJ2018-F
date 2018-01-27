using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour {

    public GameObject car;
    public float rebornTime = 5F;
    private float countDown = 0;
    [SerializeField]
    private Vector2 direct;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (countDown >= rebornTime)
        {

            CarBase carBase= GameObject.Instantiate(car,transform.position, transform.rotation,transform).GetComponent<CarBase>();
            carBase.init(direct);
            countDown = 0;
        }
        countDown += Time.deltaTime;





	}
}
