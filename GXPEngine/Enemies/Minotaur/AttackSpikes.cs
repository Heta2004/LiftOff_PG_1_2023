using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class AttackSpikes : AnimationSprite
{
    int startTime;
    int duration = 750;//500
    float speed = 300f;//275
    int id;
    public AttackSpikes(int pId) : base("AttackSpikes.png", 2,1,2,false,true)
    {
        id = pId;
        collider.isTrigger = true;
        startTime = Time.time;

        //if (rotation >= 90 && rotation <= 270)
        //    Mirror(false, false);
        //else
        //    Mirror(false, true);
    }

    void Update(){
        Console.WriteLine(id+" "+"({0},{1})", x, y);
        Animate(0.5f);
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
        float finalSpeed = speed * deltaTimeClamped / 1000;
        Move(finalSpeed,0);

        if (Time.time - startTime > duration){
            Destroy();
        }

        HitTarget();
    }

    protected void HitTarget()
    {
        GameObject[] collisions = GetCollisions();

        foreach (GameObject col in collisions)
        {
            if (col is DestructibleWall) {
                col.Destroy();
                this.Destroy();
            }

        }

    }
}
