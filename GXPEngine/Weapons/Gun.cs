using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
public class Gun : Sprite{
    protected Player player;
    protected Camera camera;
    protected GameData gameData;

    protected float angle;
    protected int gunNumber;
    protected int lastShootTime = 0;
    protected int shootCooldown = 0;
    protected int damage = 0;
    protected Pivot weaponTip = new Pivot();
    protected float bulletSpawnLocationX;
    protected float bulletSpawnLocationY;

    protected int tweenTime;
    protected int tweenDelta;
    protected Sound shotSound;
    protected string targetSound;
    protected float targetVolume;
    protected int slot;
    
    public Gun(String filename, Player pPlayer, Camera pCamera, GameData pGameData) : base(filename, false, false){
        this.AddChild(weaponTip);
        SetOrigin(width / 2, height / 2);

        gameData = pGameData;
        camera = pCamera;
        player = pPlayer;

        if (gameData.gunNumber < 0)
            gameData.gunNumber = 0;
        gameData.gunNumber++;
        gunNumber = gameData.gunNumber;
        weaponTip.x = x;


    }
    protected void Update() {
        

        var result = camera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);
        x = 0;
        rotation = 0;
        var result2 = TransformPoint(weaponTip.x, weaponTip.y);
        //var result2 = TransformPoint(player.x, player.y);
        //Mirror(false, true);
        
        bulletSpawnLocationX = result2.x;
        bulletSpawnLocationY = result2.y;
        angle = Tools.DirectionRelatedTools.CalculateAngle(result2.x, result2.y, result.x, result.y);
        rotation = angle;
        RotateAndChangeLocation();
        Shoot();

    }

    void RotateAndChangeLocation() {
        if (angle >= 90 && angle <= 270){
            Mirror(false, true);
            x = -20;
        }
        else{
            Mirror(false, false);
            x = 20;
        }
    }


    void Shoot() {
        if (Input.GetMouseButton(0) && Time.time > lastShootTime + shootCooldown&& gameData.GunBullets[slot] > 0) {
            lastShootTime = Time.time;
            CreateBullet();
        }
    }

    protected virtual void CreateBullet() {
        gameData.GunBullets[slot]--;
        Bullet bullet = new Bullet(player, "bullet.png");
        bullet.SetXY(bulletSpawnLocationX,bulletSpawnLocationY);
        bullet.rotation = angle;
        bullet.SetDamage(damage);
        parent.parent.AddChild(bullet);

        Tween tween = new Tween(TweenProperty.x, TweenProperty.y, tweenTime, tweenDelta,1);
        camera.AddChild(tween);

        ParticleGunSmoke smoke = new ParticleGunSmoke();
        weaponTip.AddChild(smoke);
        smoke.SetXY(width / 2, -height / 9);
        smoke.rotation = 0;
        SoundChannel shotSoundChannel = shotSound.Play();
        shotSoundChannel.Volume = targetVolume;

    }

    protected override void OnDestroy() {
        gameData.gunNumber--;
    }

    protected void RandomizeShootTime(int minValue,int maxValue) {
        var rand = new Random();
        int randomNumber = rand.Next(minValue, maxValue+1);
        int randomNumber2= rand.Next(1, 3);
        if (randomNumber2 == 1){
            shootCooldown += randomNumber;
        }
        else {
            shootCooldown -= randomNumber;
        }

    }

    public void SetSlot(int pSlot) { 
        slot=pSlot;
    }

}