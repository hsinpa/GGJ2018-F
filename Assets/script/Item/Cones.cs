using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cones : MonoBehaviour, ICarryItem,IEffectItem
{

    private bool isUseable = false;


    private void Start()
    {
        isUseable = false;
    }


    public void meetCar(CarBase _car)
    {
        
    }

    public void meetPlayer(Player _player)
    {
        
    }

    public void onDetect(CarBase _car)
    {
        if(isUseable)
        _car.stop(gameObject);
    }

    public void carry(Player _player)
    {
        isUseable = false;
    }

    public void abondon(Player _player)
    {
        isUseable = true;
    }
}
