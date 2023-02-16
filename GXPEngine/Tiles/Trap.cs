using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Core;
using TiledMapParser;

public class Trap:AnimationSprite{

    const int RAISESPIKES = 1;
    const int SPIKESUP = 2;
    const int LOWERSPIKES = 3;
    const int WAITING = 4;

    int targetState = 0;
    int state = 4;

    int timeSpikesStart=0;
    int timeSpikesUp = 1500;

    bool damageEnemy = false;
    bool foundCollision = false;
    Sound sound = new Sound("trap.mp3");

    int damage = 25;

    public Trap(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows) {
        collider.isTrigger = true;
    }


    void Update() {

        CheckCollisions();
        ChangeState();

        switch (state) {
            case RAISESPIKES:
                RaiseSpikes();
                break;
            case SPIKESUP:
                SpikesUp();
                break;
            case LOWERSPIKES:
                LowerSpikes();
                break;
            case WAITING:
                Waiting();
                break;
            default:
                Console.WriteLine("fix the game");
                break;
        }
    
    }

    void RaiseSpikes(){

        SetCycle(1, 6);
        AnimateFixed(1f);//1.5f
        damageEnemy = false;
        if (currentFrame == 6) { 
            sound.Play(false,0,0.2f);
            targetState = SPIKESUP;
            timeSpikesStart = Time.time;
        }

    }

    void SpikesUp() {

        damageEnemy = true;
        if (Time.time - timeSpikesStart >= timeSpikesUp)
            targetState = LOWERSPIKES;

    }

    void LowerSpikes(){
        SetCycle(7, 8);
        AnimateFixed(1.5f);
        damageEnemy = false;
        if (currentFrame == 8)
            targetState = WAITING;
    }

    void Waiting() {

        SetFrame(0);
        damageEnemy = false;
        if (foundCollision)
            targetState = RAISESPIKES;
    }

    void CheckCollisions(){

        foundCollision = false;
        GameObject[] collisions = GetCollisions();

        foreach (GameObject col in collisions){
            if (col is Enemy){

                foundCollision = true;
                if (damageEnemy && !((Enemy)col).CheckDamagedByTrap()){
                    ((Enemy)col).ChangeDamagedByTrap();
                    ((Enemy)col).DamageEnemy(damage);
                }
            }

            if (col is Player) {
                foundCollision = true;
                if (damageEnemy) 
                    ((Player)col).TakeDamage(damage);
            }

        }
    }


    void ChangeState() {
        if (targetState != 0) { 
            state = targetState;
            targetState = 0;
        }
    }

}