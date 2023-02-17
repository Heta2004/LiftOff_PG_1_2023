using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using Tools;

public class TankEnemy : Enemy
{
    const int CHASE = 1;
    const int DASH = 2;
    int state = 1;
    float speedMultiplier = 2.5f;
    int dashStartTime = 0;
    int dashDuration = 900;
    float lastRotation;

    public TankEnemy(Player pPlayer) : base("TankRun.png",4,1, pPlayer){
        EnemySetStats(85f, 25, 200);
        lastSpeed = 85f;
        scoreOnDeath = 40;
    }

    protected override void ChasePlayer()
    {

        switch (state) {
            case CHASE:
                base.ChasePlayer();
                if (DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 100){
                    state = DASH;
                    dashStartTime = Time.time;
                    lastRotation = DirectionRelatedTools.CalculateAngle(x,y,player.x,player.y);
                }
                break;
            case DASH:
                Dash();
                break;
        }

        




    }

    void Dash() {
        //speed = lastSpeed * speedMultiplier;
        speed += lastSpeed * (speedMultiplier/2f);
        MoveEnemy(lastRotation);
        if (Time.time-dashStartTime> dashDuration)
        {
            state = CHASE;
        }

    }

}

