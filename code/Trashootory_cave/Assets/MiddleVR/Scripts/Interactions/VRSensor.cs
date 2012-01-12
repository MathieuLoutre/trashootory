using UnityEngine;
using System.Collections;

public class VRSensor : MonoBehaviour {
    public string ReactToActor = "*";

	// Use this for initialization
	void Start () {
        if (gameObject.collider == null)
        {
            BoxCollider mycollider;
            mycollider = gameObject.AddComponent("BoxCollider") as BoxCollider;
            mycollider.isTrigger = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider other )
    {
        //print("Enter (looking for:" + ReactToActor +", hit by:" + other.gameObject.name );
        if( ReactToActor == "*" || other.gameObject.name == ReactToActor )
        {
            //print("Sensing : " + other.gameObject.name);
            SendMessage("Action");
        }
    }
}
