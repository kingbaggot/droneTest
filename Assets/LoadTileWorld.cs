using System.Collections;
using System.Collections.Generic;
using System.Xml;

using UnityEngine;

public class LoadTileWorld : MonoBehaviour {

    // Use this for initialization
    public Arrays myArrays;
    public TileWorld myTileWorldScript;
    
    private List<List<int>> aTileMap = new List<List<int>>();//holds the final array of bgtile data
    private List<List<int>> bTileMap = new List<List<int>>();//holds the final array of object data
    private List<List<int>> cTileMap = new List<List<int>>();//holds the final array of collision data

    public static string ToOuterXml(System.Xml.Linq.XDocument xmlDoc)
    {
        return xmlDoc.ToString();
    }

    void Start ()
    {
        trace("INIT LTW");

        myArrays = this.GetComponent<Arrays>();

        trace("myArray=" + myArrays + "<");
        
        System.Xml.Linq.XDocument xd = System.Xml.Linq.XDocument.Load(new System.Uri("levelData.xml", System.UriKind.Relative).ToString());

        //// parse this xml - stick it into the MapData lists - which are blank at the mo.

        XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
        xmlDoc.LoadXml(ToOuterXml(xd)); // load the file.
        XmlNodeList levelsList = xmlDoc.GetElementsByTagName("Levels");

        foreach (XmlNode levelInfo in levelsList)
        {
            XmlNodeList levelcontent = levelInfo.ChildNodes;
            foreach (XmlNode levelsItens in levelcontent) // levels itens nodes.
            {
                XmlNodeList levelData = levelsItens.ChildNodes;

                List<List<int>> tempLevList = new List<List<int>>();
                myArrays.mapDATA[0].Add(tempLevList);

                //Debug.Log("--new Level---");

                foreach (XmlNode levelDataLines in levelData) // levels itens nodes.
                {
                    string levelRowString = levelDataLines.InnerText;

                    List<int>  tempLevRowList = new List<int>();

                    //Debug.Log("--next line--");

                    string[] strArr = null; char[] splitchar = { ',' };
                    strArr = levelRowString.Split(splitchar);

                    foreach (string stringInt in strArr)
                    {
                        //Debug.Log("i see>" + int.Parse(stringInt));
                        tempLevRowList.Add(int.Parse(stringInt));
                    }

                    tempLevList.Add(tempLevRowList);
                }

            }
        }
        
        loadMapFlashData(0);

    }

    

    void trace(string outString)
    {
        Debug.Log(">" + outString);
    }

    public static int mapRows = 0;//holds the length of the mapin rows
    public static int mapCols = 0;//holds the width of the map in rows

    private float tileSize = 24;//32;
	private int screenWidth;
	private int screenHeight;
    
    //this is only going to happen once now. 
    private void loadMapFlashData(int f_level)
    {
        trace("-------------");
        trace("LOADING STAGE"+f_level+" arrays="+myArrays.mapDATA+"<");
        trace("-------------");

        Arrays.totalLevels = myArrays.mapDATA[f_level].Count;
        List<List<int>> fList0 = myArrays.mapDATA[f_level][Arrays.gameLevel];   // new List<List<int>>();// = Main.instance.alphaDat.MAPS_DATA[f_level][fRound]
        List<List<int>> fList = new List<List<int>>();

        foreach (List<int> thing in fList0)
        {
            fList.Add(thing);
        }
        
        mapRows = fList.Count;
        mapCols = fList[0].Count;

        for (int rowCtr = 0; rowCtr < mapRows; rowCtr++)
        {

            List<int> tempArray = new List<int>();
            List<int> tempArray2 = new List<int>();
            
            for (int colCtr = 0; colCtr < mapCols; colCtr++)
            {

                var cellVal = fList[rowCtr][colCtr];
                
                if (cellVal != Arrays.T_BLANK)
                {
                    addGameObject(cellVal, colCtr, rowCtr);// (colCtr * tileSize), (rowCtr * tileSize));
                }

                //making bg list arrays
                tempArray.Add(cellVal);
              

            }
            aTileMap.Add(tempArray); /// backtiles 
        }

        for (int rowCtr = 0; rowCtr < mapRows; rowCtr++)
        {
            for (int colCtr = 0; colCtr < mapCols; colCtr++)
            {
                int tileNum = (aTileMap[rowCtr][colCtr]);
                int destY = rowCtr;
                int destX = colCtr;

                myTileWorldScript.addTile(destX, destY);
                
            }
        }
       
        myTileWorldScript.levelBegin();
        
    }

    private void addGameObject(int type, int xpos, int ypos)
    {
        
        switch (type)
        {
            case (Arrays.T_PLAYER):
            myTileWorldScript.addPlayer(xpos, ypos);
            break;
                
            case (Arrays.T_EXIT):
            myTileWorldScript.addExit(xpos, ypos);
            break;

            case (Arrays.T_BADDIE):
            myTileWorldScript.addBaddie(xpos, ypos);
            break;
        }

    }
    

   
}
