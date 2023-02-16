using System;                                    
using GXPEngine;                                
using System.Drawing;                           
using System.Linq.Expressions;
using System.Security.Policy;
using System.Collections.Generic;
// i want to go home
public class MyGame : Game {

    string startLevel = "mainMenu.tmx";
    //string startLevel = "Level1.tmx";
    string nextLevel;
    GameData gameData=new GameData();
    public bool reset = false;
    Sound music = new Sound("song.mp3",true,true);
    public MyGame() : base(600, 450, false,true,1200,900,true) {
        game.RenderMain = false;
        SoundChannel soundChannel = music.Play(false,0,0.30f);
        game.OnAfterStep += CheckLoadLevel; 
        AddChild(gameData);
        LoadLevel(startLevel);

    }

    void DestroyAll()
    {
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children){
            child.Destroy();
        }
    }

    public void LoadLevel(string filename){
        nextLevel = filename;
    }

    void CheckLoadLevel() { 
        if (nextLevel!= null) {
            DestroyAll();
            AddChild(new Level(nextLevel,gameData));
            if (reset) {
                gameData.Reset();
                reset = false;
            }

            nextLevel= null;
        }
    
    
    }

    static void Main() {
		new MyGame().Start();                   
	}
}