using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class SpeedPickUp : Sprite
{

    public SpeedPickUp() : base("Power-up_speed.png")
    {
        SetScaleXY(0.75f, 0.75f);
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;  
    }
}
