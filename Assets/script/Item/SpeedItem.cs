using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem :MonoBehaviour,  IEffectItem
{

    public void meetCar(CarBase _car)
    {
        
    }

    public void meetPlayer(Player _player)
    {
        _player.addBuff(new BuffData(2, 0, 0.5f));
    }

    public void onDetect(CarBase _car)
    {
        
    }
}
