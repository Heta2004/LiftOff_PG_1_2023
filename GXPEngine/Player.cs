using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;
using Tools;

public class Player : AnimationSprite {
    const int IDLE = 1;
    const int RUN = 2;

    int state = IDLE;

    float speedX = 0.0f;
    float speedY = 0.0f;

    float speedChange = 200.0f;
    float lastSpeedChange = 200.0f;

    bool isMoving = false;

    Camera camera;
    GameData gameData;
    UI ui;

    int hp = 100;

    int cooldown = 750;
    int blinkTime = 100;
    int time=1000;
    int lastHitTime;

    int particleCooldown = 500;
    int particleTotalTime = 0;

    float angle;
    int flameDuration = 7500;
    int flameStart = -100000;
    int lastFlameShot = 0;
    int flameDelay = 100;
    float flameAngle;

    int timeGotSpeedBoost = -1000000;
    bool decreasedSpeed = true;

    public Player(string filename, int cols, int rows, TiledObject obj = null) : base(filename,cols,rows) {
        SetOrigin(width/2,height/2);
        //SetScaleXY(1.5f, 1.5f);
        SetCycle(0,11);
        collider.isTrigger = true;
        
    }

    void Update() {
        RotatePlayer();
        speedChange = lastSpeedChange;
        CheckCollisions();
        CheckKeyPresses();
        Invulnerability();
        AnimateFixed(0.8f);
        CheckKeyPresses();
        switch (state) {
            case IDLE:
                IdleState();
                break;
            case RUN: 
                DecideMovement(); 
                break;
            default: 
                Console.WriteLine("fix the game");
                break;
        }
        ShootFlames();
        SpeedBoost();

    }

    void IdleState() {
        SetCycle(8,11);
        
        if (isMoving)
            state = RUN;
    }


    void DecideMovement() {
        SetCycle(0, 7);

        AddStepParticle();

        if (!isMoving){
            state = IDLE;
            return;
        }

        int deltaTimeClamped = Math.Min(Time.deltaTime,40);

        float finalSpeedX = speedX * deltaTimeClamped / 1000;
        float finalSpeedY = speedY * deltaTimeClamped / 1000;
        flameAngle = DirectionRelatedTools.CalculateAngle(x,y,x+ finalSpeedX * (1.0f + gameData.playerSpeedIncrease), y+ finalSpeedY * (1.0f + gameData.playerSpeedIncrease));
        //Console.WriteLine(flameAngle);
        MoveUntilCollision(finalSpeedX*(1.0f+gameData.playerSpeedIncrease), finalSpeedY*(1.0f + gameData.playerSpeedIncrease));

        speedX = 0.0f;
        speedY = 0.0f;

    }

    void AddStepParticle() {
        particleTotalTime += Time.deltaTime;
        if (particleTotalTime >= particleCooldown)
        {
            Particle dust = new Particle("dust.png", BlendMode.NORMAL, 150, 8, 1, 8);
            parent.AddChild(dust);
            dust.SetXY(x, y + height / 4);
            particleTotalTime -= particleCooldown;
        }
    }


    void CheckKeyPresses(){
        isMoving = false;
        speedX = 0.0f;
        speedY = 0.0f;

        if (Input.GetKey(Key.W)){
            speedY -=speedChange;
            isMoving = true;
        }
        if (Input.GetKey(Key.S)){
            speedY +=speedChange;
            isMoving = true;
        }
        if (Input.GetKey(Key.A)){
            speedX -=speedChange;
            isMoving = true;
        }
        if (Input.GetKey(Key.D)){
            speedX +=speedChange;
            isMoving = true;
        }

    }

    public void TakeDamage(int damage) {
        
        if (Time.time > cooldown + lastHitTime){
            color = 0xFF7E63;
            time = 0;
            hp=hp-Math.Max((damage-gameData.playerArmor),0);
            lastHitTime= Time.time;
            ui.AddPlayerHpBar((float)hp / gameData.playerMaxHp,hp);
        }
        if (hp <= 0) {
            ((MyGame)game).reset = true;
            ((MyGame)game).LoadLevel("mainMenu.tmx");
        }
    }

