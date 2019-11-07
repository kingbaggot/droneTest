using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaddieObjClass : TileObjectClass {

    List<Color> colList = new List<Color> { Color.yellow, Color.white, Color.cyan };

    public void Start()
    {
        myObjType = objTypes.baddie;
        heightFloat = 100;
        heightClipRt.gameObject.GetComponent<Image>().color = Color.red;
    }

    public override void specificUpdate()
    {
        heightFloat = 60 + (25 * Mathf.Sin(Mathf.Deg2Rad * Time.time * 50));

        if(GLOW)
        {
            heightClipRt.gameObject.GetComponent<Image>().color = colList[Random.Range(0, 3)];
            print("GLOWING OK!");
        }
    }

   

}
