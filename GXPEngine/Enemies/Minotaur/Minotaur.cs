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
    const int WAIT = 5;
    int state = 1;
    float speedMultiplier = 2.5f;

    int dashStartTime = 0;
    int dashDuration = 1200;
    float lastRotation;
    int dashCooldown=3000;
    int dashRange = 300;

    int slamCooldown = 3500;
    int slamRange = 75;
    int slamStartTime = 0;

    int spikesCooldown = 3500;
    int spikesRange = 150;//225
    int spikesStartTime = 0;

    int waitTime = 500;
    int waitStartTime = 0;

    public Minotaur(Player pPlayer,GameData pGameData) :base("TankRun.png", 4, 1, pPlayer) {
        gameData = pGameData;
        EnemySetStats(160f, 25, 1000);
        lastSpeed = 160f;
        scoreOnDeath = 1000;
    }

    protected override void ChasePlayer()
    {
        switch (state)
        {
            case CHASE:
                changeDirection = true;
                base.ChasePlayer();
                ChooseTargeState();
                break;
            case DASH:
                Dash();
                //state = CHASE;
                break;
            case AOESLAM:
                AOESlam();
                break;
            case AOESPIKES:
                AOESpikes();
                break;
            case WAIT:
                Wait();
                break;
        }

    }

    void ChooseTargeState() {
        lastRotation = DirectionRelatedTools.CalculateAngle(x, y, player.x, player.y);
        float distance = DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y);
        if (distance < slamRange && (Time.time - slamStartTime > slamCooldown))
        {
            state = AOESLAM;
            slamStartTime = Time.time;
            return;
        }
        if (distance < spikesRange && (Time.time - spikesStartTime > spikesCooldown))
        {
            state = AOESPIKES;
            spikesStartTime = Time.time;
            return;
        }
        if (distance < dashRange && (Time.time - (dashStartTime + dashDuration) > dashCooldown))
        {
            state = DASH;
            dashStartTime = Time.time;
            return;
        }

    }

    void Dash(){
        changeDirection = false;
        speed += lastSpeed * (speedMultiplier / 2f);
        MoveEnemy(lastRotation);
        if (Time.time - dashStartTime > dashDuration){
            state = WAIT;
            waitStartTime= Time.time;
        }

    }

    void AOESlam() { 
        
        AoeSlam slam=new AoeSlam();
        slam.SetOrigin(slam.width / 2, slam.height / 2);
        parent.AddChild(slam);
        slam.SetXY(x, y);
        waitStartTime = Time.time;
        state = WAIT;
    
    }
    void AOESpikes() { 
        AoeSpikes spike1=new AoeSpikes();
        spike1.SetOrigin(0, spike1.height / 2);
        parent.AddChild(spike1);
        spike1.SetXY(x, y);
        spike1.rotation = lastRotation;


        AoeSpikes spike2 = new AoeSpikes();
        spike2.SetOrigin(0, spike2.height / 2);
        parent.AddChild(spike2);
        spike2.SetXY(x, y);
        spike2.rotation = lastRotation-15;


        AoeSpikes spike3 = new AoeSpikes();
        spike3.SetOrigin(0, spike3.height / 2);
        parent.AddChild(spike3);
        spike3.SetXY(x, y);
        spike3.rotation = lastRotation + 15;

        waitStartTime = Time.time;
        state = WAIT;
    }

    void Wait() {
        if (Time.time - waitStartTime > waitTime) {
            state = CHASE;
        }
    }
    protected override void MoveEnemy(float angle){
        rotation = angle;
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
        float finalSpeed = speed * deltaTimeClamped / 1000;
        float lastX = x;
        float lastY = y;
        Move(finalSpeed, 0);

        GameObject[] overlaps = GetCollisions(false, true);
        foreach (GameObject collision in overlaps) {
            if (collision is Wall) {
                SetXY(lastX,lastY);
                break;
            }
        }

        rotation = 0;
    }

}
