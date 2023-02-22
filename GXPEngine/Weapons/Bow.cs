using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;

public class Bow : Gun
{
    bool animate=false;
    bool shotBullet = false;
    public Bow(Player pPlayer, Camera pCamera,GameData pGameData) : base("Bow_animation.png", pPlayer, pCamera, pGameData, 8, 1, 8)
    {
        damage = 25;
        shootCooldown = 500;
        tweenTime = 250;
        tweenDelta = 25;

        RandomizeShootTime(15,50);

        shotSound = new Sound("mosinShot.mp3");
        targetSound = "mosinShot.mp3";
        targetVolume = 0.20f;

        currentFrame = 7;
        bulletSprite = "arrow.png";
        changeLocation = true;
        weaponX = 10;
        weaponY = 0;
    }
    protected override void Update()
    {
        Console.WriteLine(currentFrame);
        base.Update();
        Animate();
    }

    protected override void Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time > lastShootTime + shootCooldown && gameData.GunBullets[slot] > 0){
            animate = true;
            shotBullet = false;
        }
    }
    void Animate() {
        if (animate)
        {
            SetCycle(0, 8);
            AnimateFixed(0.5f);
            if (currentFrame == 3&&!shotBullet) { 
                CreateBullet();
                lastShootTime = Time.time;
                shotBullet = true;
            }
            if (currentFrame == 7)
                animate = false;
        }
        else { 
            currentFrame= 0;
        }
    }

    protected override void CreateBullet(){
        gameData.GunBullets[slot]--;
        Bullet bullet = new Bullet(player, bulletSprite,1,1,1);
        bullet.SetScaleXY(1.5f);
        bullet.SetXY(bulletSpawnLocationX, bulletSpawnLocationY);
        bullet.rotation = angle;
        bullet.SetDamage(damage);

        Bullet bullet1 = new Bullet(player, bulletSprite,1,1,1);
        bullet1.SetScaleXY(1.5f);
        bullet1.SetXY(bulletSpawnLocationX-10, bulletSpawnLocationY-10);
        bullet1.rotation = angle;
        bullet1.SetDamage(damage);

        Bullet bullet2 = new Bullet(player, bulletSprite, 1, 1, 1);
        bullet2.SetScaleXY(1.5f);
        bullet2.SetXY(bulletSpawnLocationX+10, bulletSpawnLocationY+10);
        bullet2.rotation = angle;
        bullet2.SetDamage(damage);

        parent.parent.AddChild(bullet);
        parent.parent.AddChild(bullet1);
        parent.parent.AddChild(bullet2);

        Tween tween = new Tween(TweenProperty.x, TweenProperty.y, tweenTime, tweenDelta, 1);
        camera.AddChild(tween);

        SoundChannel shotSoundChannel = shotSound.Play();
        shotSoundChannel.Volume = targetVolume;

    }

}