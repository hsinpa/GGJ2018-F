using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	[HideInInspector]
	public MapManager _mapManager;
	public RoundSystemManager _roundSystemManager;

    //UI Managers
	public UIManager _uiManager;

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
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

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
        
        _roundSystemManager.SetUp(roundJSON, 0);

        _uiManager.storyBoardManager.SetUp(storyboardJSON.GetField( "storyboard.1"), delegate {
            //Start Game after reading story
            SetGameConfig();
        } );
	}

    private void SetGameConfig() {
        JSONObject c_roundJSON = _roundSystemManager.currentRound;
        _mapManager.Setup(this, c_roundJSON.GetField("streetSlot").num);

        //Display round info
        SetRemindingMessage(c_roundJSON.GetField("name").str);
    }

    public void addPoint(int Point, string p_oldLaddy_id)
    {
        Score += Point;
        savedOldLaddyDic[p_oldLaddy_id]++;
        updateUI();

        //Check if victory
        if (Score >= _roundSystemManager.currentRound.GetField("win_score").num) {
            RoundEnd();
        }
    }

    public void removeOldLaddy(string p_oldLaddy_id) {
        dieOldLaddyDic[p_oldLaddy_id]++;
        updateUI();

    }

    public void SetRemindingMessage(string p_message) {
        _uiManager.roundTextAnimator.GetComponent<Text>().text = p_message;
        _uiManager.roundTextAnimator.SetTrigger("Display");
    }

    public void gameOver()
    {
        SetRemindingMessage("Game Over");

        string story_id = _roundSystemManager.currentRound.GetField("lose_storyboard").str;
         _uiManager.storyBoardManager.SetUp( storyboardJSON.GetField(story_id), delegate {
            
            //Restart game after reading story
            ReloadScene();
        } );
    }  

    //Successully win this round
    public void RoundEnd() {
        if (_roundSystemManager.SetNextRound()) {
            //Do something here
            SetGameConfig();
        } else {
            //Text for last round (if win)
            // string winText = _roundSystemManager.currentRound.GetField("win_text").str;
            string story_id = _roundSystemManager.currentRound.GetField("win_storyboard").str;
            _uiManager.storyBoardManager.SetUp(storyboardJSON.GetField(story_id), delegate {
                //Start Game after reading story
                ReloadScene();
            } );
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

    private void ReloadScene() {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
