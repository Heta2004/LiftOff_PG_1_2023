using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using Tools;

public class Minotaur:Enemy{

    const int CHASE = 1;
    const int DASH = 2;
    const int AOESLAM = 3;
    const int AOESPIKES = 4;
    int state = 1;
    float speedMultiplier = 2.5f;
    int dashStartTime = 0;
    int dashDuration = 900;
    float lastRotation;

    public Minotaur(Player pPlayer) :base("TankRun.png", 4, 1, pPlayer) {
        EnemySetStats(160f, 25, 200);
        lastSpeed = 160f;
        scoreOnDeath = 1000;
    }

    protected override void ChasePlayer()
    {

        switch (state)
        {
            case CHASE:
                base.ChasePlayer();
                if (DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 100)
                {
                    state = DASH;
                    dashStartTime = Time.time;
                    lastRotation = DirectionRelatedTools.CalculateAngle(x, y, player.x, player.y);
                }
                break;
            case DASH:
                Dash();
                break;
            case AOESLAM:

                break;
            case AOESPIKES:

                break;
        }






    }

    void Dash()
    {
        //speed = lastSpeed * speedMultiplier;
        speed += lastSpeed * (speedMultiplier / 2f);
        MoveEnemy(lastRotation);
        if (Time.time - dashStartTime > dashDuration){
            state = CHASE;
        }

    }

    void AOESlam() { 
    
    
    
    }

}
