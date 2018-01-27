using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	[HideInInspector]
	public MapManager _mapManager;
	public RoundSystemManager _roundSystemManager;


    [SerializeField]
    public int Score;
    public int speedBaseLine = 5;
    public static GameManager instance;

    public JSONObject characterJSON, vehecleJSON, roundJSON;
    // public string round_id;

	//Where everything start
	void Awake () {
        instance = this;

        _roundSystemManager = GetComponent<RoundSystemManager>();
		_mapManager = GameObject.Find("BackGround").GetComponent<MapManager>();
		Initialize();
	}

    public void addPoint(int Point)
    {
        Score += Point;
    }

    public void gameOver()
    {
        Debug.Log("gameover");
    }

	void Initialize() {
        //Set json file
        characterJSON =  new JSONObject( Resources.Load<TextAsset>("Database/Character").text );
        vehecleJSON = new JSONObject( Resources.Load<TextAsset>("Database/VehecleType").text );
        roundJSON =  new JSONObject( Resources.Load<TextAsset>("Database/Rounds").text );

        _roundSystemManager.SetUp(roundJSON);
        _mapManager.Setup(this, 5);
	}

    private void SetGameConfig() {
        
    }

    public JSONObject GetJSONComponent(string p_key) {
        if (vehecleJSON.HasField(p_key)) return vehecleJSON.GetField(p_key);
        if (characterJSON.HasField(p_key)) return characterJSON.GetField(p_key);

        return null;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
