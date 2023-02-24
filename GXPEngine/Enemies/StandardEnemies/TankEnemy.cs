using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using Tools;

public class TankEnemy : StandardEnemyBase
{
    const int CHASE = 1;
    const int DASH = 2;
    int state = 1;
    float speedMultiplier = 2.75f;//2,5f
    //int dashStartTime = 0;
    //int dashDuration = 900;
    int dashEndTime = 0;
    float lastRotation;
    int dashCooldown = 3000;

    public TankEnemy(Player pPlayer) : base("hound.png", 13, 1, pPlayer) {
        EnemySetStats(80f, 25, 200);
        lastSpeed = 80f;
        scoreOnDeath = 40;
        RandomizeSpeed(3, 7);
        SetScaleXY(1.5f);
    }

    protected override void Update(){
        //speed = lastSpeed;
        if (angle >= 90 && angle <= 270)
            Mirror(true, false);
        else
            Mirror(false, false);
        if (gameData != null){
            if (gameData.gameState == gameData.NIGHT)
                basicState = NIGHT;
        }
        switch (basicState)
        {
            case DAY:
                WalkAround();
                break;
            case NIGHT:
                ChasePlayer();
                break;

        }
        CheckForCooldown();
        speed = lastSpeed;


    }

    protected override void ChasePlayer()
    {

        switch (state) {
            case CHASE:
                SetCycle(0,8);
                AnimateFixed(0.65f);
                changeDirection = true;
                base.ChasePlayer();
                if (DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 100&&(Time.time-dashEndTime>dashCooldown)){
                    state = DASH;
                    //dashStartTime = Time.time;
                    //(Time.time - (dashStartTime + dashDuration) > dashCooldown)
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
        SetCycle(9,5);
        AnimateFixed(0.3f);
        changeDirection = false;
        speed += lastSpeed * (speedMultiplier/2f);
        angle= DirectionRelatedTools.CalculateAngle(x, y, player.x, player.y);
        MoveEnemy(lastRotation);
        if (currentFrame == 12) { 
            dashEndTime= Time.time;
            state=CHASE;
        }
        //if (Time.time-dashStartTime> dashDuration)
        //{
        //    state = CHASE;
        //}

    }

}

