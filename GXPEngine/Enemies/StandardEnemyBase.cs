using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using Tools;

public class StandardEnemyBase : Enemy {
    protected const int DAY = 1;
    protected const int NIGHT = 2;
    protected int basicState = 1;
    protected int walkCooldown = 3000;
    protected int walkStartTime = 0;
    protected float spawnX;
    protected float spawnY;
    protected float targetX;
    protected float targetY;
    protected float walkRange = 30;

    protected int pathfindingState=1;
    protected const int SLIDE = 2;
    protected const int CHASEPLAYER = 1;
    protected int pathfindingRotation;

    protected float pathfindingX;
    protected float pathfindingY;
    protected float frameTime;

    public StandardEnemyBase(string filename, int cols, int rows, Player pPlayer) : base(filename, cols, rows,pPlayer) { 
    
    }

    protected override void Update(){
        speed = lastSpeed;
        AnimateFixed(0.65f);
        if (gameData != null)
        {
            if (gameData.gameState == gameData.NIGHT)
                basicState = NIGHT;
        }
        switch (basicState){
            case DAY:
                WalkAround();
                break;
            case NIGHT:
                ChasePlayer();
                break;

        }
        base.Update();
    }

    public void setSpawnXY(float x, float y)
    {
        spawnX = x;
        spawnY = y;
    }

    protected override void ChasePlayer(){
        switch (pathfindingState) {
            case CHASEPLAYER:
                base.ChasePlayer();
                Choose(angle);
                break;
            case SLIDE:
                Slide();
                break;
        
        }
    }

    protected void Slide() {
        rotation = angle;
        float oldX = x;
        float oldY = y;
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
        float finalSpeed = speed * deltaTimeClamped / 1000;
        GXPEngine.Core.Vector2 worldDirection = TransformDirection(finalSpeed, 0);
        GXPEngine.Core.Collision col = MoveUntilCollision(worldDirection.x, worldDirection.y);
        rotation = 0;
        if (col == null){
            pathfindingState = CHASEPLAYER;
            frameTime = 0;
        }else {
            SetXY(oldX,oldY);
            Console.WriteLine(pathfindingRotation);
            rotation = pathfindingRotation;
            //GXPEngine.Core.Vector2 worldDirection1 = TransformDirection(finalSpeed, 0);
            //MoveUntilCollision(worldDirection1.x, worldDirection1.y);
            Move(finalSpeed,0);
            rotation = 0;
        }
        



    }

    protected virtual void WalkAround()
    {
        if (DirectionRelatedTools.CalculateDistance(x, y, player.x, player.y) < 100)
        {
            basicState = NIGHT;
        }

        if (Time.time - walkStartTime > walkCooldown)
        {
            var rand = new Random();
            targetX = rand.Next((int)(spawnX - walkRange), (int)(spawnX + walkRange));
            targetY = rand.Next((int)(spawnY - walkRange), (int)(spawnY + walkRange));
            walkStartTime = Time.time;
        }
        angle = Tools.DirectionRelatedTools.CalculateAngle(x, y, targetX, targetY);
        if (x!=targetX&&y!=targetY)
            MoveEnemy(angle);
    }

    protected override void ReactOnBeingDamaged()
    {
        basicState = NIGHT;
    }

    void Choose(float oldRotation) {
        switch ((int)(oldRotation / 45))
        {
            case 0:
                pathfindingRotation = 90;
                break;
            case 1:
                pathfindingRotation = 180;
                break;
            case 2:
                pathfindingRotation = 180;
                break;
            case 3:
                pathfindingRotation = 270;
                break;
            case 4:
                pathfindingRotation = 270;
                break;
            case 5:
                pathfindingRotation = 360;
                break;
            case 6:
                pathfindingRotation = 360;
                break;
            case 7:
                pathfindingRotation = 450;
                break;

        }

    }

    protected override void SwitchStatePathFinding(){
        pathfindingState = SLIDE;
        frameTime = 0;
        pathfindingX = x;
        pathfindingY = y;
    }
}
