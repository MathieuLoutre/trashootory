var hand : Transform;
var objectToThrow : Transform;
var rigid : Rigidbody;

function FixedUpdate () 
{
		// var crate = Instantiate(objectToThrow, transform.position, transform.rotation);
		// crate.rigidbody.AddForce(transform.forward * 2000);
		
		if (rigid == null)
		{
			if (Input.GetButtonDown("Fire1")) 
			{			
				var hit : RaycastHit;
				if(Physics.Raycast(transform.position, transform.forward, hit, 30)) 
				{
					if(hit.rigidbody) 
					{
						rigid = hit.rigidbody;
						rigid.MovePosition(transform.position + transform.forward * 1);
					}
				}
			}	
		}
		else
		{
			rigid.MovePosition(transform.position + transform.forward * 1);
			
			if (Input.GetButtonDown("Fire1")) 
			{
				rigid.AddForce(transform.forward * 2000);
				rigid = null;
			}
		}
}