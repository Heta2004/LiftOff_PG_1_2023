using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class HpDrop:AnimSprite{

    public HpDrop() : base("Potion_weakness.png", 1,1) {
        SetOrigin(width/2,height/2);
        collider.isTrigger = true;
    }

    void Update() {
        //AnimateFixed(0.5f);
    }
}
