var pellet : Rigidbody = null;
var start_pos : Vector3;
var pos_set = false;

function OnTriggerEnter (other:Collider) 
{
	if (pellet == null && other.attachedRigidbody && other.attachedRigidbody.tag == "pickup")
	{
		pellet = other.attachedRigidbody;
		other.transform.parent = null;
		pellet.useGravity = false;
	}
}

function Update () 
{
	if (pellet != null)
	{
		if (pos_set)
		{
			pellet.MovePosition(start_pos);
		}
		else
		{
			pellet.MovePosition(transform.position);
		}
		
		 if (Input.GetButtonDown("Fire1"))
		 {
		 	if (pos_set) // object is set in the air
		 	{
		 		pellet.useGravity = true;
                pellet.isKinematic = false;
		 		pellet.AddForce((start_pos - transform.position) * 1000);
		 		pellet = null;
		 		pos_set = false;
		 	}
		 	else // set the position
		 	{
		 		start_pos = transform.position;
		 		pos_set = true;
		 	}
		 }

         		
//		vrButtons buttons = null;
//		
//		if (MiddleVR.VRDeviceMgr != null) {
//		           buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0");
//		}
//		
//		if (buttons != null && buttons.IsButtonPressed(0)) 
//		{
//			if (pos_set) // object is set in the air
//			{
//				pellet.useGravity = true;
//				pellet.AddForce((start_pos - transform.position) * 1000);
//				pellet = null;
//				pos_set = false;
//			}
//			else // set the position
//			{
//				start_pos = transform.position;
//				pos_set = true;
//			}
//		}
	}
}