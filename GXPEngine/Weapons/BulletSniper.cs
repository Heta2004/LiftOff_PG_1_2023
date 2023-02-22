using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class BulletSniper:Bullet{
    int collisionsLeft = 3;
    GameObject lastCollision=null;

    public BulletSniper(Player player) :base(player, "spear.png",1,1,1)
    {
        speed = 700f;

    }

    protected override void HitTarget()
    {

        GameObject[] collisions = GetCollisions();
        bool destroy = false;

        foreach (GameObject col in collisions)
        {
            if (col is Enemy&&lastCollision!=col){
                lastCollision = col;
                ((Enemy)col).DamageEnemy(damage);
                collisionsLeft--;
                if (collisionsLeft == 0)
                    destroy = true;
                break;
            }

            if (col is Wall||col is DestructibleWall)
                destroy = true;
        }

        Destroy(destroy);

    }

}
