using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using Tools;

public class BidentFire:Bullet{
    int collisionsLeft = 3;
    GameObject lastCollision = null;
    float spawnX;
    float spawnY;
    float range = 200;
    public BidentFire(Player pPlayer):base(pPlayer, "Fire.png",3,1,3) {
        speed = 300f;
        spawnX = x;
        spawnY = y;
    }
    protected override void Update(){
        Console.WriteLine(spawnX+" "+spawnY);
        AnimateFixed(0.5f);
        base.Update(); 
        if (DirectionRelatedTools.CalculateDistance(x,y,spawnX,spawnY)>range)
            Destroy();
    }

    protected override void HitTarget()
    {

        GameObject[] collisions = GetCollisions();
        bool destroy = false;

        foreach (GameObject col in collisions)
        {
            if (col is Enemy && lastCollision != col)
            {
                lastCollision = col;
                ((Enemy)col).DamageEnemy(damage);
                collisionsLeft--;
                if (collisionsLeft == 0)
                    destroy = true;
                break;
            }
            if (col is BreakableVase)
            {
                ((BreakableVase)col).StartAnimation();
                collisionsLeft--;
                if (collisionsLeft == 0)
                    destroy = true;
            }

            if (col is Wall || col is DestructibleWall)
                destroy = true;
        }

        Destroy(destroy);

    }

    public void SetSpawnXY(float pSpawnX,float pSpawnY) {
        spawnX = pSpawnX;
        spawnY = pSpawnY;   
    }
}
