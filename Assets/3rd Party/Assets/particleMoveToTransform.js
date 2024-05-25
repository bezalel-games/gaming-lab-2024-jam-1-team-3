var target : Transform;

var theSpeed = 1.0;

function Update () 
{	
    
    // extract the particles
    var theParticles = particleEmitter.particles;
    
    
    for (var i=0; i<theParticles.Length; i++) 
    {        
        theParticles[i].position = Vector3.Lerp (theParticles[i].position, target.position, Time.deltaTime * theSpeed);
    }
    
    // copy them back to the particle system
    particleEmitter.particles = theParticles;
}
