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
    int time = -20000000;
    int timeUntilActivation = 10000;
    Sound sound = new Sound("Teleporter_Shorter.wav");
    public Teleporter(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows) {
        collider.isTrigger= true;
    }

    void Update() {

        if (Time.time - time > timeUntilActivation&&!activated) { 
            activated= true;
            time = -20000000;
            Animate(1);
        }

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
        sound.Play();
    }

    public void ChangeActivation() {
        activated = !activated;
        time = Time.time;
        Animate(1);
    }

    public void SetPlayer(Player pPlayer) { 
        player= pPlayer;
    }

    public void SetNumber(int pNumber) { 
        number=pNumber;
    }

    public bool CheckActivation() {
        return activated;
    }

}
