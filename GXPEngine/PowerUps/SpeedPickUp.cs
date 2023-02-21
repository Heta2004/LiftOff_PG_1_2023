using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class SpeedPickUp : Sprite
{

    public SpeedPickUp() : base("feather.png"){
        SetScaleXY(0.1f, 0.1f);
        collider.isTrigger = true;  
    }
}
