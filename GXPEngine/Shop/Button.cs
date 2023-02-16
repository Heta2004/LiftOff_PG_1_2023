using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class Button:ButtonBase{
    string levelName;

    public Button(string filename,int cols,int rows,TiledObject obj):base(filename,cols,rows) {
        levelName=obj.GetStringProperty("levelName");
    }

    protected override void OnMouseClick(){
        ((MyGame)game).LoadLevel(levelName);
    }

}
