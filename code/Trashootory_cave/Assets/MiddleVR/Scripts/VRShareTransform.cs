//using UnityEngine;
//using System.Collections;
//
//using MiddleVR_Unity3D;
//
//public class VRShareTransform : MonoBehaviour {
//    vrTracker tracker = null;
//
//	// Use this for initialization
//	void Start () {
//	}
//	
//	// Update is called once per frame
//	void Update () {
//        if( tracker == null && MiddleVR.VRDeviceMgr != null )
//        {
//            tracker = MiddleVR.VRDeviceMgr.CreateTracker(gameObject.name);
//            MiddleVRTools.Log("Created shared tracker " + tracker.GetName() );
//        }
//
//        if( MiddleVR.VRClusterMgr.IsServer() )
//        {
//            Vector3 p = transform.position;
//            Quaternion q = transform.rotation;
//
//            vrVec3 pos = new vrVec3(p.x, p.y, p.z);
//            vrQuat or = new vrQuat(q.x, q.y, q.z, q.w);
//
//            tracker.SetPosition(pos);
//            tracker.SetOrientation(or);
//
//            //MiddleVRTools.Log("Server pushing data : " + p.x );
//        }
//	}
//}