    public void SetCamera(Camera pCamera) {
        camera = pCamera;
        //AddChild(camera);
    }

    public void SetGameData(GameData pGameData) { 
        gameData=pGameData;
        hp = gameData.playerMaxHp;
    }

    public void SetUI(UI pUi) {
        ui = pUi;
        ui.AddPlayerHpBar((float)hp/gameData.playerMaxHp,hp);
    }

    void Invulnerability()
    {
        if (time < cooldown)
        {
            time += Time.deltaTime;
            Blink();
        }
        else { 
            alpha = 1.0f;
            color = 0xFFFFFF;
        }
    }

    void Blink(){
        if ((time/blinkTime)%2==0){
            alpha = 1.0f;
        }
        else
            alpha = 0.3f;
    }

    void CheckCollisions() {
        GameObject[] collisions = GetCollisions();
        foreach (GameObject col in collisions){

            if (col is SlowTile) {
                speedChange = lastSpeedChange*gameData.speedDecreaseMutliplier;
            }

            if (col is HpDrop && hp < gameData.playerMaxHp){
                hp = Math.Min(hp + gameData.HPONPICKUP, gameData.playerMaxHp);
                ui.AddPlayerHpBar((float)hp / gameData.playerMaxHp, hp);
                SoundChannel soundhchannel = new Sound("potion.mp3").Play(false, 0, 0.3f);
                col.LateDestroy();
            }

            if (col is WeaponPickUp){
                switch (((WeaponPickUp)col).CheckWeaponType()){
                    case "ak":
                        AddWeaponAndAmmo(gameData.AK,gameData.MAXAKBULLETS,col);
                        break;
                    case "mosin":
                        AddWeaponAndAmmo(gameData.MOSIN,gameData.MAXMOSINBULLETS,col);
                        break;
                    case "rocketLauncher":
                        AddWeaponAndAmmo(gameData.ROCKETLAUNCHER, gameData.MAXROCKETLAUNCHERBULLETS, col);
                        break;
                    case "sniper":
                        AddWeaponAndAmmo(gameData.SNIPER,gameData.MAXSNIPERBULLETS, col);
                        break;
                    default:
                        Console.WriteLine("fix the game");
                        break;
                }
                
            }

            if (col is FlameThrowerPickUp) {
                flameStart = Time.time;
                col.Destroy();
            }

            if (col is SpeedPickUp) {
                gameData.playerSpeedIncrease += gameData.SPEEDINCREASEPICKUP;
                timeGotSpeedBoost= Time.time;
                decreasedSpeed = false;
                col.Destroy();
            }
        }

    }

    void AddWeaponAndAmmo(int weaponType,int bullets,GameObject col) {
        bool exists = false;
        for (int i = 0; i < gameData.gunArray.Count; i++)
            if (gameData.gunArray[i] == weaponType){
                gameData.GunBullets[i] = bullets;
                exists = true;
                col.Destroy();
                break;
            }
        if (!exists&& gameData.gunArray.Count < gameData.maxGunNumber)
        {
            gameData.gunArray.Add(weaponType);
            gameData.GunBullets.Add(bullets);
        }


    }

    public bool CheckIsMoving(){
        return isMoving;
    }

    void RotatePlayer() {
        var result = camera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);
        angle=DirectionRelatedTools.CalculateAngle(x, y, result.x, result.y);
        if (angle > 90 && angle < 270)
            Mirror(true, false);
        else
            Mirror(false, false);
    }

    public float CheckAngle() {
        return angle;
    }

    void SpeedBoost() {
        if ((Time.time - timeGotSpeedBoost > gameData.speedBoostDuration)&&!decreasedSpeed) {
            gameData.playerSpeedIncrease -= gameData.SPEEDINCREASEPICKUP;
            decreasedSpeed = true;
        }
    }
    void ShootFlames() {
        if (Time.time-flameStart<flameDuration) {
            if (Time.time - lastFlameShot > flameDelay) {
                Bullet flame = new Bullet(this, "bullet.png");
                flame.SetXY(x + width / 2, y);
                flame.rotation = flameAngle;
                flame.SetDamage(5);
                parent.parent.AddChild(flame);
                lastFlameShot = Time.time;
            }
        }
    }
}

