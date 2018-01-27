using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OldLaddy : MonoBehaviour, IEffectItem,ICarryItem
{


    public enum status
    {
        Moving,
        BeHelp,
        Idle

    }

    public float waitTime=3f;
    public float speed = 0.4f;
    public int point = 50;
    private status _oldLaddyStatus;
    private float _endLineY;
    private float time=0;
    public void init(Vector3 position)
    {
        ///算離目標多遠
        float far = Vector3.Distance(transform.position,position);
        //等速到目標地
        transform.DOMove(position, far/speed).OnComplete(toIdle);
        _endLineY = GameManager.instance._mapManager.getEndY();
    }




    public void toIdle()
    {
        time = 0;
        _oldLaddyStatus = status.Idle;
    }


    private void Update()
    {
        switch (_oldLaddyStatus)
        {
            case status.Idle:
                if (time > waitTime)
                {
                    _oldLaddyStatus= status.Moving;
                }
                time += Time.deltaTime;
                break;
            case status.Moving:
                ///向前衝阿
                gameObject.transform.position += Vector3.up * speed * Time.deltaTime;
                if (transform.position.y > _endLineY)
                {
                    GameManager.instance.addPoint(point);
                    HUDManager.instance.ShowHUD(point.ToString(), transform.position);
                }
                break;
        }
    }


    public void die()
    {
        HUDManager.instance.ShowHUD("Die", transform.position);
        Destroy(gameObject);

    }

    public void onDetect(CarBase _car)
    {

    }

    public void meetCar(CarBase _car)
    {
             Debug.Log("HIT");
        die();
    }

    public void meetPlayer(Player _player)
    {
       
    }

    public void carry(Player _player)
    {
        _oldLaddyStatus = status.BeHelp;
        _player.speedBuff(0.5f);
    }

    public void abondon(Player _player)
    {
        _oldLaddyStatus = status.Moving;
        _player.speedBuff(2f);
    }
}
