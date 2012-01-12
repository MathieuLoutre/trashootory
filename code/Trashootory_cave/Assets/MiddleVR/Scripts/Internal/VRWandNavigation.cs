using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

[RequireComponent (typeof(VRWandInteraction))]
public class VRWandNavigation : MonoBehaviour {
    public string NodeToMove = "CenterNode";
    public string ReferenceNode = "HandNode";

    public bool Fly = true;
    private vrAxis m_Axis = null;

    private bool m_SearchedAxis = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        GameObject refNode = null;
        GameObject node = null;

        refNode = GameObject.Find(ReferenceNode);
        node = GameObject.Find(NodeToMove);

        if (refNode != null && node != null )
        {
            if (m_Axis == null)
            {
                string axisName = MiddleVR.VRDeviceMgr.GetWandAxisName();

                m_Axis = MiddleVRTools.GetAxis(axisName);
            }

            if (m_Axis != null )
            {
                uint ForwardAxis = MiddleVR.VRDeviceMgr.GetWandAxis1();
                uint RotationAxis = MiddleVR.VRDeviceMgr.GetWandAxis0();

                float speed  = m_Axis.GetValue(ForwardAxis) * Time.deltaTime;
                float speedR = m_Axis.GetValue(RotationAxis) * Time.deltaTime * 50;

                Vector3 translationVector = new Vector3(0, 0, 1);
                Vector3 tVec = /*refNode.transform.worldToLocalMatrix **/ translationVector;
                Vector3 nVec = new Vector3(tVec.x * speed, tVec.y * speed, tVec.z * speed );

                if (Fly == false)
                {
                    nVec.y = 0.0f;
                }

                node.transform.Translate(nVec,refNode.transform);
                node.transform.Rotate(new Vector3(0, 1, 0), speedR);
            }
            else
            {
                if (m_SearchedAxis == false)
                {
                    print("[X] Failed to find axis '" + MiddleVR.VRDeviceMgr.GetWandAxisName() + "'");
                    m_SearchedAxis = true;
                }
            }
        }
    }
}
