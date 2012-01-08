var pellet : Rigidbody = null;
var start_pos : Vector3;
var pos_set = false;
var slingSound : AudioClip;
var shotSound : AudioClip;
var grabSound : AudioClip;

function OnTriggerEnter (other:Collider) 
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

function Update () 
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
		
		 if (Input.GetButtonDown("Fire1"))
		 {
		 	if (pos_set) // object is set in the air
		 	{
		 		pellet.useGravity = true;
                pellet.isKinematic = false;

				var diff = start_pos - transform.position;
		 		pellet.AddForce(diff.normalized * (4000 * diff.magnitude*diff.magnitude));
				
				audio.clip = shotSound;
				var l = (start_pos - transform.position).magnitude;
				audio.pitch = 0.7 + l/4;
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
	}
}