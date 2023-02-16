using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class Wall : AnimationSprite
{

    public Wall(string filename, int cols, int rows, TiledObject obj = null) : base(filename,cols,rows){
    }

}