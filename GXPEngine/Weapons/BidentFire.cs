using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class BidentFire:Bullet{

    public BidentFire(Player pPlayer):base(pPlayer, "Fire.png",3,1,3) {
        speed = 300f;
    }
    protected override void Update()
    {
        AnimateFixed(0.5f);
        base.Update(); 
    }
}
