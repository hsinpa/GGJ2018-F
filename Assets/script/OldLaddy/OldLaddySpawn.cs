using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

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
    Sprite[] oldWomanImages;

    private bool activate = false;
	// Use this for initialization
	public void SetUp (RoundSystemManager p_roundManager) {
        characterJSON =  p_roundManager.GetCharacterJSON();
        activate = true;

        oldWomanImages = Resources.LoadAll<Sprite>("Sprite/OldWoman");
	}

	// Update is called once per frame
	void Update () {

        if (_countDown >= _rebornTime && activate)
        {
            int randomCharacterIndex = Random.Range(0, characterJSON.Count - 1);
            string oldWomanID = characterJSON[randomCharacterIndex].GetField("id").str;
            JSONObject characterComp= GameManager.instance.GetJSONComponent( oldWomanID );

            Sprite oldWomanSprite =  UtilityColl.LoadSpriteFromMulti(oldWomanImages, oldWomanID);
            _rebornTime = characterJSON[randomCharacterIndex].GetField("frequency").num;
            OldLaddy oldLaddy = GameObject.Instantiate(_oldLaddy, transform.position, transform.rotation, transform).GetComponent<OldLaddy>();
            oldLaddy.init(new Vector3(   TargetLine.transform.position.x+ Random.Range(-1*LineLong, LineLong), TargetLine.transform.position.y,0),
                characterComp, oldWomanID , oldWomanSprite
            );

            _countDown = 0;
        }
        _countDown += Time.deltaTime;

    }
}
