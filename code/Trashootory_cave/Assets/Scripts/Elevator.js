var elevator:GameObject;

function OnCollisionEnter (hit:Collision) {
	Debug.Log("Meh.");
	elevator.animation.Play("Elevator");
}