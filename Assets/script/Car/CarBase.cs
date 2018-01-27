﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBase : MonoBehaviour {


    enum status
    {
        Moving,
        Stop,

    }

    private Animator _animator;
    public float speed = 5f;
    /// <summary>
    /// 撞人機率0-100
    /// </summary>
    public float stopProbability = 50f;
    public Vector2 direct;
    private status _carStatus;
    /// <summary>
    /// 目前阻止車移動的物品 檢查沒東西才繼續前進
    /// </summary>
    private List<GameObject> _detectList=new List<GameObject>();
	// Use this for initialization
	void Start () {

        _animator = GetComponent<Animator>();
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
            stop(_obj);
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

    public void init(Vector2 _direct)
    {
        direct = _direct;
        transform.localScale = new Vector2((_direct.x == 1) ? -1 : 1 , 1 );
    }


	// Update is called once per frame
	void Update () {

        switch (_carStatus)
        {
            case status.Moving:move(); break;
            case status.Stop:
                ////無阻擋務實再前進
                if (_detectList.Count == 0)
                    moving();
                break;
        }

	}

    public void move()
    {
        gameObject.transform.position += new Vector3(direct.x, direct.y, 0) * speed * Time.deltaTime;
    }



}
