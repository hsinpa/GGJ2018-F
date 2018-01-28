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
        Idle,
        MoveOut
    }

    public float waitTime=3f;
    public float speed = 0.4f;
    public int point = 50;
    private status _oldLaddyStatus;
    private float _endLineY;
    private float time=0;
    private string type = "";
    private string _id;

    public void init(Vector3 position, JSONObject p_characterJSONComp, string p_oldladdy_id, Sprite p_sprite)
    {
        _id = p_oldladdy_id;
        speed = GameManager.instance.speedBaseLine * ( p_characterJSONComp.GetField("speed").n);
        point = p_characterJSONComp.GetField("score").num;
        
        GetComponent<SpriteRenderer>().sprite = p_sprite;

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
                    _oldLaddyStatus = status.MoveOut;
                    MoveOut();
                }
                break;



        }
    }
    /// <summary>
    /// 結束動畫 離場
    /// </summary>
    public void MoveOut()
    {
        GameManager.instance.addPoint(point, _id);
        HUDManager.instance.ShowHUD("+" + point.ToString(), transform.position);
        gameObject.transform.DOMoveY(transform.position.y + 30,30 / speed).OnComplete(()=>Destroy(gameObject));

    }

    public void die()
    {
        SoundManager.instance.plauMusic("shriek");
        GameManager.instance.removeOldLaddy(_id);
        HUDManager.instance.ShowHUD("Die", transform.position);
        Destroy(gameObject);
    }

    public void onDetect(CarBase _car)
    {

    }

    public void meetCar(CarBase _car)
    {
        die();
    }

    public void meetPlayer(Player _player)
    {
       
    }

    public void carry(Player _player)
    {
        _oldLaddyStatus = status.BeHelp;
        _player.addBuff(new BuffData(1, speed, 0f));
    }

    public void abondon(Player _player)
    {
        _oldLaddyStatus = status.Moving;
        _player.removeBuff(1);
    }
}
