using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class AoeSlam:Sprite{
    int startTime;
    int duration = 200;
    public AoeSlam() : base("AOEShockWave.png", true) { 
        collider.isTrigger= true;
        startTime = Time.time;
    }

    void Update() { 
        if (Time.time-startTime > duration) { 
            Destroy();
        }
    }
}
