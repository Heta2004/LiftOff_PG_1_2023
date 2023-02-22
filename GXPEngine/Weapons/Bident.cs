using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;

public class Bident : Gun{

    public Bident(Player pPlayer, Camera pCamera,GameData pGameData) : base("Bident.png", pPlayer, pCamera,pGameData,1,1,1){
        damage = 10;

        shootCooldown= 75;
        tweenTime = 75;
        tweenDelta = 9;
        RandomizeShootTime(20,40);

        shotSound = new Sound("ak1.mp3");
        targetVolume = 0.15f;
        changeLocation = true;
        SetOrigin(8, 4);
        weaponX = 10; weaponY=0;
    }

    protected override void CreateBullet()
    {
        gameData.GunBullets[slot]--;
        BidentFire bullet=new BidentFire(player);
        bullet.SetScaleXY(3f);
        bullet.SetXY(bulletSpawnLocationX, bulletSpawnLocationY);
        bullet.rotation = angle;
        bullet.SetDamage(damage);
        bullet.SetSpawnXY(bulletSpawnLocationX, bulletSpawnLocationY);
        parent.parent.AddChild(bullet);

        Tween tween = new Tween(TweenProperty.x, TweenProperty.y, tweenTime, tweenDelta, 1);
        camera.AddChild(tween);

        //ParticleGunSmoke smoke = new ParticleGunSmoke();
        //weaponTip.AddChild(smoke);
        //smoke.SetXY(width / 2, -height / 9);
        //smoke.rotation = 0;
        SoundChannel shotSoundChannel = shotSound.Play();
        shotSoundChannel.Volume = targetVolume;

    }

}