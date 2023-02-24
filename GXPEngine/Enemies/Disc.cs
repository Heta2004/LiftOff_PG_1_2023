using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class Disc : Bullet
{
    public Disc(Player pPlayer) : base(pPlayer, "disk.png", 1, 1, 1)
    {
        speed = 600f;
        //damage = 10;
        player = pPlayer;
    }
    protected override void Update()
    {
        SetCycle(0, 5);
        AnimateFixed(0.4f);
        base.Update();
    }
    protected override void HitTarget()
    {
        GameObject[] collisions = GetCollisions();
        bool destroy = false;

        foreach (GameObject col in collisions)
        {
            if (col is Player)
            {
                ((Player)col).TakeDamage(damage);
                this.LateDestroy();
            }
            if (col is Wall)
                destroy = true;
        }

        Destroy(destroy);

    }



}

