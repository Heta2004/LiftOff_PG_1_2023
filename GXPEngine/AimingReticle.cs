using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class AimingReticle : Sprite {
    Camera camera;
    public AimingReticle(Camera pCamera) :base("aimThing.png",false,false)
    { 
        camera= pCamera;
    }
    void Update() {
        var result = camera.ScreenPointToGlobal(Input.mouseX, Input.mouseY);
        x = result.x;
        y = result.y;
    }
}
