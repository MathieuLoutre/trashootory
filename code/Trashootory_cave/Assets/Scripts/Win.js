function OnTriggerEnter (hit : Collider) 
{
	if (hit.attachedRigidbody && (hit.attachedRigidbody.tag == "pickup" || hit.attachedRigidbody.tag == "scene"))
	{
		yield WaitForSeconds(5.0);
		Application.LoadLevel("Level2");
	}
}