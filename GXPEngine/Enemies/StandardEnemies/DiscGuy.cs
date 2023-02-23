using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;
using Tools;

public class DiscGuy : StandardEnemyBase
{
    protected int shotDamage = 20;
    protected int lastShootTime = 0;
    protected int ShootCooldown = 1000;
    protected const int SHOOT = 1;
    protected const int CHASEP = 2;
    protected int state = CHASEP;

    public DiscGuy(Player pPlayer) : base("diskGuy.png", 8, 1, pPlayer)
    {
        EnemySetStats(0, 10, 50);

        lastSpeed = 0;
        scoreOnDeath = 60;
        player = pPlayer;
        RandomizeSpeed(6, 15);
        SetCycle(0, 8);
    }
    protected override void Update()
    {
        var result = TransformPoint(player.x, player.y);
        var result2 = TransformPoint(x, y);
        angle = Tools.DirectionRelatedTools.CalculateAngle(result2.x, result2.y, result.x, result.y);
        if (angle >= 90 && angle <= 270)
            Mirror(true, false);
        else
            Mirror(false, false);
        ChasePlayer();
        CheckForCooldown(); 
    }

    protected override void ChasePlayer()
    {

        if (DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 150 && Time.time > lastShootTime + ShootCooldown)
        {
            AnimateFixed(0.4f);
            if (currentFrame == 7)
            {
                Shoot();
            }
        }
        else {
            //currentFrame = 0;
        }
        

    }

    void Shoot()
    {
        var result = TransformPoint(player.x, player.y);
        var result2 = TransformPoint(x, y);
        float angle = Tools.DirectionRelatedTools.CalculateAngle(result2.x, result2.y, result.x, result.y);
        lastShootTime = Time.time;
        EnemyProjectile bullet = new EnemyProjectile(player);
        bullet.SetDamage(shotDamage);
        if (!_mirrorX)
            bullet.SetXY(x + 5, y);
        else
            bullet.SetXY(x - 5, y);
        bullet.rotation = angle;
        parent.AddChild(bullet);
        Animate(1f);
    }
}
