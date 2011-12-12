var winControl : Win;

function OnTriggerEnter() 
{
	transform.collider.isTrigger = false;
	transform.renderer.material.color = Color.green;
	winControl.updateGoals();
}