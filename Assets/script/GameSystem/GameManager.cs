using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour {
	[HideInInspector]
	public MapManager _mapManager;
	public RoundSystemManager _roundSystemManager;

    //UI Managers
	public StoryBoardManager _storyboardManager;

    [SerializeField]
    public int Score=0;
    public int speedBaseLine = 5;
    public static GameManager instance;
    public delegate void systemDelegate();
    public event systemDelegate updateUI;


    public Dictionary<string, int> savedOldLaddyDic=new Dictionary<string, int>();
    public Dictionary<string, int> dieOldLaddyDic = new Dictionary<string, int>();
    public JSONObject characterJSON, vehicleJSON, roundJSON, storyboardJSON;
    // public string round_id;

	//Where everything start
	void Awake () {


        instance = this;
        _roundSystemManager = GetComponent<RoundSystemManager>();
		_mapManager = GameObject.Find("BackGround").GetComponent<MapManager>();
        _storyboardManager = GameObject.Find("Canvas/StoryBoard").GetComponent<StoryBoardManager>();

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
        storyboardJSON =   new JSONObject( Resources.Load<TextAsset>("Database/StoryBoard").text );

        ///讀腳色表 鍵Dictionary
        ////跳過第一個
        for (int i = 1; i < characterJSON.Count; i++)
        {
            savedOldLaddyDic.Add(characterJSON.keys[i],0);
            dieOldLaddyDic.Add(characterJSON.keys[i], 0);
        }
        updateUI();
        


        _roundSystemManager.SetUp(roundJSON, 2);

        _storyboardManager.SetUp(storyboardJSON.GetField( "storyboard.1"), delegate {
            //Start Game after reading story
            SetGameConfig();
        } );
	}

    private void SetGameConfig() {
        JSONObject c_roundJSON = _roundSystemManager.currentRound;
        _mapManager.Setup(this, c_roundJSON.GetField("streetSlot").num);

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

        string story_id = _roundSystemManager.currentRound.GetField("lose_storyboard").str;
         _storyboardManager.SetUp( storyboardJSON.GetField(story_id), delegate {
            
            //Restart game after reading story
            Initialize();
        } );
    }  

    //Successully win this round
    public void RoundEnd() {
        if (_roundSystemManager.SetNextRound()) {
            //Do something here

        } else {
            //Text for last round (if win)
            // string winText = _roundSystemManager.currentRound.GetField("win_text").str;

        }
    }

    public JSONObject GetJSONComponent(string p_key) {
        if (vehicleJSON.HasField(p_key)) return vehicleJSON.GetField(p_key);
        if (characterJSON.HasField(p_key)) return characterJSON.GetField(p_key);
        if (storyboardJSON.HasField(p_key)) return storyboardJSON.GetField(p_key);

        return null;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
