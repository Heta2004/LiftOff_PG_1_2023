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
    int dashCooldown=4000;//3000
    int dashRange = 300;

    int slamCooldown = 3500;
    int slamRange = 75;//100
    int slamStartTime = 0;

    int spikesCooldown = 3500;
    int spikesRange = 175;//150
    int spikesStartTime = 0;

    int waitTime = 2000;//500
    int waitStartTime = 0;


    public Minotaur(Player pPlayer,GameData pGameData) :base("Minotaur.png", 12, 1, pPlayer) {
        gameData = pGameData;
        
        SetScaleXY(2f,2f);
        EnemySetStats(160f, 25, 1000);
        lastSpeed = 160f;
        scoreOnDeath = 1000;
    }

    protected override void Update()
    {
        speed = lastSpeed;
        ChasePlayer();
        base.Update();
    }

    protected override void ChasePlayer()
    {

        if (state != WAIT&&state!=DASH) {
            float angle = Tools.DirectionRelatedTools.CalculateAngle(x, y, player.x, player.y);
            if (angle >= 90 && angle <= 270)
                Mirror(true, false);
            else
                Mirror(false, false);
        }
        switch (state)
        {
            case CHASE:
                SetCycle(0,4);
                AnimateFixed(0.65f);
                changeDirection = true;
                base.ChasePlayer();
                ChooseTargetState();
                break;
            case DASH:
                Dash();
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

    void ChooseTargetState() {
        lastRotation = DirectionRelatedTools.CalculateAngle(x, y, player.x, player.y);
        float distance = DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y);
        if (distance < slamRange && (Time.time - slamStartTime > slamCooldown))
        {
            state = AOESLAM;
            slamStartTime = Time.time;
            return;
        }
        if (distance < spikesRange && (Time.time - spikesStartTime > spikesCooldown)&&(distance>slamRange))
        {
            state = AOESPIKES;
            spikesStartTime = Time.time;
            return;
        }
        if (distance < dashRange && (Time.time - (dashStartTime + dashDuration) > dashCooldown)&&(distance>spikesRange))
        {
            state = DASH;
            dashStartTime = Time.time;
            return;
        }

    }

    void Dash(){
        SetCycle(0, 3);
        AnimateFixed(1f);
        changeDirection = false;
        speed += lastSpeed * (speedMultiplier / 2f);
        MoveEnemy(lastRotation);
        if (Time.time - dashStartTime > dashDuration){
            state = WAIT;
            waitStartTime= Time.time;
        }

    }

    void AOESlam() {
        SetCycle(8, 4);
        AnimateFixed(0.2f);
        if (currentFrame == 10) {
            AoeSlam slam = new AoeSlam();
            slam.SetOrigin(slam.width / 2, slam.height / 2);
            parent.AddChild(slam);
            slam.SetXY(x, y);

        }
        if (currentFrame == 11) {
            waitStartTime = Time.time;
            state = WAIT;
        }

    
    }
    void AOESpikes() {
        SetCycle(4,2);
        AnimateFixed(0.1f);
        if (currentFrame == 5)
        {
            AoeSpikes spike1 = new AoeSpikes();
            spike1.SetOrigin(0, spike1.height / 2);
            parent.AddChild(spike1);
            if (!_mirrorX)
                spike1.SetXY(x + width / 4, y + height / 2);
            else
                spike1.SetXY(x - width / 4, y + height / 2);
            spike1.rotation = lastRotation;


            AoeSpikes spike2 = new AoeSpikes();
            spike2.SetOrigin(0, spike2.height / 2);
            parent.AddChild(spike2);
            if (!_mirrorX)
                spike2.SetXY(x + width / 4, y + height / 2);
            else
                spike2.SetXY(x - width / 4, y + height / 2);
            spike2.rotation = lastRotation - 10;


            AoeSpikes spike3 = new AoeSpikes();
            spike3.SetOrigin(0, spike3.height / 2);
            parent.AddChild(spike3);
            if (!_mirrorX)
                spike3.SetXY(x + width / 4, y + height / 2);
            else
                spike3.SetXY(x - width / 4, y + height / 2);
            spike3.rotation = lastRotation + 10;

            AttackSpikes attackSpike1 = new AttackSpikes();
            attackSpike1.SetOrigin(attackSpike1.width/2, attackSpike1.height / 2);
            parent.AddChild(attackSpike1);
            if (!_mirrorX)
            {
                attackSpike1.SetXY(x+10 , y + height / 4);
            }
            else { 
                attackSpike1.SetXY(x-10 , y + height / 4);
                attackSpike1.Mirror(false, true);
            }

            attackSpike1.rotation = lastRotation;


            AttackSpikes attackSpike2 = new AttackSpikes();
            attackSpike1.SetOrigin(attackSpike2.width / 2, attackSpike2.height / 2);
            parent.AddChild(attackSpike2);
            if (!_mirrorX)
            {
                attackSpike2.SetXY(x+10 , y + height / 4);
            }
            else
            {
                attackSpike2.SetXY(x-10 , y + height / 4);
                attackSpike2.Mirror(false, true);
            }
            attackSpike2.rotation = lastRotation - 10;


            AttackSpikes attackSpike3 = new AttackSpikes();
            attackSpike3.SetOrigin(attackSpike3.width / 2, attackSpike3.height / 2);
            parent.AddChild(attackSpike3);
            if (!_mirrorX)
            {
                attackSpike3.SetXY(x+10 , y + height / 4);
            }
            else
            {
                attackSpike3.SetXY(x-10 , y + height / 4);
                attackSpike3.Mirror(false, true);
            }
            attackSpike3.rotation = lastRotation + 10;

            waitStartTime = Time.time;
            state = WAIT;
        }

    }

    void Wait() {
        SetCycle(6, 2);
        AnimateFixed(0.1f);
        changeDirection = true;
        if (Time.time - waitStartTime > waitTime) {
            
            state = CHASE;
        }
    }
    protected override void MoveEnemy(float angle)
    {
        rotation = angle;
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
        float finalSpeed = speed * deltaTimeClamped / 1000;
        float lastX = x;
        float lastY = y;
        Move(finalSpeed, 0);
        Console.WriteLine(rotation);
        GameObject[] overlaps = GetCollisions(false, true);
        foreach (GameObject collision in overlaps)
        {
            if (collision is Wall)
            {
                Console.WriteLine(rotation);
                SetXY(lastX, lastY);
                if (state != DASH) {
                    Move(finalSpeed, 0);
                    Move(finalSpeed, 0);
                }
                
                break;
            }
        }

        rotation = 0;
    }

}
