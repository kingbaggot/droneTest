using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileWorld : MonoBehaviour {

    
    public static float tileWidth = 0;

    GameObject skyLayer;
    GameObject objLayer;
    GameObject bgLayer;
    public ControlClass myControl;
    public PlayerObjClass myPlayerClass;

    List<TileObjectClass> objList = new List<TileObjectClass> { };
    List<TileObjectClass> bgList = new List<TileObjectClass> { };

    bool LEVEL_IN_PLAY = false;
    bool PLAYER_DEAD = false;

    public Text textObj;
    public Canvas tileCanvas;

    public GameObject baddiePrefab;
    public GameObject exitPrefab;
    public GameObject tilePrefab;
    public GameObject playerPrefab;

    List<Color> greyList = new List<Color> { new Color(0.6f,0,6f,0.6f), new Color(0.65f, 0, 65f, 0.65f) };


    void Awake ()
    {
        objLayer = new GameObject("objLayer", typeof(RectTransform));
        bgLayer = new GameObject("bgLayer", typeof(RectTransform));
        skyLayer =new GameObject("skyLayer", typeof(RectTransform));

        bgLayer.transform.SetParent(gameObject.transform, false);
        objLayer.transform.SetParent(gameObject.transform, false);
        skyLayer.transform.SetParent(gameObject.transform, false);
    }
    
    public void addPlayer(int xpos,int ypos)
    {
        GameObject myPlayer = (GameObject)Instantiate(playerPrefab) as GameObject;
        myPlayer.transform.SetParent(objLayer.transform, false);
        
        myPlayer.AddComponent<PlayerObjClass>().initObj(xpos, ypos,skyLayer);
        myPlayerClass = myPlayer.GetComponent<PlayerObjClass>();
        objList.Add(myPlayer.GetComponent<TileObjectClass>());
        myPlayerClass.moveComplete += objMoveComplete;
    }

    public void addExit(int xpos, int ypos)
    {
        GameObject exit = (GameObject)Instantiate(exitPrefab);
        exit.transform.SetParent(objLayer.transform, false);
        exit.AddComponent<ExitObjClass>().initObj(xpos,ypos);
        objList.Add(exit.GetComponent<TileObjectClass>());
    }

    public void addBaddie(int xpos, int ypos)
    {
        GameObject tempBaddie = (GameObject)Instantiate(Resources.Load("baddiePrefab"));
        tempBaddie.transform.SetParent(objLayer.transform, false);
        tempBaddie.AddComponent<BaddieObjClass>().initObj(xpos, ypos,skyLayer);
        objList.Add(tempBaddie.GetComponent<TileObjectClass>());
        
    }

    public void addTile(int xpos, int ypos)
    {
        GameObject tempTile = (GameObject)Instantiate(tilePrefab);
        tempTile.transform.SetParent(bgLayer.transform, false);
        tempTile.AddComponent<TileObjectClass>().initObj(xpos, ypos);
        bgList.Add(tempTile.GetComponent<TileObjectClass>());

        tempTile.GetComponent<Image>().color = greyList[(xpos + ypos) % 2];
       
    }

    public void levelBegin()
    {
      
        float padding = 0.90f;
        tileWidth = (tileCanvas.GetComponent<RectTransform>().rect.width * padding) / (LoadTileWorld.mapCols * 1.0f);

        foreach (TileObjectClass bg in bgList)
        {
            bg.updateObj();
        }

        // centre tile world.
        float bgShift = (   (tileCanvas.GetComponent<RectTransform>().rect.width * padding) / 2.0f) - tileWidth / 2.0f; 

        bgLayer.GetComponent<RectTransform>().anchoredPosition = new Vector2(-bgShift, -bgShift);
        objLayer.GetComponent<RectTransform>().anchoredPosition = skyLayer.GetComponent<RectTransform>().anchoredPosition = bgLayer.GetComponent<RectTransform>().anchoredPosition;

        //add control callback.
        myControl.myButAction += butHit;

        LEVEL_IN_PLAY = true;
    }
    
    void butHit(ControlClass.butMove butIs)
    {
        if (!LEVEL_IN_PLAY) { return; }

        switch (butIs)
        {
            case ControlClass.butMove.up:
                myPlayerClass.setMove(0, -1);
                break;
            case ControlClass.butMove.down:
                myPlayerClass.setMove(0, 1);
                break;
            case ControlClass.butMove.left:
                myPlayerClass.setMove(-1, 0);
                break;
            case ControlClass.butMove.right:
                myPlayerClass.setMove(1, 0);
                break;
        }
    }

    void objMoveComplete(TileObjectClass.objTypes objType)
    {
        // check against all other object x
        if(objType == TileObjectClass.objTypes.player)
        {
            foreach(TileObjectClass obj in objList)
            {
                if(obj.myX == myPlayerClass.myX && obj.myY == myPlayerClass.myY && obj.myObjType != TileObjectClass.objTypes.player && LEVEL_IN_PLAY)
                {

                    if (obj.myObjType == TileObjectClass.objTypes.exit)
                    {
                        showText("LEVEL OOP");
                        obj.gameObject.GetComponent<Image>().color = Color.cyan;
                        Invoke("nextLevel", 1.5f);
                        LEVEL_IN_PLAY = false;
                        myPlayerClass.startLand();
                    }
                    else
                    {
                        showText("OH DEAR");
                        Invoke("gameOver", 1.5f);
                        obj.GLOW = true;
                        PLAYER_DEAD = true;
                        LEVEL_IN_PLAY = false;
                        myPlayerClass.startSpin();
                    }

                }
            }
        }
    }
    
    void gameOver()
    {
        Arrays.gameLevel = 0;
        SceneManager.LoadScene("gameEngine");
    }

    void nextLevel()
    {
        Arrays.gameLevel++;
        if (Arrays.gameLevel > Arrays.totalLevels-1) { Arrays.gameLevel = 0; }
        SceneManager.LoadScene("gameEngine");
    }

    void showText(string inString)
    {
        textObj.text = inString;
    }

    void Update ()
    {
		
        foreach(TileObjectClass obj in objList)
        {
            obj.updateObj();
        }

	}
}
