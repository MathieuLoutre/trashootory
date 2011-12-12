var ElevatorSound : AudioClip;
//var Foo : Light;

public function NextLevel() {
	// Change the light inside of the control room
	//TODO: Foo.light.color = Color.Red;
	
    // Create the curve
	var curve : AnimationCurve;
	// curve = AnimationCurve(Keyframe(0, 21), Keyframe(3.45, 27), Keyframe(8.12, 64), Keyframe(10, 63));
	curve = AnimationCurve(Keyframe(0, transform.position.y), Keyframe(3.45, transform.position.y+6), Keyframe(8.12, transform.position.y+46), Keyframe(10, transform.position.y+45));
	
    // Create the clip with the curve
    var clip : AnimationClip = new AnimationClip();
    clip.SetCurve("", Transform, "localPosition.y", curve);
    
    // Add and play the clip
    animation.AddClip(clip, "Elevator");
    animation.Play("Elevator");
	audio.clip = ElevatorSound;
	audio.Play();
}
@script RequireComponent(Animation)