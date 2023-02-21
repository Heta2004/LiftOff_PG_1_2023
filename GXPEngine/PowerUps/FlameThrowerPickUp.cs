using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class FlameThrowerPickUp:Sprite{

    public FlameThrowerPickUp() : base("circle.png") {
        SetScaleXY(0.25f, 0.25f);
        collider.isTrigger= true;
    }
}
