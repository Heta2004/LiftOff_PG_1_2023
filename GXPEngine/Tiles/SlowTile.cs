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
    
}