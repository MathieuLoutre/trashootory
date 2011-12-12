using UnityEngine;
using System.Collections;

public class catcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        vrButtons buttons = null;
        		
        if (MiddleVR.VRDeviceMgr != null) {
        	buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0");
        }
        	
        if (buttons != null && buttons.IsPressed(0))
        {
            transform.renderer.enabled = false;            
        }
        else transform.renderer.enabled = true;            
	}
}
