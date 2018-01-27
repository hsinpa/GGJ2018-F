using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 觸發行道具
/// </summary>
public interface IEffectItem  {


    void meetCar(CarBase _car);
    /// <summary>
    /// 被車輛偵測時
    /// </summary>
    /// <param name="_car"></param>
    void onDetect(CarBase _car);

    void meetPlayer(Player _player);

}
