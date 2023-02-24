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
    protected const int SHOOT = 1;
    protected const int CHASEP = 2;
    protected int state=CHASEP;

    public Shooter(Player pPlayer) : base("Purple_snake.png", 9,1, pPlayer) {
        EnemySetStats(100f, 10, 100);//165
        
        lastSpeed = 100f;
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

        switch (state) {
            case SHOOT:
                SetCycle(4, 5);
                AnimateFixed(0.2f);
                if (currentFrame == 8)
                {
                    Shoot();
                    state = CHASEP;
                }
                break;
            case CHASEP:
                SetCycle(0, 4);
                base.ChasePlayer();
                if (DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 150)
                    state = SHOOT;
                break;

        
        }
        //base.ChasePlayer();
        //if (DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 150)
        //    Shoot();
    }

    void Shoot() {
        var result = TransformPoint(player.x, player.y);
        var result2 = TransformPoint(x, y);
        float angle = Tools.DirectionRelatedTools.CalculateAngle(result2.x, result2.y, result.x, result.y);
        if (Time.time > lastShootTime + ShootCooldown){
            lastShootTime = Time.time;
            EnemyProjectile bullet = new EnemyProjectile(player);
            bullet.SetDamage(shotDamage);
            bullet.SetXY(x, y);
            bullet.rotation = angle;
            parent.AddChild(bullet);
        }
    }
}
