using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitObjClass : TileObjectClass
{
    public void Start()
    {
        myObjType = objTypes.exit;
        print("exit is " + myObjType);
    }

    void specificUpdate()
    {

    }
	
}
