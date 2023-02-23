using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;
using TiledMapParser;

public class Lightning : Gun
{

    public Lightning(Player pPlayer, Camera pCamera, GameData pGameData) : base("Lighting_bolt.png", pPlayer, pCamera, pGameData,1,1,1)
    {
        damage = gameData.LIGHTNINGDAMAGE;
        shootCooldown = gameData.LIGHTNINGSPEED;
        tweenTime = 500;
        tweenDelta = 80;
        RandomizeShootTime(50,150);
        shotSound = new Sound("RocketLauncherFire.mp3");
        targetVolume = 0.15f;
        changeLocation = true ;
        SetOrigin(16, 3);//16
        weaponX = 8; weaponY = 3;
    }

    protected override void Update()
    {
        base.Update();
        if (Time.time > lastShootTime + shootCooldown - shootCooldown/5)
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
        var result = camera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);
        Console.WriteLine(result);
        gameData.GunBullets[slot]--;
        Bolt bolt = new Bolt(player);
        bolt.SetXY(bulletSpawnLocationX, bulletSpawnLocationY);
        //bolt.rotation = 270;
        bolt.SetTargetXY(result.x,result.y,damage);
        Tween tween = new Tween(TweenProperty.x, TweenProperty.y, TweenProperty.rotation, tweenTime, tweenDelta, 1);
        camera.AddChild(tween);
        parent.parent.AddChild(bolt);

        //Bullet rocket = new Rocket(player);
        //rocket.SetXY(bulletSpawnLocationX, bulletSpawnLocationY);
        //rocket.rotation = angle;
        //rocket.SetDamage(damage);
        //Tween tween = new Tween(TweenProperty.x,TweenProperty.y,TweenProperty.rotation,tweenTime,tweenDelta,1);
        //camera.AddChild(tween);
        //parent.parent.AddChild(rocket);
        SoundChannel shotSoundChannel = shotSound.Play();
        shotSoundChannel.Volume = targetVolume;
    }


}