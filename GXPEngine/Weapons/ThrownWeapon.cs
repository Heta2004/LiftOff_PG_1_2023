using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class ThrownWeapon : Bullet {

    float oldRotation;
    float rotationChange;
    float rotationChangePerSecond = 1000;

    bool setOldRotation = false;
    public ThrownWeapon(Player pPlayer, string filename) : base(pPlayer, filename) {
        speed = 400;
        damage = 50;
    }

    protected override void Update(){
        //Console.WriteLine(x + " " + y);

        SetOldRotation();
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);

        float finalRotation= rotationChangePerSecond * deltaTimeClamped / 1000;

        rotationChange += finalRotation;
        rotation = oldRotation;

        float finalSpeed = speed * deltaTimeClamped / 1000;
        Move(finalSpeed, 0);
        HitTarget();
        rotation += rotationChange;
    }
    void SetOldRotation() {
        if (!setOldRotation) { 
            oldRotation= rotation;
            setOldRotation= true;
        }
    
    }

}
