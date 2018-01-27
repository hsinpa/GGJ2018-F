using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IroadObject
{



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
    // Use this for initialization
    void Start()
    {
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ///是否為可攜帶物件和未帶任何東西
        if (null != collision.gameObject.GetComponent<ICarryItem>())
        {
            _touchItem.Add(collision.gameObject);
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


    public void onHit(CarBase _car)
    {
        die();
    }

    public void onDetect(CarBase _car)
    {

    }
}
