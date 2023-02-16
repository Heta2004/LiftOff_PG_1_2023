using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine;

public class ScorePopUp:EasyDraw{
    int score;
    public ScorePopUp(int pScore) : base(30,30,false) { 
        score= pScore;
        TextSize(9);
        TextAlign(CenterMode.Center, CenterMode.Center);
        Text(String.Format("{0}",score));
        Tween tween = new Tween(TweenProperty.x, TweenProperty.y,500,30,1);
        tween.SetDestroy();
        AddChild(tween);
        
    }
    void Update() {
        int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
        float finalChange = ((float)deltaTimeClamped) / 700;
        alpha -= finalChange;
    }

}
