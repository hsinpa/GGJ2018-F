using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class MapManager : MonoBehaviour {
	SpriteRenderer streetObject; 

	GameObject detectorPrefab, carSpawnPrefab;

	GameObject topBlockObject, bottomBlockObject;

	GameManager _gameManager;

	public List<CarSpawn> carSpawnPointList =  new List<CarSpawn>();

	//Varaible
	int slotheight = 4;
	int detectorHeight = 2;
	int slotNumer;


    public float getEndY()
    {
        return topBlockObject.transform.position.y;

    }

	public void Setup(GameManager p_gameManager, int p_slot_num) {
		Clear();

		_gameManager = p_gameManager;
		slotNumer = p_slot_num;

		detectorPrefab = Resources.Load<GameObject>("Prefab/MapObject/DetectorObject");
		carSpawnPrefab = Resources.Load<GameObject>("Prefab/MapObject/CarSpawn");
		
		streetObject = transform.Find("Street").GetComponent<SpriteRenderer>();
		SetStreetSlots(p_slot_num);


		float[] btVaraible = GetBottomTopLine(streetObject);

		//Bottom Block
		bottomBlockObject = GetDetctorObject(bottomBlockObject, "BottomBlock", new Vector2(transform.position.x,  btVaraible[0] +  (detectorHeight/2f)), 
						new Vector2(streetObject.size.x, detectorHeight ) );

		//Top Block
		topBlockObject = GetDetctorObject(topBlockObject, "TopBlock", new Vector2(transform.position.x,  btVaraible[1]+(detectorHeight/2f)),
						 new Vector2(streetObject.size.x, detectorHeight) );

		//Set Grandma config
		SetGrandMaStartingLine(new Vector2(bottomBlockObject.transform.position.x,  bottomBlockObject.transform.position.y - slotheight));
		transform.Find("OldLaddySpawnList/OldLaddySpawn").GetComponent<OldLaddySpawn>().SetUp(p_gameManager._roundSystemManager);

		//Set Player config
		SetPlayerStartingLine(new Vector2(0,  bottomBlockObject.transform.position.y - slotheight));

		//Set carspawn config
		SetCarSpawnGenerator(btVaraible[0], p_slot_num);
	}
	//================================================================ Map Setting ================================================================
	public void SetStreetSlots(int p_slot_num) {
		streetObject.size = new Vector2(streetObject.size.x,  slotheight * p_slot_num);
	}

	//0 = bottom, 1 = top
	public float[] GetBottomTopLine(SpriteRenderer p_streetObject ) {
		float centerAxisY = p_streetObject.transform.position.y,
		halfheight = (p_streetObject.size.y / 2),
		topPos = centerAxisY + halfheight,
		bottomPos = centerAxisY - halfheight;

		return new float[] {bottomPos,topPos };
	}


	public GameObject GetDetctorObject(GameObject dectorObject, string p_name, Vector2 p_position, Vector2 p_size) {
		if (dectorObject == null) dectorObject = GameObject.Instantiate(detectorPrefab);
		BoxCollider2D boxColider =  dectorObject.GetComponent<BoxCollider2D>();

		dectorObject.name = p_name;
		dectorObject.transform.SetParent(this.transform);
		dectorObject.transform.position = p_position;
		
		boxColider.size = p_size;
		boxColider.isTrigger = true;
		return dectorObject;
	}

	//================================================================ Car Spawn Setting ================================================================
	private CarSpawn CreateCarSpawn(Vector2 p_position, Vector2 p_direction) {
		GameObject carSpawnObject = GameObject.Instantiate(carSpawnPrefab);
		CarSpawn carSpawnScript = carSpawnObject.GetComponent<CarSpawn>();

		Transform carSpawnHolder = transform.Find("CarSpawnList");

		carSpawnObject.transform.SetParent(carSpawnHolder);
		carSpawnObject.transform.position = p_position;

		carSpawnScript.SetUp(p_direction, _gameManager._roundSystemManager.GetVehecleJSON());
		return carSpawnScript;
	}
	
	private void SetCarSpawnGenerator(float p_streetBottomPosition, int p_carSlot) {
		float centerAxisX = streetObject.transform.position.x,
		halfWidth = (streetObject.size.x / 2),
		xLeftPos = centerAxisX-halfWidth,
		xRightPos = centerAxisX+halfWidth;

		for ( int i = 0; i < p_carSlot; i++) {
			bool isFaceLeft = UtilityColl.FlipCoin(0.5f);

			float carSpawnYPos = p_streetBottomPosition + (slotheight * i) + (slotheight/2f);
			Vector2 carSpawnPos = new Vector2( (isFaceLeft) ?  xLeftPos : xRightPos,
											 	carSpawnYPos);
			Vector2 carDirection = (isFaceLeft) ?  Vector2.right :Vector2.left;

			CarSpawn gCarSpawn = CreateCarSpawn( carSpawnPos, carDirection);
			// Vector2 carSpawnPosition,
			// spawnDir = ;
			carSpawnPointList.Add(gCarSpawn);
		}
	}

	//================================================================ GrandMa Setting ================================================================
	public void SetGrandMaStartingLine(Vector2 p_position) {
		Transform grandmaStartlineObject = transform.Find("OldLaddySpawnList/TargetLine");
		grandmaStartlineObject.position = p_position;
	}


	// Player Setting	
	public void SetPlayerStartingLine(Vector2 p_position) {
		Player player = transform.Find("Player").GetComponent<Player>();
		player.transform.position = p_position;
	}


	public void Clear() {
		foreach (CarSpawn carSpawn in carSpawnPointList) {
			GameObject.Destroy(carSpawn);
		}
		carSpawnPointList.Clear();

	}

}
