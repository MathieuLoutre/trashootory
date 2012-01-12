var idleSound : AudioClip;
var prePos : Vector3;
var speed : float;


function Start ()
{
	audio.clip = idleSound;
	audio.Play();
	
	prePos = transform.position;
}


function Update () {

speed = (transform.position-prePos).sqrMagnitude;

Debug.Log (speed);

prePos = transform.position;

audio.pitch = 1 + 10*speed;


}


