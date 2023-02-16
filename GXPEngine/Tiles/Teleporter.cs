using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class Teleporter:AnimationSprite{
    Player player;
    bool activated = true;
    TeleporterManager tm;
    int number;
    public Teleporter(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows) {
        number = obj.GetIntProperty("number");
    }

    void Update() {
        if (HitTest(player)&&activated) {
            tm.ChooseTarget(this);   
        }
    }

    public void SetTeleportManager(TeleporterManager pTm) {
        tm = pTm;
    }
    public int GetNumber() {
        return number;
    }
    public void Teleport(float x,float y) {
        player.SetXY(x,y);
    }

    public void ChangeActivation() {
        activated = !activated;
    }

}
