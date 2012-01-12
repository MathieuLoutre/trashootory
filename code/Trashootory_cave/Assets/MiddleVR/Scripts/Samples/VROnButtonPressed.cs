using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class VROnButtonPressed : MonoBehaviour {
    public string Device = null;
    public int ButtonIndex = 0;

    private vrButtons m_Buttons = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if( m_Buttons == null )
        {
            m_Buttons = MiddleVRTools.GetButtons(Device);
        }

        if (m_Buttons != null)
        {
            if (m_Buttons.IsToggled((uint)ButtonIndex))
            {
                print("Button pressed !");
            }
        }
        else
        {
            print("[X] Failed to find button '" + Device + "'");
        }
	}
}
