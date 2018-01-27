using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCtrl : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    Player _player;
    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;
    public KeyCode help;




    // Use this for initialization
    void Start () {
        _player= player.GetComponent<Player>();
 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(up))
            _player.move(new Vector2(0,1));
        if (Input.GetKey(down))
            _player.move(new Vector2(0, -1));
        if (Input.GetKey(right))
            _player.move(new Vector2(1, 0));
        if (Input.GetKey(left))
            _player.move(new Vector2(-1, 0));
        if (Input.GetKeyDown(help))
            _player.help();
        if (Input.GetKeyUp(help))
            _player.help();


    }
}
