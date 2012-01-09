using UnityEngine;
using System.Collections;

public class catcher : MonoBehaviour 
{	
	public Rigidbody pellet = null;
	public Vector3 start_pos;
	public bool pos_set = false;
    public AudioClip slingSound;
    public AudioClip shotSound;
    public AudioClip grabSound;
	
	void OnTriggerEnter (Collider other) 
	{
		if (pellet == null && other.attachedRigidbody && other.attachedRigidbody.tag == "pickup")
		{
			pellet = other.attachedRigidbody;
			other.transform.parent = null;
			pellet.useGravity = false;
		
			audio.clip = grabSound;
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		
		if (Input.GetKeyDown(KeyCode.F))
			Screen.fullScreen = !Screen.fullScreen;
		
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

	        vrButtons buttons = null;

	        if (MiddleVR.VRDeviceMgr != null)
	        {
	            buttons = MiddleVR.VRDeviceMgr.GetButtons("VRPNButtons0");
	        }

	        if (buttons != null && buttons.IsToggled(0))
	        {
	            if (pos_set) // object is set in the air
	            {
	                pellet.useGravity = true;
	                pellet.isKinematic = false;

	                Vector3 diff = start_pos - transform.position;
	                pellet.AddForce(diff.normalized * (4000 * diff.magnitude * diff.magnitude));

	                audio.clip = shotSound;
	                float l = (start_pos - transform.position).magnitude;
	                audio.pitch = 0.7f + l / 4.0f;
	                audio.Play();

	                pellet = null;
	                pos_set = false;
	            }
	            else // set the position
	            {
	                start_pos = transform.position;
	                pos_set = true;
	                audio.clip = slingSound;
	                audio.Play();
	            }
	        }

	        //        if (buttons != null && buttons.IsToggled(0))
	        //        {
	        //            //renderer.enabled = false;
	        //        }
	        //
	        //        if (buttons != null && buttons.IsToggled(0, false))
	        //        {
	        //            //renderer.enabled = true;
	        //        }

	    }
	}

}