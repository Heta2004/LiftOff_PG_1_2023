using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;

public class Spear : Gun{

    public Spear(Player pPlayer, Camera pCamera, GameData pGameData) : base("spear.png", pPlayer, pCamera, pGameData,1,1,1){
        damage = 75;

        shootCooldown = 750;
        tweenTime = 75;
        tweenDelta = 9;
        RandomizeShootTime(20, 40);

        shotSound = new Sound("ak1.mp3");
        targetVolume = 0.15f;
        changeLocation = true;
        weaponX= 6;
        weaponY= 0;
        SetOrigin(10,2);
    }
    protected override void Update()
    {
        base.Update();
        if (Time.time > lastShootTime + shootCooldown - 250)
        {
            alpha = 1f;
        }
        else
        {
            alpha = 0f;
        }

    }

    protected override void CreateBullet()
    {
        gameData.GunBullets[slot]--;
        BulletSniper bullet = new BulletSniper(player);
        bullet.SetXY(bulletSpawnLocationX, bulletSpawnLocationY);
        bullet.SetScaleXY(1.4f);
        bullet.rotation = angle;
        bullet.SetDamage(damage);
        parent.parent.AddChild(bullet);

        Tween tween = new Tween(TweenProperty.x, TweenProperty.y, tweenTime, tweenDelta, 1);
        camera.AddChild(tween);

        SoundChannel shotSoundChannel = shotSound.Play();
        shotSoundChannel.Volume = targetVolume;

    }



}