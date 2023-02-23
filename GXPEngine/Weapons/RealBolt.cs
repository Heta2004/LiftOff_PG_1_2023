using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Tools;

public class RealBolt:Bullet{
    float targetX;
    float targetY;
    float initialDistance;
    
    public RealBolt(Player pPlayer) : base(pPlayer, "Lighting_bolt.png", 1, 1, 1) {
        speed = 0f;
        rotation = 90;
        SetScaleXY(3.0f, 3.0f);
    }
    protected override void Update()
    {
        base.Update();
        Console.WriteLine(targetX+" "+targetY);
    }
    protected override void HitTarget(){
        SetScaleXY(3.0f - (initialDistance-DirectionRelatedTools.CalculateDistance(x, y, targetX, targetY)) / (initialDistance/2));

        if (y >= targetY-width/2)
            DestroyBolt();

    }

    protected void DestroyBolt(){

        Explosion explosion = new Explosion();
        parent.AddChild(explosion);
        explosion.SetDamage(damage);
        explosion.SetXY(x, y);
        Destroy();
    }

    public void SetTargetXY(float pTargetX,float pTargetY,int pDamage) { 
        targetX=pTargetX;
        targetY=pTargetY;
        speed = 850f;//550
        initialDistance = Math.Abs(player.y - 200-targetY); 
        speed = (speed/200)*(initialDistance);
        x = targetX;
        y = player.y - 200;
        Console.WriteLine(initialDistance+" "+x+" "+y);
        damage = pDamage;
    }
}
