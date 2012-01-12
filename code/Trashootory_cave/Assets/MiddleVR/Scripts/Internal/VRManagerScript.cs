using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System;

public class VRManagerScript : MonoBehaviour
{
    public string ConfigFile;
    public GameObject RootNode = null;
    
    public bool ShowWand = true;
    public bool ShowFPS = true;
    public bool DisableExistingCameras = true;
    public bool GrabExistingNodes = false;
    public bool DebugNodes = false;
    public bool DebugScreens = false;
    public bool QuitOnEsc = true;

    [HideInInspector] public bool isInit = false;
    private bool isGeometrySet = false;

    private bool DisplayLog = false;

    private GUIText m_GUI = null;
    private GameObject m_Wand = null;

    public  vrKernel             kernel = null;
    private vrDisplayManager displayMgr = null;


    void DumpOptions()
    {
        MiddleVRTools.Log(3,"[ ] Dumping VRManager's options:");
        MiddleVRTools.Log(3,"[ ] - ConfigFile : " + ConfigFile);
        MiddleVRTools.Log(3,"[ ] - RootNode : " + RootNode);
        MiddleVRTools.Log(3,"[ ] - ShowWand : " + ShowWand);
        MiddleVRTools.Log(3,"[ ] - ShowFPS  : " + ShowFPS);
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

        isInit = MiddleVRTools.VRInitialize(ConfigFile);

        DumpOptions();

        kernel = MiddleVR.VRKernel;
        displayMgr = MiddleVR.VRDisplayMgr;

        if (!isInit)
        {
            GameObject gui = new GameObject();
            m_GUI = gui.AddComponent("GUIText") as GUIText;
            gui.transform.localPosition = new UnityEngine.Vector3(0.5f, 0.0f, 0.0f);
            m_GUI.pixelOffset = new UnityEngine.Vector2(0, 0);
            m_GUI.anchor = TextAnchor.LowerLeft;

            string txt = kernel.GetLogString(true);
            print(txt);
            m_GUI.text = txt;

            return;
        }

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

        m_Wand = GameObject.Find("VRWand");

        if (ShowFPS)
        {
            guiText.enabled = true;
        }
        else
        {
            guiText.enabled = false;
        }

        if (ShowWand)
        {
            EnableWand(true);
        }
        else
        {
            EnableWand(false);
        }
	}

    void EnableWand(bool iState)
    {
        if( m_Wand != null )
        {
            m_Wand.SetActiveRecursively(iState);
        }
        else
        {
            print("!W");
        }
    }
	
	// Update is called once per frame
	void Update () {
        //MiddleVRTools.Log("VRManagerUpdate");

        if (isInit)
        {
            MiddleVRTools.Log(4, "[>] Unity Update - Start");

            if (kernel.GetFrame() >= 1 && !isGeometrySet && !Application.isEditor)
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

            vrKeyboard keyb = MiddleVR.VRDeviceMgr.GetKeyboard();

            if (keyb.IsKeyToggled(MiddleVR.VRK_F) && (keyb.IsKeyPressed(MiddleVR.VRK_LSHIFT) || keyb.IsKeyPressed(MiddleVR.VRK_RSHIFT)))
            {
                ShowFPS = !ShowFPS;
                guiText.enabled = ShowFPS;
            }

            if ((keyb.IsKeyToggled(MiddleVR.VRK_W) || keyb.IsKeyToggled(MiddleVR.VRK_Z)) && (keyb.IsKeyPressed(MiddleVR.VRK_LSHIFT) || keyb.IsKeyPressed(MiddleVR.VRK_RSHIFT)))
            {
                ShowWand = !ShowWand;
                EnableWand(ShowWand);
            }

            MiddleVRTools.Log(4, "[<] Unity Update - End");
        }
        else
        {
            //Debug.LogWarning("[ ] If you have an error mentionning 'DLLNotFoundException: MiddleVR_CSharp', please restart Unity. If this does not fix the problem, please make sure MiddleVR is in the PATH environment variable.");
        }
	}
}
