using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;

public class Mosin : Gun
{

    public Mosin(Player pPlayer, Camera pCamera,GameData pGameData) : base("Mosin_Old.png", pPlayer, pCamera,pGameData)
    {
        damage = 25;
        shootCooldown = 500;
        tweenTime = 250;
        tweenDelta = 25;

        RandomizeShootTime(15,50);

        shotSound = new Sound("mosinShot.mp3");
        targetSound = "mosinShot.mp3";
        targetVolume = 0.20f;
    }

    
}