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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (_countDown >= _rebornTime)
        {
            _rebornTime = Random.Range(rebornMin, rebornMax);
            OldLaddy oldLaddy = GameObject.Instantiate(_oldLaddy, transform.position, transform.rotation, transform).GetComponent<OldLaddy>();
            oldLaddy.init(new Vector3(   TargetLine.transform.position.x+ Random.Range(-1*LineLong, LineLong), TargetLine.transform.position.y,0));

            _countDown = 0;
        }
        _countDown += Time.deltaTime;

    }
}
