using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class InfiniteKnife : Gun {

    public InfiniteKnife(Player pPlayer, Camera pCamera, GameData pGameData) : base("knife.png", pPlayer, pCamera, pGameData, 1, 1, 1) {
        damage = gameData.KNIFEDAMAGE;
        shootCooldown = gameData.KNIFESPEED;
        tweenTime = 75;
        tweenDelta = 9;
        bulletSprite = "knife.png";
        shotSound = new Sound("ak1.mp3");
        targetVolume = 0.15f;

    }
    protected override void Update(){
        base.Update();
        if (Time.time > lastShootTime + shootCooldown - shootCooldown/3.5f)
        {
            alpha = 1f;
        }
        else {
            alpha = 0f;
        }

    }
}
