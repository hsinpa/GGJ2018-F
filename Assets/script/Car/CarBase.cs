using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class CarBase : MonoBehaviour {


    enum status
    {
        Moving,
        Stop,
        Dead
    }

    private Animator _animator;
    public float speed = 5f;
    /// <summary>
    /// 撞人機率0-100
    /// </summary>
    public float stopProbability = 50f;
    public Vector2 direct;
    private status _carStatus;
    private JSONObject carJSON;

    /// <summary>
    /// 目前阻止車移動的物品 檢查沒東西才繼續前進
    /// </summary>
    private List<GameObject> _detectList=new List<GameObject>();

    public void init(string p_id, Vector2 _direct, JSONObject p_carJSON)
    {
        _animator = GetComponent<Animator>();
        transform.name = p_id;

        carJSON = p_carJSON;
        speed = GameManager.instance.speedBaseLine * ( p_carJSON.GetField("speed").n);
        stopProbability = p_carJSON.GetField("target_character").n;
        direct = _direct;
        transform.localScale = new Vector2((_direct.x == 1) ? -1 : 1 , 1 );
    }

	// Update is called once per frame
	void Update () {

        switch (_carStatus)
        {
            case status.Dead: break;
            case status.Moving:move(); break;
            case status.Stop:
                ////無阻擋務實再前進
                if (_detectList.Count == 0)
                    moving();
                break;
        }
	}

    //
    public bool CanDestoryFrontCar(string p_target_id ) {
        if (!carJSON.GetField("target_vehecle").HasField(p_target_id)) return false;
        float possibility = carJSON.GetField("target_vehecle").GetField(p_target_id).n;

        return UtilityColl.FlipCoin(possibility);
    }

    public void detectEnter(GameObject _obj)
    {
        if (_obj.CompareTag("person"))
        {
            ///機率性狀人
            if (Random.Range(0, 100f) >= stopProbability)
            {
                stop(_obj);
            }
        }
        else if (_obj.CompareTag("car"))
        {
            if (CanDestoryFrontCar(_obj.name)) {
                _obj.GetComponent<CarBase>().OnDestory();
            }
            else {
                stop(_obj);
            }
        }
        
        IEffectItem _effectItem = _obj.GetComponent<IEffectItem>();
        if (_effectItem != null)
        {
            _effectItem.onDetect(this);
        }
    }

    public void detectExit(GameObject _obj)
    {
        if (_detectList.Contains(_obj))
        {
            _detectList.Remove(_obj);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        IEffectItem _effectItem= collision.gameObject.GetComponent<IEffectItem>();
        if (_effectItem != null)
        {
            _effectItem.meetCar(this);
        }


    }





    public void moving()
    {
        _carStatus = status.Moving;
        _animator.SetBool("isStop", false);
    }

    /// <summary>
    /// 停下車
    /// </summary>
    /// <param name="_obj"></param>誰讓他停下
    public void stop(GameObject _obj)
    {
        _carStatus = status.Stop;
        _detectList.Add(_obj);
        _animator.SetBool("isStop", true);
    }


    public void move()
    {
        gameObject.transform.position += new Vector3(direct.x, direct.y, 0) * speed * Time.deltaTime;
    }

    public void OnDestory() {
        //Play some animation
        this.GetComponent<BoxCollider2D>().enabled = false;
        _carStatus = status.Dead;

        HUDManager.instance.ShowHUD("Destroy!!", transform.position);
        Debug.Log("Destory it");
        Destroy(gameObject, 2f);
    }



}
