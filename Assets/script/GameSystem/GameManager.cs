using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	[HideInInspector]
	public MapManager _mapManager;

	//Where everything start
	void Awake () {
		_mapManager = GameObject.Find("BackGround").GetComponent<MapManager>();
		Initialize();

	}
	

	void Initialize() {
		_mapManager.Setup(this, 5);
	}

}
