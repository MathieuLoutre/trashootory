using UnityEngine;
using System.Collections;

public class VRActor : MonoBehaviour {

    public bool Trigger = true;

	// Use this for initialization
	void Start () {
        if (gameObject.rigidbody == null)
        {
            Rigidbody body;
            body = gameObject.AddComponent("Rigidbody") as Rigidbody;
            body.isKinematic = true;
        }

        if(gameObject.collider == null)
        {
            gameObject.AddComponent("BoxCollider");
        }

        if (gameObject.collider != null && Trigger )
        {
            gameObject.collider.isTrigger = true;
        }

        if (gameObject.collider == null)
        {
            print("Actor object has no collider !! Put one or it won't act." + gameObject.name );
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
