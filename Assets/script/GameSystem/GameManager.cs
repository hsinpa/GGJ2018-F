using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	[HideInInspector]
	public MapManager _mapManager;
    [SerializeField]
    public int Socore;
    public static GameManager instance;
	//Where everything start
	void Awake () {
        instance = this;

		_mapManager = GameObject.Find("BackGround").GetComponent<MapManager>();
		Initialize();

	}

    public void addPoint(int Point)
    {
        Socore += Point;
    }

    public void gameOver()
    {
        Debug.Log("gameover");
    }

	void Initialize() {
		_mapManager.Setup(this, 5);
	}

    private void OnDestroy()
    {
        instance = null;
    }
}
