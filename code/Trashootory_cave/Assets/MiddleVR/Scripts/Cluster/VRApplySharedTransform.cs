using UnityEngine;
using System.Collections;

using MiddleVR_Unity3D;

public class VRApplySharedTransform : MonoBehaviour {
    vrTracker tracker = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (MiddleVR.VRClusterMgr.IsClient())
        {
            if (tracker == null && MiddleVR.VRDeviceMgr != null)
            {
                tracker = MiddleVR.VRDeviceMgr.GetTracker(gameObject.name);
                //MiddleVRTools.Log("acquired shared tracker " + tracker.GetName());
            }

            if( tracker != null)
            {
                Destroy(rigidbody);

                vrVec3 pos = tracker.GetPosition();
                vrQuat or = tracker.GetOrientation();

                Vector3 p = new Vector3(pos.x(), pos.y(), pos.z());
                Quaternion q = new Quaternion((float)or.x(), (float)or.y(), (float)or.z(), (float)or.w());

                transform.position = p;
                transform.rotation = q;

                //MiddleVRTools.Log("Client applying data : " + p.x );
            }
        }
	}
}
