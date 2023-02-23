
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Tools;

public class Bolt:Sprite{
    Player player;
    float despawnRange = 200;
    float targetX,targetY;
    float distance;
    float speed=500f;
    int damage;
    public Bolt(Player pPlayer) : base("Lighting_bolt.png",false,false) { 
        player= pPlayer;
        rotation = 270;
    
    }
    void Update() {
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
        float finalSpeed = speed * deltaTimeClamped / 1000;
        Move(finalSpeed, 0);
        distance = DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y);
        SetScaleXY(1.0f+distance/(despawnRange/2));
        if (distance>despawnRange) { 
            DestroyBolt();
        }
    
    }

    void DestroyBolt() {
        RealBolt bolt = new RealBolt(player);
        bolt.SetTargetXY(targetX,targetY,damage);
        parent.AddChild(bolt);

        Destroy();
    }

    public void SetTargetXY(float pTargetX, float pTargetY, int pDamage)
    {
        targetX = pTargetX;
        targetY = pTargetY;
        damage = pDamage;
    }
}
