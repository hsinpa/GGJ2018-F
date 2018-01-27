using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData
{

    public BuffData(int _buffID, float _speedBase, float _speedScale)
    {
        buffID = _buffID;
        speedBase = _speedBase;
        speedScale = _speedScale;
    }

    public int buffID;
    public float speedBase;
    public float speedScale;

}
