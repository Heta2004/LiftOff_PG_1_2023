using System;
using System.Collections.Generic;
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
    //AnimationSprite idle=new AnimationSprite("",1,1,-1,false,false);
    //AnimationSprite run = new AnimationSprite("",1,1,-1,false,false);

    public Player(string filename, int cols, int rows, TiledObject obj = null) : base(filename,cols,rows) {
        SetOrigin(width/2,height/2);
        SetCycle(0,12);
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

    }

    void IdleState() {
        SetCycle(8,12);
        if (isMoving)
            state = RUN;
    }


    void DecideMovement() {
        SetCycle(0, 8);

        AddStepParticle();  

        if (!isMoving)
            state = IDLE;

        int deltaTimeClamped = Math.Min(Time.deltaTime,40);

        float finalSpeedX = speedX * deltaTimeClamped / 1000;
        float finalSpeedY = speedY * deltaTimeClamped / 1000;

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
        AddChild(camera);
    }

    public void SetGameData(GameData pGameData) { 
        gameData=pGameData;
        hp = gameData.playerMaxHp;
    }

    public void SetUI(UI pUi) {
        ui = pUi;
        ui.AddPlayerHpBar((float)hp/gameData.playerMaxHp,hp);
    }

    void Invulnerability(){
        if (time < cooldown){
            time += Time.deltaTime;
            Blink();
        }
        else
            alpha = 1.0f;
    }

    void Blink(){
        if ((time/blinkTime)%2==0){
            alpha = 1.0f;
        }
        else
            alpha = 0.5f;
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
                        AddWeaponAndAmmo(gameData.AK,gameData.MAXAKBULLETS);
                        break;
                    case "mosin":
                        AddWeaponAndAmmo(gameData.MOSIN,gameData.MAXMOSINBULLETS);
                        break;
                    case "rocketLauncher":
                        //gameData.gunArray.Add(3);
                        //gameData.GunBullets.Add(gameData.MAXROCKETLAUNCHERBULLETS);
                        break;
                    case "sniper":
                        AddWeaponAndAmmo(gameData.SNIPER,gameData.MAXSNIPERBULLETS);
                        break;
                    default:
                        Console.WriteLine("fix the game");
                        break;
                }
                col.Destroy();
            }

        }

    }

    void AddWeaponAndAmmo(int weaponType,int bullets) {
        bool exists = false;
        for (int i = 0; i < gameData.gunArray.Count; i++)
            if (gameData.gunArray[i] == weaponType){
                gameData.GunBullets[i] = bullets;
                exists = true;
                break;
            }
        if (!exists&& gameData.gunArray.Count < 4)
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

}

