using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Rocket:Bullet{

    public Rocket(Player player) : base(player,"Rocket.png") {
        speed = 700f;
    }

    protected override void Destroy(bool destroy) {
        if (destroy){
            Explosion explosion = new Explosion();
            explosion.SetXY(this.x, this.y);    
            parent.AddChild(explosion);
            SoundChannel shotSoundChannel = new Sound("explosion.mp3").Play();
            shotSoundChannel.Volume = 0.3f;
            this.LateDestroy();
        }    
    }

}
