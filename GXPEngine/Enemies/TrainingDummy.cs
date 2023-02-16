using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class TrainingDummy:Enemy{
    bool lowHp = false;
    public TrainingDummy(Player player) : base("sprite_targetdummy04.png",1,1,player) {
        hp = 200;
        maxHp = 200;
    }
    protected override void Update() { 
        base.Update();
        if (hp < 30) {
            lowHp = true;
            hp = maxHp;
        }

    
    }

    public bool CheckIfLowHp() {
        return lowHp;
    
    }
}
