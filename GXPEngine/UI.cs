using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using GXPEngine;
using System.Xml;
using TiledMapParser;
using System.Runtime.Remoting.Messaging;
using System.Reflection;

public class UI : GameObject
{
    GameData gameData;
    Camera camera;
    string levelName;

    EasyDraw hpBar = new EasyDraw(100, 100, false);
    EasyDraw scoreCounter = new EasyDraw(100, 100, false);
    EasyDraw timer=new EasyDraw(120, 100, false);
    EasyDraw stage = new EasyDraw(100,100,false);
    EasyDraw bulletCounter=new EasyDraw(100,100,false);

    Sprite clock = new Sprite("Clock.png",false,false);
    
    Sprite pointer = new Sprite("Pointer.png",false,false);
    
    bool addedStage = false;
    int lastScore;

    public UI(GameData pGameData, Camera pCamera)
    {
        gameData = pGameData;
        camera = pCamera;
        lastScore = gameData.score;
        AddChild(hpBar);

        scoreCounter.TextSize(10);
        scoreCounter.TextAlign(CenterMode.Center, CenterMode.Min);
        scoreCounter.Text(String.Format("Score : {0}", gameData.score));
        AddChild(scoreCounter);

        bulletCounter.TextSize(8);
        bulletCounter.TextAlign(CenterMode.Center, CenterMode.Min);
        AddChild(bulletCounter);

        clock.SetOrigin(clock.width / 2, clock.height / 2);
        pointer.SetOrigin(pointer.width / 2, 0);
        pointer.rotation = 90;
        clock.AddChild(pointer);

        timer.TextSize(10);
        timer.TextAlign(CenterMode.Center, CenterMode.Min);
        AddChild(timer);


        stage.TextSize(12);
        stage.SetXY(-25, -300);
        stage.TextAlign(CenterMode.Center, CenterMode.Min);
        AddChild(stage);

    }

    void Update(){
        //pointer.rotation++;
        var result = camera.ScreenPointToGlobal(0, 0);
        UpdateLocations(result.x,result.y);
        AddCashCounter();
        //AddTimer();
        AddStage();
        RotateClock();
        BulletCounter();
    }

    public void AddPlayerHpBar(float hpPercentage,int playerHp) {
        hpBar.graphics.Clear(Color.Empty);
        hpBar.ShapeAlign(CenterMode.Min, CenterMode.Min);
        hpBar.NoStroke();
        hpBar.Fill(0, 255, 0);
        hpBar.Rect(0, 0, 80.0f * hpPercentage, 10);
        hpBar.StrokeWeight(1);
        hpBar.NoFill();
        hpBar.Rect(0, 0, 80.0f, 10);
        hpBar.TextSize(7);
        hpBar.Fill(255, 255, 255);
        hpBar.TextAlign(CenterMode.Min,CenterMode.Min);
        hpBar.Text(String.Format("         {0}/{1}",playerHp,gameData.playerMaxHp));
    }

    void AddCashCounter() {

        if (lastScore != gameData.score){
            scoreCounter.graphics.Clear(Color.Empty);

            scoreCounter.TextAlign(CenterMode.Center, CenterMode.Min);
            scoreCounter.Text(String.Format("Score : {0}", gameData.score));
            
        }

    }

    void RotateClock(){
        if (levelName == "Level1.tmx") { 
            float targetChange = 360 / ((float)(gameData.dayLength + gameData.nightLength) / 1000);
            int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
            float finalChange = targetChange * deltaTimeClamped / 1000;
            pointer.rotation += finalChange;
        }
    }

    void BulletCounter(){
        if (levelName == "Level1.tmx"){
            bulletCounter.graphics.Clear(Color.Empty);
            if (gameData.selectedWeapon!=-1)
                bulletCounter.Text(String.Format("Bullets : {0}", gameData.GunBullets[gameData.selectedWeapon]));
        }

    }

    void AddStage() {
        if (!addedStage&& levelName == "Level1.tmx") { 
            stage.graphics.Clear(Color.Empty);
            stage.Text(String.Format("Day : {0}", gameData.stage + 1));
            addedStage = true;
        }
    }

    public void SetLevel(String pLevel){
        levelName = pLevel;
        if (levelName == "Level1.tmx") {
            AddChild(clock);
        }
    }
    void UpdateLocations(float x,float y) {
        hpBar.SetXY(x+10, y+10);
        scoreCounter.SetXY(x+575, y+10);//475
        stage.SetXY(x+281,y+8);//280
        timer.SetXY(x+270,y+33);
        clock.SetXY(x + 330, y + 50);//50
        bulletCounter.SetXY(x,y+35);
    }

}

