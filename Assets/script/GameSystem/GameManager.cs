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


    public List<string> savedOldLaddyList = new List<string>();
    public List<string> dieOldLaddyList = new List<string>();

    public JSONObject characterJSON, vehicleJSON, roundJSON;
    // public string round_id;

	//Where everything start
	void Awake () {
        instance = this;

        _roundSystemManager = GetComponent<RoundSystemManager>();
		_mapManager = GameObject.Find("BackGround").GetComponent<MapManager>();
		Initialize();
	}

    void Initialize() {
        //Set json file
        characterJSON =  new JSONObject( Resources.Load<TextAsset>("Database/Character").text );
        vehicleJSON = new JSONObject( Resources.Load<TextAsset>("Database/VehicleType").text );
        roundJSON =  new JSONObject( Resources.Load<TextAsset>("Database/Rounds").text );

        _roundSystemManager.SetUp(roundJSON, 2);

        //_roundSystemManager.currentRound.GetField("streetSlot").num
        _mapManager.Setup(this, 4 );
	}

    private void SetGameConfig() {
        
    }

    public void addPoint(int Point, string p_oldLaddy_id)
    {
        Score += Point;
        savedOldLaddyList.Add(p_oldLaddy_id);
    }

    public void removeOldLaddy(string p_oldLaddy_id) {
        dieOldLaddyList.Add(p_oldLaddy_id);
    }

    public void gameOver()
    {
        Debug.Log("gameover");

        string loseText = _roundSystemManager.currentRound.GetField("lose_text").str;
        //Display it
    }  

    //Successully win this round
    public void RoundEnd() {
        if (_roundSystemManager.SetNextRound()) {
            //Do something here

        } else {
            //Text for last round (if win)
            string winText = _roundSystemManager.currentRound.GetField("win_text").str;

        }
    }

    public JSONObject GetJSONComponent(string p_key) {
        if (vehicleJSON.HasField(p_key)) return vehicleJSON.GetField(p_key);
        if (characterJSON.HasField(p_key)) return characterJSON.GetField(p_key);

        return null;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
