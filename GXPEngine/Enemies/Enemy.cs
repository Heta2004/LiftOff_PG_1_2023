using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GXPEngine;

public class Enemy:AnimationSprite{

    protected Player player;
    protected int hp;
    protected int maxHp;
    protected float speed;
    protected float lastSpeed;
    protected int damage;
    protected EnemySpawnManager esm;
    protected int scoreOnDeath;
    protected GameData gameData;

    protected bool damagedByExplosion = false;
    protected bool damagedByTrap = false;

    protected int lastHitTime;
    protected int lastRocketHitTime;
    protected int lastTrapHitTime;
    protected int rocketDamageCooldown=200;
    protected int trapDamageCooldown = 500;

    protected bool changeDirection = true;
    protected float angle;
    float backupPath;

    EasyDraw hpBar=new EasyDraw(25,25,false);

    public Enemy(string filename,int cols,int rows,Player pPlayer) : base(filename,cols,rows){
        SetOrigin(width / 2, height / 2);
        player = pPlayer;
        collider.isTrigger = true;
        SetCycle(0, 4);
        AddChild(hpBar);
        hpBar.SetXY(-10, -2*height/3);

    }

    protected virtual void Update() {
        speed = lastSpeed;
        AnimateFixed(0.65f);
        DamagePlayer();
        ChasePlayer();
        CheckForCooldown();
    }

     protected virtual void ChasePlayer() {

        var result = TransformPoint(player.x, player.y);
        var result2 = TransformPoint(x, y);
        angle = Tools.DirectionRelatedTools.CalculateAngle(result2.x, result2.y, result.x, result.y);
        MoveEnemy(angle);

    }


    protected virtual void MoveEnemy(float angle) {
        rotation = angle;
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
        float finalSpeed = speed * deltaTimeClamped / 1000;
        float lastX = x;
        float lastY = y;
        float oldRotation = rotation;
        int lastTry = -1;
        Move(finalSpeed, 0);

        GameObject[] overlaps = GetCollisions(false, true);


        if (changeDirection)
        {
            if (overlaps.Length > 0)
            {
                SetXY(lastX, lastY);
                switch ((int)(oldRotation / 45))
                {
                    case 0:
                        rotation = 90;
                        break;
                    case 1:
                        rotation = 180;
                        break;
                    case 2:
                        rotation = 180;
                        break;
                    case 3:
                        rotation = 270;
                        break;
                    case 4:
                        rotation = 270;
                        break;
                    case 5:
                        rotation = 360;
                        break;
                    case 6:
                        rotation = 360;
                        break;
                    case 7:
                        rotation = 450;
                        break;

                }
                backupPath = rotation;
                Move(finalSpeed, 0);
                overlaps = GetCollisions(false, true);
                lastTry++;
            }
        }
        else {
            if (overlaps.Length > 0) {
                SetXY(lastX, lastY);

            }


        }



        //if (x==lastX&& y==lastY){
        //    rotation = backupPath;
        //    Move(finalSpeed,0);
        //}
        rotation = 0;


        //while (lastTry < 2 && overlaps.Length > 0){
        //    SetXY(lastX, lastY);
        //    switch (lastTry)
        //    {
        //        case 0:
        //            rotation = oldRotation+(90-(oldRotation%90));
        //            break;
        //        case 1:
        //            rotation = oldRotation - (oldRotation%90);
        //            break;
        //    }
        //    Move(finalSpeed, 0);
        //    overlaps = GetCollisions(false, true);
        //    lastTry++;
        //}

        //rotation = 0;

        //if (changeDirection)
        //{
        //    while (lastTry < 2 && overlaps.Length > 0)
        //    {
        //        SetXY(lastX, lastY);
        //        switch (lastTry)
        //        {
        //            case 0:
        //                rotation = oldRotation - 90;
        //                break;
        //            case 1:
        //                rotation = oldRotation + 90;
        //                break;
        //        }
        //        Move(finalSpeed, 0);
        //        overlaps = GetCollisions(false, true);
        //        lastTry++;
        //    }

        //}
        //else{
        //    if (overlaps.Length > 0)
        //        SetXY(lastX, lastY);
        //}


        //while (lastTry < 2 && overlaps.Length > 0)
        //{
        //    SetXY(lastX, lastY);
        //    switch (lastTry)
        //    {
        //        case 0:
        //            rotation = oldRotation - 90;
        //            break;
        //        case 1:
        //            rotation = oldRotation + 90;
        //            break;
        //    }
        //    Move(finalSpeed, 0);
        //    overlaps = GetCollisions(false, true);
        //    lastTry++;
        //}

        //rotation = 0;


        //GameObject[] overlaps = GetCollisions(false, true);
        //if (overlaps.Length > 0){
        //    Console.WriteLine("rhanks?");
        //    SetXY(lastX, lastY);
        //}

        //if (changeDirection&&overlaps.Length > 0) {

        //    rotation -= 90;
        //    //rotation += 90;
        //    Move(finalSpeed, 0);
        //}
        //rotation = 0;
    }


