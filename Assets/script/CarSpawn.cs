using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour {

    public GameObject car;
    public float rebornTime = 5F;
    private float countDown = 0;
    [SerializeField]
    private Vector2 direct;
    private bool isActivate = false;

	// Use this for initialization
	public void SetUp (Vector2 p_direction ) {
        isActivate = true;
        direct = p_direction;
	}
	
	void Update () {
        //Work only if is activate
        if (!isActivate)  return;

        if (countDown >= rebornTime)
        {

            CarBase carBase= GameObject.Instantiate(car,transform.position, transform.rotation,transform).GetComponent<CarBase>();
            carBase.init(direct);
            countDown = 0;
        }
        countDown += Time.deltaTime;





	}
}
