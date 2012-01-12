using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class VRInteractionTest : MonoBehaviour {
	void Start () {}
	
	void Update () {
        vrTracker tracker = null;
        vrMouse     mouse = null;
        vrKeyboard   keyb = null;
        vrJoystick    joy = null;
        vrAxis       axis = null;
        vrButtons buttons = null;

        if (MiddleVR.VRDeviceMgr != null) {
            tracker = MiddleVR.VRDeviceMgr.GetTracker("VRPNTracker0.0");
            mouse   = MiddleVR.VRDeviceMgr.GetMouse();
            keyb    = MiddleVR.VRDeviceMgr.GetKeyboard();
            joy     = MiddleVR.VRDeviceMgr.GetJoystick(0);
            axis    = MiddleVR.VRDeviceMgr.GetAxis("VRPNAxis0");
            buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0");
        }

        if( tracker != null )
        {
            // print("TrackerX : " + tracker.GetPosition().x() );
        }

        if (mouse != null && mouse.IsButtonPressed(0)) {
            print("Mouse Button pressed!");
            print("VRMouseX : " + mouse.GetAxisValue(0));
        }

        if (keyb != null && keyb.IsKeyPressed(MiddleVR.VRK_SPACE)) {
            print("Space!");
        }

        if (joy != null && joy.IsButtonPressed(0)) {
            print("Joystick!");
        }

        if( axis != null && axis.GetValue(0) > 0 )
        {
            print("Axis Value: " + axis.GetValue(0));
        }

        if (buttons != null)
        {
            if (buttons.IsToggled(0))
            {
                print("Button 0 pressed !");
            }

            if (buttons.IsToggled(0, false))
            {
                print("Button 0 released !");
            }
        }
    }
}
