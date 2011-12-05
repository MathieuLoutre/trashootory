using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

public class VRManagerScript : MonoBehaviour
{
    public string ConfigFile;
    public GameObject RootNode = null;
    public bool DisableExistingCameras = true;
    public bool GrabExistingNodes = false;
    public bool DebugNodes = false;
    public bool DebugScreens = false;
    public bool QuitOnEsc = true;

    private bool isInit = false;
    private bool isGeometrySet = false;

    private bool DisplayLog = false;

    private GUIText m_GUI = null;

    private vrKernel             kernel = null;
    //private vrDeviceManager   deviceMgr = null;
    private vrDisplayManager displayMgr = null;


    void DumpOptions()
    {
        MiddleVRTools.Log(3,"[ ] Dumping VRManager's options:");
        MiddleVRTools.Log(3,"[ ] - ConfigFile : " + ConfigFile);
        MiddleVRTools.Log(3,"[ ] - RootNode : " + RootNode);
        MiddleVRTools.Log(3,"[ ] - DisableExistingCameras : " + DisableExistingCameras );
        MiddleVRTools.Log(3,"[ ] - GrabExistingNodes : " + GrabExistingNodes );
        MiddleVRTools.Log(3,"[ ] - DebugNodes : " + DebugNodes );
        MiddleVRTools.Log(3,"[ ] - DebugScreens : " + DebugScreens );
        MiddleVRTools.Log(3,"[ ] - QuitOnEsc : " + QuitOnEsc );

        // XXX Dump VSync ? RunInBackground ... ?

    }

    void OnApplicationQuit()
    {
        MiddleVRTools.Log(3,"[>] Resetting MiddleVR.");
        MiddleVR.VRDestroy();
        MiddleVRTools.Log(3,"[<] MiddleVR reset.");
    }

    void InitializeVR()
    {
        if (DisplayLog)
        {
            GameObject gui = new GameObject();
            m_GUI = gui.AddComponent("GUIText") as GUIText;
            gui.transform.localPosition = new UnityEngine.Vector3(0.5f, 0.0f, 0.0f);
            m_GUI.pixelOffset = new UnityEngine.Vector2(15, 0);
            m_GUI.anchor = TextAnchor.LowerCenter;
        }

        if( ConfigFile.Length > 0 )
            isInit = MiddleVRTools.VRInitialize(ConfigFile);

        DumpOptions();

        // XXX LOG FAQ if MVR !in Path or just restart Unity?

        //string txt = MiddleVR.VRKernel.GetLogString(true);
        //Debug.Log(txt);

        /*
        if (DisplayLog)
        {
            m_GUI.text = txt;
        }*/

        if (!isInit)
            return;

        kernel     = MiddleVR.VRKernel;
        displayMgr = MiddleVR.VRDisplayMgr;
        //deviceMgr  = MiddleVR.VRDeviceMgr;

        //uint nbDevices = deviceMgr.GetDevicesNb();

        if (DisableExistingCameras)
        {
            Camera[] cameras = GameObject.FindObjectsOfType(typeof(Camera)) as Camera[];

            foreach (Camera cam in cameras)
            {
                if( cam.targetTexture == null )
                    cam.enabled = false;
            }
        }

        MiddleVRTools.CreateNodes(RootNode, DebugNodes, DebugScreens, GrabExistingNodes);
        MiddleVRTools.CreateViewportsAndCameras();

        MiddleVRTools.Log(4, "[<] End of VR initialization script");
    }

	void Start () {
        MiddleVRTools.Log(4,"[>] VR Manager Start.");
        InitializeVR();
        MiddleVRTools.Log(4, "[<] End of VR Manager Start.");
	}
	
	// Update is called once per frame
	void Update () {
        //MiddleVRTools.Log("VRManagerUpdate");
        MiddleVRTools.Log(4, "[>] Unity Update - Start");
            
	    if( isInit )
        {
            if( kernel.GetFrame() >= 1 && !isGeometrySet && !Application.isEditor)
            {
                displayMgr.SetWindowGeometry();
                isGeometrySet = true;
            }

            kernel.Update();

            MiddleVRTools.UpdateNodes();

            if (DisplayLog)
            {
                string txt = kernel.GetLogString(true);
                print(txt);
                m_GUI.text = txt;
            }
        }

        MiddleVRTools.Log(4, "[<] Unity Update - End");
	}
}
