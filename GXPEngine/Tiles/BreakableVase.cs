using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;
using TiledMapParser;

public class BreakableVase:AnimationSprite{
    bool start=false;
    public BreakableVase(string filename, int cols, int rows, TiledObject obj = null) : base("Vase.png", 1,1,1,false,true) { 
        collider.isTrigger= true;
    }

    void Update() {
        if (start) {
            DestroyVase();
        }
    }
    void DestroyVase() {
        var rand = new Random();
        int randomNumber = rand.Next(0,7);
        switch (randomNumber) {
            case 0:
                FlameThrowerPickUp flame=new FlameThrowerPickUp();
                flame.SetXY(x,y);
                parent.AddChild(flame);
                break;
            case 1:
                SpeedPickUp speed=new SpeedPickUp();
                speed.SetXY(x,y);
                parent.AddChild(speed);
                break;
            case 2:
                WeaponPickUp bident = new WeaponPickUp("Bident.png", "bident");
                parent.AddChild(bident);
                bident.SetXY(x, y);
                break;
            case 3:
                WeaponPickUp bow = new WeaponPickUp("bow.png", "bow");
                parent.AddChild(bow);
                bow.SetXY(x, y);
                break;
            case 4:
                WeaponPickUp light = new WeaponPickUp("Lighting_bolt.png", "lightning");
                parent.AddChild(light);
                light.SetXY(x, y);
                break;
            case 5:
                WeaponPickUp spear = new WeaponPickUp("spear.png", "spear");
                parent.AddChild(spear);
                spear.SetXY(x, y);
                break;
            case 6:
                HpDrop hpDrop = new HpDrop();
                hpDrop.SetXY(x, y);
                parent.AddChild(hpDrop);
                break;
        }

        Destroy();
    }

    public void StartAnimation() {
        start = true;
    
    }

}
