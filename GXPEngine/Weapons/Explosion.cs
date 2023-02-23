using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class Explosion:AnimationSprite{
    private int timeAlive = 500;
    private int startTime = Time.time;
    private int explosionDamage;
    public Explosion() : base("Lighting_explosion.png", 6,1,6) {
        SetOrigin(width / 2,height/2);
        SetScaleXY(2.5f, 2.5f);//3f
    }

    void Update() {
        AnimateFixed(0.6f);//0.5f
        GameObject[] collisions = GetCollisions(true,false);

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
    public void SetDamage(int damage) { 
        explosionDamage = damage;
    }
}
