using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class AoeSlam:AnimationSprite{
    int startTime;
    int duration = 500;
    public AoeSlam() : base("AOEShockWave.png",2,1,2,false,true) { 
        collider.isTrigger= true;
        startTime = Time.time;
        Tween tween = new Tween(TweenProperty.rotation,500,1000,2);
        AddChild(tween);

    }

    void Update() {
        AnimateFixed(0.75f);
        if (Time.time-startTime > duration) { 
            Destroy();
        }
    }
}
