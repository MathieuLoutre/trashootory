var HitSound : AudioClip;

function OnCollisionEnter (collision : Collision) {
            
            if (collision.relativeVelocity.magnitude > 0.5) {
            audio.clip = HitSound;
            audio.Play();
            audio.volume = (collision.relativeVelocity.magnitude / 5 + 0.5);
            
            audio.pitch = (Random.value * 0.5 + 0.5);
            }

}