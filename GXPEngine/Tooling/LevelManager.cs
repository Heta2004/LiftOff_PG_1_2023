using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using GXPEngine;
using GXPEngine.Managers;
using TiledMapParser;

public class Level : GameObject{
    Player player;
    TiledLoader loader;
    string currentLevelName;
    Camera camera = new Camera(0, 0, 600, 450);
    GameData gameData;
    UI ui;
    long startTime;
    Pivot imageLayer = new Pivot();
    Pivot tileLayer=new Pivot();
    Pivot objectLayer=new Pivot();
    Pivot lateStuffToAdd= new Pivot();
    Pivot PlaceForUI= new Pivot();
    Sprite blackThing = new Sprite("black.png", false, false);
    float blackThingAlphaTarget = 0.65f;

    public Level(String filename, GameData pGameData)
    {
        AddChild(imageLayer);
        AddChild(tileLayer);
        AddChild(objectLayer);  
        AddChild(lateStuffToAdd);
        AddChild(PlaceForUI);
        blackThing.SetScaleXY(10, 10);
        lateStuffToAdd.AddChild(blackThing);
        blackThing.SetXY(-700, -700);
        blackThing.alpha = 0f;
        blackThing.blendMode = BlendMode.PREMULTIPLIED;

        gameData = pGameData;
        Console.WriteLine(filename);
        currentLevelName = filename;
        loader = new TiledLoader(filename);
        CreateLevel();
        startTime = Time.time;
        gameData.levelStartTime = Time.time;
    }
    void CreateLevel(bool includeImageLayers = true){


        gameData.changedLevel = true;
        //gameData.stage++;
        if (currentLevelName == "Level1.tmx"){
            gameData.stage++;
        }
        loader.rootObject = imageLayer;
        loader.addColliders = false;
        loader.autoInstance = true;
        loader.LoadImageLayers();
        loader.rootObject = tileLayer;
        loader.LoadTileLayers();
        loader.addColliders = true;
        loader.rootObject = objectLayer;
        if (currentLevelName != "Level.tmx") {

            loader.LoadObjectGroups();

        }
        else {
            LoadMainLevel();
        }

        if (currentLevelName != "mainMenu.tmx"&&currentLevelName!="end.tmx"){
            ui = new UI(gameData, camera);
            ui.SetLevel(currentLevelName);
            
            PlaceForUI.AddChild(ui);

        }

        player = FindObjectOfType<Player>();
        if (player == null) { 
            camera.SetXY(300,225);
            AddChild(camera); 
        }
        else{
            player.SetCamera(camera);
            player.SetGameData(gameData);
            player.SetUI(ui);
        }

        TutorialManager t=FindObjectOfType<TutorialManager>();
        if (t != null) {
            t.SetGameDataAndOtherStuff(gameData,camera,player); 
        }

        EnemySpawnManager e = FindObjectOfType<EnemySpawnManager>();
        if (e != null){
            e.SetTarget(player);
            e.SetGameData(gameData);
        }

        WeaponManager wm = FindObjectOfType<WeaponManager>();
        if (wm != null){
            wm.setCamera(camera);
            wm.SetGameData(gameData);
            wm.setTarget(player);
        }
        BuyButton[] b = FindObjectsOfType<BuyButton>();
        foreach (BuyButton bb in b)
            bb.SetGameData(gameData);

        ShopManager sm = FindObjectOfType<ShopManager>();
        if (sm != null){
            sm.SetGameData(gameData);
        }

        TeleporterManager tm = new TeleporterManager();
        Teleporter[] tp= FindObjectsOfType<Teleporter>();
        tm.SetTeleporterList(tp);
        foreach (Teleporter i in tp) {
            i.SetTeleportManager(tm);
        }
        
        
        

        WeaponPickUp wpu = new WeaponPickUp("Mosin_Old.png","mosin");
        objectLayer.AddChild(wpu);
        wpu.SetXY(700, 700);

    }

    void Update() {
        if (Input.GetKeyUp(Key.R)) {
            ((MyGame)game).reset = true;
            ((MyGame)game).LoadLevel("mainMenu.tmx");
        }
        if (currentLevelName != "mainMenu.tmx")
            if (Time.time - startTime > (float)gameData.dayLength / 3 * 2&& Time.time - startTime<=gameData.dayLength) {
                float targetChange=((float)gameData.dayLength/30000)* blackThingAlphaTarget;
                int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
                float finalChange = targetChange * deltaTimeClamped / 1000;
                blackThing.alpha = Math.Min(blackThing.alpha+finalChange, blackThingAlphaTarget);
            }

        if (Time.time - startTime >= gameData.dayLength)
            gameData.gameState = gameData.NIGHT;
        if (currentLevelName!= "mainMenu.tmx")
            if (Time.time - startTime > (float)gameData.dayLength + (float)gameData.nightLength / 3 * 2 && Time.time - startTime <= gameData.dayLength + gameData.nightLength){
                float targetChange = ((float)gameData.dayLength / 30000) * blackThingAlphaTarget;
                int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
                float finalChange = targetChange * deltaTimeClamped / 1000;
                blackThing.alpha = Math.Max(blackThing.alpha - finalChange, 0f);
            }



        if (gameData.stage != -1 && currentLevelName != "mainMenu.tmx")
            if (Time.time - startTime >= gameData.dayLength + gameData.nightLength){
                gameData.scoreMultiplier += gameData.scoreMultiplierIncrease;
                gameData.gameState=gameData.DAY;
                ((MyGame)game).LoadLevel("Level1.tmx");
            }

    }

    void LoadMainLevel() {
        // Shit 
        //SlowTile slowTile = new SlowTile("square.png", 1, 1, null);
        //objectLayer.AddChild(slowTile);
        //slowTile.SetXY(450, 450);

        //loader.LoadObjectGroups(0);
        //if (gameData.stage < 4)
        //    loader.LoadObjectGroups(gameData.stage + 1);
        //loader.LoadObjectGroups(5);
        //loader.LoadObjectGroups(6);
        //loader.LoadObjectGroups(7);




        loader.LoadObjectGroups();
    }


}
