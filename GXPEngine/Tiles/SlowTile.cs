using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class SlowTile : AnimationSprite{

    public SlowTile(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows){
        collider.isTrigger = true;
        SetScaleXY(2);
    }

    void Update() { 
    
    
    }

    void CheckCollisions() {
        GameObject[] collisions = GetCollisions(true,false);
        foreach (GameObject col in collisions)
        {
            if (col is Enemy)
            {
                ((Enemy)col).speed = ((Enemy)col).lastSpeed * ((Enemy)col).speedDecreaseMultiplier;
            }
        }
    }


}