using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IroadObject  {


    void onHit(CarBase _car);

    void onDetect(CarBase _car);
}
