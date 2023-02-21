using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;
using Tools;

public class Shooter: StandardEnemyBase
{
    protected int shotDamage = 15;
    protected int lastShootTime = 0;
    protected int ShootCooldown = 500;

    public Shooter(Player pPlayer) : base("Shooter.png", 4,1, pPlayer) {
        EnemySetStats(80f, 10, 50);//165
        
        lastSpeed = 80f;
        scoreOnDeath = 60;
        player= pPlayer;
        RandomizeSpeed(6, 15);
    }

    //protected override void MoveEnemy(float angle){
    //    rotation = angle;
    //    if (Tools.DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 200)
    //    {
    //        rotation = 0;
    //        Shoot();
    //    }
    //    else {

    //        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
    //        float finalSpeed = speed * deltaTimeClamped / 1000;
    //        Move(finalSpeed, 0);

    //    }
    //    rotation = 0;
    //}
    protected override void ChasePlayer()
    {
        base.ChasePlayer();
        if (DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 150)
            Shoot();
    }

    void Shoot() {
        var result = TransformPoint(player.x, player.y);
        var result2 = TransformPoint(x, y);
        float angle = Tools.DirectionRelatedTools.CalculateAngle(result2.x, result2.y, result.x, result.y);
        if (Time.time > lastShootTime + ShootCooldown){
            lastShootTime = Time.time;
            EnemyProjectile bullet = new EnemyProjectile(player);
            bullet.SetDamage(damage);
            bullet.SetXY(x, y);
            bullet.rotation = angle;
            parent.AddChild(bullet);
        }
    }
}
