using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using GXPEngine;
using System.Xml;
using TiledMapParser;

public class UI : GameObject
{
    GameData gameData;
    Camera camera;
    string levelName;

    EasyDraw hpBar = new EasyDraw(100, 100, false);
    EasyDraw scoreCounter = new EasyDraw(100, 100, false);
    EasyDraw timer=new EasyDraw(120, 100, false);
    EasyDraw stage = new EasyDraw(100,100,false);

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


        timer.TextSize(10);
        timer.TextAlign(CenterMode.Center,CenterMode.Min);
        AddChild(timer);


        stage.TextSize(12);
        stage.SetXY(-25, -300);
        stage.TextAlign(CenterMode.Center, CenterMode.Min);
        AddChild(stage);

    }

    void Update(){
        var result = camera.ScreenPointToGlobal(0, 0);
        UpdateLocations(result.x,result.y);
        AddCashCounter();
        AddTimer();
        AddStage();
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

    void AddTimer(){
        if (levelName == "Level1.tmx"){
            timer.graphics.Clear(Color.Empty);
            switch (gameData.gameState) {
                case 1:
                    timer.Text(String.Format("Day time left : {0}", ((gameData.dayLength - Time.time + gameData.levelStartTime) / 1000) + 1));
                    break;
                case 2:
                    timer.Text(String.Format("Night time left : {0}", ((gameData.nightLength+gameData.dayLength - Time.time + gameData.levelStartTime) / 1000) + 1));
                    break;
            
            }
            
            
        }
    }

    void AddStage() {
        if (!addedStage&& levelName == "Level1.tmx") { 
            stage.graphics.Clear(Color.Empty);
            stage.Text(String.Format("Stage : {0}", gameData.stage + 1));
            addedStage = true;
        }
    }

    public void SetLevel(String pLevel){
        levelName = pLevel;
    }
    void UpdateLocations(float x,float y) {
        hpBar.SetXY(x+10, y+10);
        scoreCounter.SetXY(x+475, y+10);
        stage.SetXY(x+250,y+8);
        timer.SetXY(x+240,y+33);
    }

}

