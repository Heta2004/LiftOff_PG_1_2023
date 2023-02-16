using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class ParticleGunSmoke:Particle{

    public ParticleGunSmoke():base("MuzzleFlash.png", BlendMode.NORMAL,100,3,1,3) { 
    
    
    }

    protected override void Update(){
        AnimateFixed(0.5f);
        base.Update();
    }
}
