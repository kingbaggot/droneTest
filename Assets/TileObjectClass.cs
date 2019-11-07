using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileObjectClass : MonoBehaviour {

    public enum objTypes { tile,player,exit,baddie };
    public objTypes myObjType;
    public System.Action<objTypes> moveComplete;

    public bool IS_MOVING = false;
    public bool IS_OVER = false;
    public bool GLOW = false;
    public float myX = 0;
    public float myY = 0;
    public float heightFloat = 0;

    public float targX = 0;
    public float targY = 0;

    float tileWidth = 0;
    float moveStep = 0;
    float moveMax = 5;
    

    RectTransform rt;
    public RectTransform heightClipRt;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	public void initObj(float x, float y, GameObject skyLayer = null)
    {
        rt = this.GetComponent<RectTransform>();
        myX = targX = x;
        myY = targY = y;
        
        if(skyLayer != null)
        {
            heightClipRt = this.gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
            heightClipRt.gameObject.transform.SetParent(skyLayer.transform, true);
        }
    }

    public void setMove(int xm, int ym)
    {
        if (!IS_MOVING)
        {
            IS_MOVING = true;
            IS_OVER = false;
            targX = myX + xm;
            targY = myY + ym;

            if (targX < 0 || targX > LoadTileWorld.mapCols - 1) { targX = myX + xm * 0.2f; IS_OVER = true; };
            if (targY < 0 || targY > LoadTileWorld.mapRows - 1) { targY = myY + ym * 0.2f; IS_OVER = true; };

            moveStep = 0;
        }
    }

    public void updateObj()
    {
        float xShift = 0;
        float yShift = 0;

        if (IS_MOVING)
        {
            moveStep += Time.deltaTime * 10.0f;
            float movePerc = Mathf.Sin(   (moveStep / moveMax) * (90.0f * Mathf.Deg2Rad)    );

            if (IS_OVER)
            {
            movePerc = Mathf.Sin((moveStep / moveMax) * (180.0f * Mathf.Deg2Rad));
            }

            xShift = (targX - myX) * movePerc;
            yShift = (targY - myY) * movePerc;
           
            if (moveStep > moveMax)
            {
                moveStep = moveMax;
                xShift = yShift = 0;

                if (!IS_OVER)
                {
                    myX = targX;
                    myY = targY;
                }

                IS_MOVING = false;

                moveComplete(myObjType);
            }

        }
       
        rt.anchoredPosition = new Vector2((myX + xShift) * TileWorld.tileWidth, (myY + yShift) * TileWorld.tileWidth);
        rt.localScale = new Vector2(TileWorld.tileWidth/100.0f, TileWorld.tileWidth / 100.0f);

        if(heightClipRt)
        {
            heightClipRt.anchoredPosition = rt.anchoredPosition - new Vector2(0, heightFloat);
            heightClipRt.localScale = rt.localScale;
        }

        specificUpdate();
    }

    public virtual void specificUpdate()
    {
        
    }
}
