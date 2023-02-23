using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
public class StandardEnemy : StandardEnemyBase
{

    public StandardEnemy(Player pPlayer) : base("Snek_attac.png", 9,1, pPlayer){
        EnemySetStats(80f,10,50);
        lastSpeed = 80f;
        scoreOnDeath =20;
        RandomizeSpeed(6, 15);
        SetCycle(0, 4);
    }



}

