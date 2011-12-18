using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class VRManagerPostFrame : MonoBehaviour {
    public vrKernel kernel = null;
    public vrDeviceManager dmgr = null;

    private bool LoggedNoKeyboard = false;

    IEnumerator EndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        MiddleVRTools.Log(4, "[>] VR End of Frame.");
        
        if( kernel == null )
        {
            kernel = MiddleVR.VRKernel;
        }

        if (kernel != null)
        {
            kernel.PostFrameUpdate();
        }

        if( dmgr == null )
        {
            dmgr = MiddleVR.VRDeviceMgr;
        }

        if (dmgr != null)
        {
            vrKeyboard keyb = dmgr.GetKeyboard();
            if (keyb != null)
            {
                VRManagerScript vrmgr = GetComponent<VRManagerScript>();

                if( vrmgr != null && vrmgr.QuitOnEsc && keyb.IsKeyToggled((uint)MiddleVR.VRK_ESCAPE))
                {
                    if (Application.isEditor)
                    {
						MiddleVRTools.Log("If we were in player mode, MiddleVR would exit.");
                    }
                    else
                    {
                        Application.Quit();
                    }

                }
            }
            else
            {
                if( !LoggedNoKeyboard)
                {
                    MiddleVRTools.Log("[X] No VR keyboard");
                    LoggedNoKeyboard = true;
                }
                
            }
        }


        MiddleVRTools.Log(4, "[<] End of VR End of Frame.");

        if( kernel.GetFrame() == 2 && !Application.isEditor )
        {
            MiddleVRTools.Log(3, "[ ] If the application is stuck here and you're using Quad-buffer active stereoscopy, make sure that in the Player Settings of Unity, the option 'Run in Background' is checked.");
        }
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //MiddleVRTools.Log(4, "[>] VR PostFrame Update!");
        StartCoroutine(EndOfFrame());

        /*
        if (kernel == null)
        {
            kernel = MiddleVR.VRKernel;
        }

        if (kernel != null)
        {
            kernel.PostFrameUpdate();
        }*/

        //MiddleVRTools.Log(4, "[<] End of VR PostFrame Update!");
	}
}
