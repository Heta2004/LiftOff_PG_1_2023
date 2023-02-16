using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
public class TankEnemy : Enemy
{


    public TankEnemy(Player pPlayer) : base("TankRun.png",4,1, pPlayer){
        EnemySetStats(85f, 25, 200);
        lastSpeed = 85f;
        scoreOnDeath = 40;
    }



}

