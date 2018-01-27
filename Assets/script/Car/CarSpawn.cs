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

    private List<JSONObject> carTypesList = new List<JSONObject>();

	// Use this for initialization
	public void SetUp (Vector2 p_direction, List<JSONObject> p_car_types ) {
        carTypesList = p_car_types;
        isActivate = true;  
        direct = p_direction;
	}
	
	void Update () {
        //Work only if is activate
        if (!isActivate)  return;

        if (countDown >= rebornTime)
        {
            int randomCarIndex = Random.Range(0, carTypesList.Count - 1);
            string carID = carTypesList[randomCarIndex].str;
            JSONObject carComp= GameManager.instance.GetJSONComponent( carID );
            rebornTime = carComp.GetField("spawn_time").num;

            CarBase carBase= GameObject.Instantiate(car,transform.position, transform.rotation,transform).GetComponent<CarBase>();
            carBase.init( carID, direct, carComp);
            countDown = 0;
        }
        countDown += Time.deltaTime;
	}

}
