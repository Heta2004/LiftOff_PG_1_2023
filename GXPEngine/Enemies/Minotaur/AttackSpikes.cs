using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class AttackSpikes : AnimationSprite
{
    int startTime;
    int duration = 500;
    float speed = 275f;
    public AttackSpikes() : base("AttackSpikes.png", 2,1,2,false,true)
    {
        collider.isTrigger = true;
        startTime = Time.time;

        //if (rotation >= 90 && rotation <= 270)
        //    Mirror(false, false);
        //else
        //    Mirror(false, true);
    }

    void Update(){
        Animate(0.5f);
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
        float finalSpeed = speed * deltaTimeClamped / 1000;
        Move(finalSpeed,0);

        if (Time.time - startTime > duration){
            Destroy();
        }
    }
}
