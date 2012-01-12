var world : World;
var thrown = false;

function Update()
{
	if (thrown == false)
	{
		if (transform.gameObject.tag == "shot")
		{
			thrown = true;
			world.PelletThrown();
		}
	}
}