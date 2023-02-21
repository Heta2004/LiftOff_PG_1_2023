using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;
using Tools;

public class Teleporter:AnimationSprite{
    Player player;
    bool activated = true;
    TeleporterManager tm;
    int number;
    public Teleporter(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows) {
        collider.isTrigger= true;
    }

    void Update() {
        
        if (HitTest(player)&&activated&&DirectionRelatedTools.CalculateDistance(x,y,player.x,player.y)<=20) {
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
        Animate(1);
    }

    public void SetPlayer(Player pPlayer) { 
        player= pPlayer;
    }

    public void SetNumber(int pNumber) { 
        number=pNumber;
    }

}
