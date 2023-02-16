using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;
using TiledMapParser;

public class RocketLauncher : Gun
{

    public RocketLauncher(Player pPlayer, Camera pCamera, GameData pGameData) : base("RocketLauncher.png", pPlayer, pCamera, pGameData)
    {
        damage = 50;
        shootCooldown = 1000;
        tweenTime = 500;
        tweenDelta = 80;
        RandomizeShootTime(50,150);
        shotSound = new Sound("RocketLauncherFire.mp3");
        targetVolume = 0.15f;
    }

    protected override void CreateBullet()
    {
        Bullet rocket = new Rocket(player);
        rocket.x = bulletSpawnLocationX;
        rocket.y = bulletSpawnLocationY;
        rocket.rotation = angle;
        rocket.SetDamage(damage);
        Tween tween = new Tween(TweenProperty.x,TweenProperty.y,TweenProperty.rotation,tweenTime,tweenDelta,1);
        camera.AddChild(tween);
        parent.AddChild(rocket);
        SoundChannel shotSoundChannel = shotSound.Play();
        shotSoundChannel.Volume = targetVolume;
    }


}