using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
public class StandardEnemy : StandardEnemyBase
{

    public StandardEnemy(Player pPlayer) : base("StandardEnemy.png",4,1, pPlayer){
        EnemySetStats(80f,10,50);
        lastSpeed = 80f;
        scoreOnDeath =20;
        RandomizeSpeed(6, 15);
    }



}

