var world : World;
var thrown = false;

function Update()
{
	if (thrown == false)
	{
		if (transform.rigidbody.isKinematic == false)
		{
			thrown = true;
			world.PelletThrown();
		}
	}
}