    //protected virtual void MoveEnemy(float angle)
    //{
    //    rotation = angle;
    //    int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
    //    float finalSpeed = speed * deltaTimeClamped / 1000;
    //    float lastX = x;
    //    float lastY = y;
    //    Move(finalSpeed, 0);

    //    GameObject[] overlaps = GetCollisions(false, true);
    //    if (overlaps.Length > 0)
    //    {
    //        Console.WriteLine("rhanks?");
    //        SetXY(lastX, lastY);
    //    }

    //    if (changeDirection && overlaps.Length > 0)
    //    {

    //        rotation -= 90;
    //        //rotation += 90;
    //        Move(finalSpeed, 0);
    //    }
    //    rotation = 0;
    //}

    public void DamageEnemy(int damage){
        hp -= damage;
        ShowHealthBar();
        lastHitTime = Time.time;
        if (hp <= 0) {
            CheckForDrops();
            if (esm!=null)
                esm.DecreaseEnemies();
            if (gameData!=null)
                gameData.score += (int)(scoreOnDeath*gameData.scoreMultiplier);
            ScorePopUp scorePopUp = new ScorePopUp((int)(scoreOnDeath * gameData.scoreMultiplier));
            parent.AddChild(scorePopUp);
            scorePopUp.SetXY(this.x,this.y-height);

            this.LateDestroy();


        }
    }

    protected void DamagePlayer() {

        GameObject[] collisions=GetCollisions();
        foreach (GameObject col in collisions){
            if (col is Player)
                player.TakeDamage(10);
            if (col is SlowTile) {
                speed = lastSpeed * gameData.speedDecreaseMutliplier;
            }
        }
    
    }

    protected void EnemySetStats(float pSpeed,int pDamage,int pHp) {
        speed = pSpeed;
        damage = pDamage;
        hp=pHp;
        maxHp = hp;
        
        ShowHealthBar();
    }


    public void SetEnemySpawnManager(EnemySpawnManager pEsm) {
        esm = pEsm;
    }

    public void SetGameData(GameData pGameData)
    {
        gameData = pGameData;
        if (gameData != null && gameData.gameState == gameData.NIGHT)
        {
            Console.WriteLine("working");
            speed += speed * gameData.nightSpeedIncrease;
            maxHp += (int)((float)maxHp * gameData.nightHPIncrease);
            hp = maxHp;
            damage += (int)((float)damage * gameData.nightDamageIncrease);
            ShowHealthBar();
        }
    }

    public void ChangeDamagedByExplosion() { 
        damagedByExplosion=!damagedByExplosion;
        lastRocketHitTime = Time.time;
    }

    public void ChangeDamagedByTrap()
    {
        damagedByTrap = !damagedByTrap;
        lastTrapHitTime = Time.time;
    }


    public bool CheckDamagedByExplosion() {

        return damagedByExplosion;
    }

    public bool CheckDamagedByTrap() {

        return damagedByTrap;
    }
    
    protected void CheckForCooldown() {
        if (Time.time - lastRocketHitTime > rocketDamageCooldown)
            damagedByExplosion = false;
        if (Time.time - lastTrapHitTime > trapDamageCooldown)
            damagedByTrap = false;
    }


    protected void CheckForDrops() {

        var rand = new Random();
        int randomNumber = rand.Next(1, 16);
        if (randomNumber == 1) {
            HpDrop hpDrop = new HpDrop();
            hpDrop.SetXY(x, y);
            parent.AddChild(hpDrop);
        }
        if (randomNumber == 2){
            WeaponPickUp pickup;
            int randomNumber2=rand.Next(1,5);
            switch (randomNumber2) {
                case 1:
                    pickup = new WeaponPickUp("ak.png","ak");
                    parent.AddChild(pickup);
                    pickup.SetXY(x, y);
                    break;
                case 2:
                    pickup = new WeaponPickUp("Mosin_Old.png", "mosin");
                    parent.AddChild(pickup);
                    pickup.SetXY(x, y);
                    break;
                case 3:
                    pickup = new WeaponPickUp("RocketLauncher.png", "rocketLauncher");
                    parent.AddChild(pickup);
                    pickup.SetXY(x, y);
                    break;
                case 4:
                    pickup = new WeaponPickUp("ak.png", "sniper");
                    parent.AddChild(pickup);
                    pickup.SetXY(x, y);
                    break;
            }
               
        }

    }

    protected void ShowHealthBar() {
        hpBar.graphics.Clear(Color.Empty);
        hpBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
        hpBar.NoStroke();
        hpBar.Fill(255, 0, 0);
        hpBar.Rect(0, 0, 20.0f * (float)hp/(float)maxHp, 3);
    }

}

