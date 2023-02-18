using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class InfiniteKnife:Gun{

    public InfiniteKnife(Player pPlayer,Camera pCamera,GameData pGameData) : base("knife.png", pPlayer, pCamera,pGameData) {
        damage = 10;

        shootCooldown = 350;
        tweenTime = 75;
        tweenDelta = 9;
        //RandomizeShootTime(20, 40);

        shotSound = new Sound("ak1.mp3");
        targetVolume = 0.15f;

    }

}
