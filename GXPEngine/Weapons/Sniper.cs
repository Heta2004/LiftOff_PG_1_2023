using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;

public class Sniper : Gun{

    public Sniper(Player pPlayer, Camera pCamera, GameData pGameData) : base("Ak.png", pPlayer, pCamera, pGameData){
        damage = 75;

        shootCooldown = 750;
        tweenTime = 75;
        tweenDelta = 9;
        RandomizeShootTime(20, 40);

        shotSound = new Sound("ak1.mp3");
        targetVolume = 0.15f;
    }

    protected override void CreateBullet()
    {
        gameData.GunBullets[slot]--;
        BulletSniper bullet = new BulletSniper(player);
        bullet.SetXY(bulletSpawnLocationX, bulletSpawnLocationY);
        bullet.rotation = angle;
        bullet.SetDamage(damage);
        parent.parent.AddChild(bullet);

        Tween tween = new Tween(TweenProperty.x, TweenProperty.y, tweenTime, tweenDelta, 1);
        camera.AddChild(tween);

        ParticleGunSmoke smoke = new ParticleGunSmoke();
        weaponTip.AddChild(smoke);
        smoke.SetXY(width / 2, -height / 9);
        smoke.rotation = 0;
        SoundChannel shotSoundChannel = shotSound.Play();
        shotSoundChannel.Volume = targetVolume;

    }



}