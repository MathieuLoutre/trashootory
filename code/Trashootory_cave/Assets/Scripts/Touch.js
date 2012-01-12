var winControl : Win;
var hitsound : AudioClip;

function OnTriggerEnter() 
{
	transform.collider.isTrigger = false;
	transform.renderer.material.color = Color.green;
    audio.clip = hitsound;
    audio.Play();
	winControl.updateGoals();
}