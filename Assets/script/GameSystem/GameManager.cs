using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour {
	[HideInInspector]
	public MapManager _mapManager;
	public RoundSystemManager _roundSystemManager;


    [SerializeField]
    public int Score=0;
    public int speedBaseLine = 5;
    public static GameManager instance;
    public delegate void systemDelegate();
    public event systemDelegate updateUI;


    public Dictionary<string, int> savedOldLaddyDic=new Dictionary<string, int>();
    public Dictionary<string, int> dieOldLaddyDic = new Dictionary<string, int>();
    public JSONObject characterJSON, vehicleJSON, roundJSON;
    // public string round_id;

	//Where everything start
	void Awake () {
        instance = this;

        _roundSystemManager = GetComponent<RoundSystemManager>();
		_mapManager = GameObject.Find("BackGround").GetComponent<MapManager>();
	
	}

    private void Start()
    {
        	Initialize();
    }

    void Initialize() {
        //Set json file
        characterJSON =  new JSONObject( Resources.Load<TextAsset>("Database/Character").text );
        vehicleJSON = new JSONObject( Resources.Load<TextAsset>("Database/VehicleType").text );
        roundJSON =  new JSONObject( Resources.Load<TextAsset>("Database/Rounds").text );
        _roundSystemManager.SetUp(roundJSON, 2);




        ///讀腳色表 鍵Dictionary
        ////跳過第一個
        for (int i = 1; i < characterJSON.Count; i++)
        {
            savedOldLaddyDic.Add(characterJSON.keys[i],0);
            dieOldLaddyDic.Add(characterJSON.keys[i], 0);
        }
        updateUI();
        
        //_roundSystemManager.currentRound.GetField("streetSlot").num
        _mapManager.Setup(this, 4 );
	}

    private void SetGameConfig() {
        
    }

    public void addPoint(int Point, string p_oldLaddy_id)
    {
        Score += Point;
        savedOldLaddyDic[p_oldLaddy_id]++;
        updateUI();
    }

    public void removeOldLaddy(string p_oldLaddy_id) {
        dieOldLaddyDic[p_oldLaddy_id]++;
        updateUI();

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
