using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEffectItem
{


    public float baseSpeed;
    public float speed = 0.3f;
    /// <summary>
    /// =碰到的所有道具
    /// </summary>
    [SerializeField]
    private List<GameObject> _touchItem = new List<GameObject>();
    /// <summary>
    /// 正帶的東西
    /// </summary>
    [SerializeField]
    private GameObject _carryItem;
    private List<BuffData> _buffDataList=new List<BuffData>();


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ///是否為可攜帶物件和未帶任何東西
        if (null != collision.gameObject.GetComponent<ICarryItem>())
        {
            _touchItem.Add(collision.gameObject);
        }
        IEffectItem _efffectItem = GetComponent<IEffectItem>();
        if(null!=_efffectItem)
        {
            _efffectItem.meetPlayer(this);


        }


                }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ///離開的是 我未帶的物件
        if (_touchItem.Contains(collision.gameObject))
        {
            _touchItem.Remove(collision.gameObject);
        }
    }


    public void help()
    {

        if (null != _carryItem)
        {

            _carryItem.transform.SetParent(null);
            _carryItem.GetComponent<ICarryItem>().abondon(this);
            _carryItem = null;
        }
        else if (_touchItem.Count > 0)
        {
            _carryItem = _touchItem[0];
            _carryItem.transform.parent = transform;
            _touchItem[0].GetComponent<ICarryItem>().carry(this);

        }
    }

    public void move(Vector2 direct)
    {
        gameObject.transform.position += new Vector3(direct.x, direct.y, 0) * speed * Time.deltaTime;
    }


    private void die()
    {
        HUDManager.instance.ShowHUD("死了 ㄏㄏ", transform.position);
        GameManager.instance.gameOver();
    }

    /// <summary>
    /// 速度加成
    /// </summary>
    /// <param name="BuffTimes"></param> 加成倍數
    public void speedBuff(float BuffTimes)
    {
        speed *= BuffTimes;

    }



    public void meetCar(CarBase _car)
    {
          die();
    }

    public void meetPlayer(Player _player)
    {
        
    }

    public void onDetect(CarBase _car)
    {
        
    }


    public void addBuff(BuffData _buffData )
    {
        _buffDataList.Add(_buffData);
        float _speedBase=baseSpeed,_buffScale=1;

        for (int i = 0; i < _buffDataList.Count; i++)
        {
            if(_buffDataList[i].speedBase!=0)
            _speedBase = _buffDataList[i].speedBase;
            _buffScale += _buffScale;
        }
        speed = (_speedBase * _buffScale);
    }

    public void removeBuff(int BuffID)
    {
        for (int i = 0; i < _buffDataList.Count; i++)
        {
            if (_buffDataList[i].buffID == BuffID)
            {
                _buffDataList.RemoveAt(i);
                break;
            }
        }



    }
}
