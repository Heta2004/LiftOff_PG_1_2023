using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;

public class AK : Gun{

    public AK(Player pPlayer, Camera pCamera,GameData pGameData) : base("Ak.png", pPlayer, pCamera,pGameData){
        damage = 15;

        shootCooldown= 200;
        tweenTime = 75;
        tweenDelta = 9;
        RandomizeShootTime(20,40);

        shotSound = new Sound("ak1.mp3");
        targetVolume = 0.15f;
    }

}