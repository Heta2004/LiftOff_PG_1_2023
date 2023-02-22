using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;

public class Bullet : AnimationSprite {
    protected float speed = 900.0f;
    protected int damage;
    protected Player player;
    public Bullet(Player pPlayer,string filename,int cols,int rows,int frames) : base(filename,cols,rows,frames) {
        player = pPlayer;
        SetOrigin(width / 2, height / 2);
        collider.isTrigger = true;
    }

    protected virtual void Update() {
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);

        float finalSpeed = speed * deltaTimeClamped / 1000;

        Move(finalSpeed, 0);
        HitTarget();
    }

    protected virtual void HitTarget() {

        GameObject[] collisions = GetCollisions();
        bool destroy = false;

        foreach (GameObject col in collisions) {
            if (col is Enemy) {
                ((Enemy)col).DamageEnemy(damage);
                destroy = true;
                break;
            }
            if (col is BreakableVase) { 
                ((BreakableVase)col).StartAnimation();
                destroy= true;
            }
            if (col is Wall||col is DestructibleWall)
                destroy = true;
        }

        Destroy(destroy);

    }

    protected virtual void Destroy(bool destroy){
        if (destroy)
            this.Destroy();
    }

    public void SetDamage(int pDamage) { 
        damage=pDamage;
    }
}

