using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class DestructibleWall : AnimationSprite
{
    Minotaur minotaur;
    public DestructibleWall(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows){
    }

    void Update() { 
        if (minotaur!=null)
            if(HitTest(minotaur))
                Destroy();
    
    }


    public void SetMinotaur(Minotaur pMinotaur) { 
        minotaur = pMinotaur;
    }
}