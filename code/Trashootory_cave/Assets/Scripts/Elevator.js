
public function NextLevel() {
    // Create the curve
	var curve : AnimationCurve;
	// curve = AnimationCurve(Keyframe(0, 21), Keyframe(3.45, 27), Keyframe(8.12, 64), Keyframe(10, 63));
	curve = AnimationCurve(Keyframe(0, transform.position.y), Keyframe(3.45, transform.position.y+6), Keyframe(8.12, transform.position.y+43), Keyframe(10, transform.position.y+42));
	
    // Create the clip with the curve
    var clip : AnimationClip = new AnimationClip();
    clip.SetCurve("", Transform, "localPosition.y", curve);
    
    // Add and play the clip
    animation.AddClip(clip, "Elevator");
    animation.Play("Elevator");
}
@script RequireComponent(Animation)