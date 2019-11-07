using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlClass : MonoBehaviour {

    public GameObject butUp, butDown, butLeft, butRight;
    public enum butMove { up,down,left,right};
    public System.Action<butMove> myButAction;

    // Use this for initialization
    void Start () {

        butUp.AddComponent<Button>().onClick.AddListener(() => butHit(butMove.up));
        butDown.AddComponent<Button>().onClick.AddListener(() => butHit(butMove.down));
        butLeft.AddComponent<Button>().onClick.AddListener(() => butHit(butMove.left));
        butRight.AddComponent<Button>().onClick.AddListener(() => butHit(butMove.right));


    }

    public void butHit(butMove butMoveIs)
    {
        if(myButAction != null)
        {
            myButAction(butMoveIs);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
