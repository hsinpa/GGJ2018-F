using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldLaddySpawn : MonoBehaviour {

  

    [SerializeField]
    private float rebornMin, rebornMax;
    [SerializeField]
    private GameObject _oldLaddy;
    private float _countDown = 0;
    [SerializeField]
    private GameObject TargetLine;
    [SerializeField]
    private float LineLong = 10;
    private float _rebornTime = 5;

    RoundSystemManager roundSystemManager;
    List<JSONObject> characterJSON;
    private bool activate = false;
	// Use this for initialization
	public void SetUp (RoundSystemManager p_roundManager) {
        characterJSON =  p_roundManager.GetCharacterJSON();
        activate = true;
	}
	
    


	// Update is called once per frame
	void Update () {

        if (_countDown >= _rebornTime && activate)
        {
            int randomCharacterIndex = Random.Range(0, characterJSON.Count - 1);
            JSONObject characterComp= GameManager.instance.GetJSONComponent( characterJSON[randomCharacterIndex].GetField("id").str );

            
            _rebornTime = characterJSON[randomCharacterIndex].GetField("frequency").num;
            OldLaddy oldLaddy = GameObject.Instantiate(_oldLaddy, transform.position, transform.rotation, transform).GetComponent<OldLaddy>();
            oldLaddy.init(new Vector3(   TargetLine.transform.position.x+ Random.Range(-1*LineLong, LineLong), TargetLine.transform.position.y,0),
                characterComp
            );

            _countDown = 0;
        }
        _countDown += Time.deltaTime;

    }
}
