using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

internal class EnemyProjectile : Bullet
{
    public EnemyProjectile(Player pPlayer) : base(pPlayer, "enemyProjectile.png")
    {
        speed = 600f;
        damage = 10;
        player = pPlayer;
    }

    protected override void HitTarget() {
        GameObject[] collisions = GetCollisions();
        bool destroy = false;

        foreach (GameObject col in collisions)
        {
            if (col is Player)
            {
                ((Player)col).TakeDamage(damage);
                this.LateDestroy();
            }
            if (!(col is Enemy)&&!(col is Bullet) && !(col is EnemyProjectile))
                destroy = true;
        }

        Destroy(destroy);

    }



}

