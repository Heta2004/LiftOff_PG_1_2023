using System;
using GXPEngine;
using System.Drawing;

public class Particle : AnimationSprite{

    float vx, vy; 
    Color startColor = Color.White;
    Color endColor = Color.White;
    float startScale = 1;
    float endScale = 1;

    int totalLifeTimeMs;
    int currentLifeTimeMs = 0;

    public Particle(string filename, BlendMode blendMode, int lifeTimeMs,int cols,int rows,int frames) : base(filename,cols,rows,frames,true,false){ 
        this.blendMode = blendMode;
        totalLifeTimeMs = lifeTimeMs;
        SetOrigin(width / 2, height / 2);
    }

    public Particle SetScale(float start, float end){
        startScale = start;
        endScale = end;
        scale = start;
        return this;
    }

    public Particle SetColor(Color start, Color end){
        startColor = start;
        endColor = end;
        SetColor(start.R / 255f, start.G / 255f, start.B / 255f);
        return this; 
    }

    public Particle SetVelocity(float velX, float velY){
        vx = velX;
        vy = velY;
        return this; 
    }



    protected virtual void Update(){
        AnimateFixed(1.5f);

        currentLifeTimeMs += Time.deltaTime;

        float t = Mathf.Clamp(1f * currentLifeTimeMs / totalLifeTimeMs, 0, 1);

        float currentR = startColor.R * (1 - t) + endColor.R * t;
        float currentG = startColor.G * (1 - t) + endColor.G * t;
        float currentB = startColor.B * (1 - t) + endColor.B * t;
        SetColor(currentR / 255f, currentG / 255f, currentB / 255f);

        scale = startScale * (1 - t) + endScale * t;

        x += vx;
        y += vy;

        if (currentLifeTimeMs >= totalLifeTimeMs){
            Destroy();
        }
    }

}

