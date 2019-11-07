using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrays : MonoBehaviour {

    // Use this for initialization

    // collision codes
    public const int C_ALL = 0;


    // tile codes
    public const int T_BLANK = 0;
    public const int T_PLAYER = 1;
    public const int T_EXIT = 2;
    public const int T_BADDIE = 3;
    
    public List<List<List<List<int>>>> mapDATA;

    public List<List<int>> bgBadList;

    public static int gameLevel = 0;
    public static int totalLevels = 0;

    void Awake()
    {
        Debug.Log("start Arrays");

        //set collision codes
        bgBadList = new List<List<int>>
        {
            new List<int> { T_BLANK,T_EXIT }
        };

        //make big blank list - will fill it from the xml.
        mapDATA = new List<List<List<List<int>>>>
        {
            new List<List<List<int>>>
            {
                //new List<List<int>>
                //{
                //new List<int> { 2,2,2,2,2,2,2,2 },
                //new List<int> { 2,2,2,2,2,2,2,2 },
                //new List<int> { 2,0,0,0,0,0,0,2 },
                //new List<int> { 2,0,0,4,4,0,0,2 },
                //new List<int> { 2,0,0,3,3,0,0,2 },
               // new List<int> { 2,0,0,0,0,0,0,2 },
                //new List<int> { 2,0,3,3,3,3,0,2 },
                //new List<int> { 2,0,0,1,0,0,0,2 },
                //new List<int> { 2,0,0,2,2,0,0,2 },
                //new List<int> { 2,2,2,2,2,2,2,2 }
                //}
            }

        };

	}
	
	
}
