using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class Explosion:Sprite{
    private int timeAlive = 200;
    private int startTime = Time.time;
    private int explosionDamage = 50;
    public Explosion() : base("Explosion.png") {
        SetOrigin(width / 2,height/2);
    }

    void Update() {
        GameObject[] collisions = GetCollisions();

        foreach (GameObject col in collisions)
        {
            if (col is Enemy&& !((Enemy)col).CheckDamagedByExplosion()){
                ((Enemy)col).ChangeDamagedByExplosion();
                ((Enemy)col).DamageEnemy(explosionDamage);
            }
        }


        if (Time.time - startTime > timeAlive) { 
            this.LateDestroy();
        }
    
    
    }

}
