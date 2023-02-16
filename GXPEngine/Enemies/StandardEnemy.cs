using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
public class StandardEnemy : Enemy
{

    public StandardEnemy(Player pPlayer) : base("StandardEnemy.png",4,1, pPlayer){
        EnemySetStats(165f,10,50);
        lastSpeed = 165f;
        scoreOnDeath =20;
    }



}

