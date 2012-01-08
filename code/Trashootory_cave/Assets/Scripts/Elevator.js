var ElevatorSound : AudioClip;
// var ElevatorLight : Light;

public function NextLevel() {
	// Change the light inside of the control room
	// ElevatorLight.light.color = Color.Red;
	
    // Create the curve
	var curve : AnimationCurve;
	curve = AnimationCurve(Keyframe(0, transform.position.y), Keyframe(3.45, transform.position.y+6), Keyframe(8.12, transform.position.y+47), Keyframe(10, transform.position.y+46));
	
    // Create the clip with the curve
    var clip : AnimationClip = new AnimationClip();
    clip.SetCurve("", Transform, "localPosition.y", curve);
    
    // Add and play the clip
    animation.AddClip(clip, "Elevator");
    animation.Play("Elevator");
	
	// Play Sound
	audio.clip = ElevatorSound;
	audio.Play();
}
@script RequireComponent(Animation)