using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjClass : TileObjectClass
{
    bool SPIN = false;
    bool LAND = false;

    public void Start()
    {
        myObjType = objTypes.player;
        heightFloat = 120;
    }

    public override void specificUpdate()
    {
        if(SPIN)
        {
        heightClipRt.localEulerAngles = new Vector3(0, 0, Time.time * 1000.0f);
        }else if(LAND)
        {
        heightFloat *= 0.9f;
        }
        else
        {
        heightFloat = 80 + (50 * Mathf.Sin(Mathf.Deg2Rad * Time.time * 100));
        }
       
       //
    }

    public void startSpin()
    {
        SPIN = true;
    }

    public void startLand()
    {
        LAND = true;
    }

}



