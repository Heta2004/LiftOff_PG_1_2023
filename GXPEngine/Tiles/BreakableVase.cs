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
        int randomNumber = rand.Next(0,2);
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
        }

        Destroy();
    }

    public void StartAnimation() {
        start = true;
    
    }

}
