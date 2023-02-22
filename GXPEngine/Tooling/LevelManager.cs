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
    Camera camera = new Camera(0, 0, 683, 384);
    
    GameData gameData;
    UI ui;
    long startTime;
    Pivot imageLayer = new Pivot();
    Pivot tileLayer=new Pivot();
    Pivot objectLayer=new Pivot();
    Pivot lateStuffToAdd= new Pivot();
    Pivot PlaceForUI= new Pivot();
    Sprite fogOfWar = new Sprite("fogOfWar.png",false,false);
    Sprite blackThing = new Sprite("black.png", false, false);
    float blackThingAlphaTarget = 0.25f;
    float fogOfWarAlphaTarget = 0.25f;
    float fogScale = 8f;

    Minotaur minotaur;
    public bool continueLevel=false;

    public Level(String filename, GameData pGameData)
    {
        game.OnAfterStep += UpdateCameraLocation;
        camera.scale= 1f;
        AddChild(imageLayer);
        AddChild(tileLayer);
        AddChild(objectLayer);  
        AddChild(lateStuffToAdd);
        AddChild(PlaceForUI);

        fogOfWar.SetOrigin(fogOfWar.width / 2, fogOfWar.height / 2);;
        fogOfWar.SetScaleXY(8f, 8f);

        lateStuffToAdd.AddChild(fogOfWar);
        fogOfWar.alpha = 0;

        blackThing.SetScaleXY(10, 10);
        lateStuffToAdd.AddChild(blackThing);
        blackThing.SetXY(-700, -700);
        blackThing.alpha = 0f;

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
        if (currentLevelName!= "mainMenu.tmx"&&currentLevelName!= "Shop.tmx"&&currentLevelName!= "ShopAndShit.tmx")
        {
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
        loader.LoadObjectGroups();

        if (currentLevelName != "mainMenu.tmx"&&currentLevelName!="end.tmx"){
            ui = new UI(gameData, camera);
            ui.SetLevel(currentLevelName);
            
            PlaceForUI.AddChild(ui);

        }

        player = FindObjectOfType<Player>();
        if (player == null) { 
            camera.SetXY(341,192);
            AddChild(camera); 
        }
        else{
            objectLayer.AddChild(camera);
            player.SetCamera(camera);
            player.SetGameData(gameData);
            player.SetUI(ui);
        }

        if (currentLevelName == "MinotaurLevel.tmx")
        {

            minotaur = new Minotaur(player, gameData);
            minotaur.SetScaleXY(2);
            objectLayer.AddChild(minotaur);
            minotaur.SetXY(380, 360);

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
        int number = 0;
        foreach (Teleporter i in tp) {
            i.SetTeleportManager(tm);
            i.SetPlayer(player);
            i.SetNumber(number++);
        }
        
        //WeaponPickUp wpu = new WeaponPickUp("Mosin_Old.png","mosin");
        //objectLayer.AddChild(wpu);
        //wpu.SetXY(700, 700);

        Button[] button=FindObjectsOfType<Button>();
        foreach (Button i in button) { 
            i.SetGameData(gameData);
        }

        DestructibleWall[] ds= FindObjectsOfType<DestructibleWall>();
        foreach (DestructibleWall d in ds)
        {
            d.SetMinotaur(minotaur);
        }

        if (currentLevelName=="Level1.tmx") {
            FlameThrowerPickUp pickup = new FlameThrowerPickUp();
            pickup.SetXY(700,700);
            objectLayer.AddChild(pickup);
        }
    }

    void Update() {
        //Console.WriteLine( "stage: {0}",gameData.stage);
        Console.WriteLine();
        if (player != null)
            fogOfWar.SetXY(player.x,player.y);
        
        if (Input.GetKeyUp(Key.R)) {
            ((MyGame)game).reset = true;
            ((MyGame)game).LoadLevel("mainMenu.tmx");
        }   
        if (currentLevelName == "Level1.tmx"&&gameData.gameState==gameData.DAY)
            if (Time.time - startTime > (float)gameData.dayLength / 4 * 3) {
                float targetChange= blackThingAlphaTarget/((float)gameData.dayLength/4000);
                float targetChangeFog = fogOfWarAlphaTarget/((float)gameData.dayLength / 4000);
                float targetScaleChange = 3 / (((float)gameData.dayLength) / 4000);


                int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
                float finalChange = targetChange * deltaTimeClamped / 1000;
                float finalChangeFog = targetChangeFog * deltaTimeClamped / 1000;
                float finalScaleChange= targetScaleChange * deltaTimeClamped / 1000;
                blackThing.alpha = Math.Min(blackThing.alpha+finalChange, blackThingAlphaTarget);
                fogOfWar.alpha = Math.Min(fogOfWar.alpha + finalChangeFog, fogOfWarAlphaTarget);
                fogScale = Math.Max(3, fogScale - finalScaleChange);
                fogOfWar.SetScaleXY(fogScale);

            }

        if (Time.time - startTime >= gameData.dayLength)
            gameData.gameState = gameData.NIGHT;
        if (currentLevelName == "Level1.tmx" && gameData.gameState == gameData.NIGHT)
        {
            if (Time.time - startTime >= (float)gameData.dayLength + (float)gameData.nightLength / 5*4){
                float targetBlackness = 0.25f;
                float targetFogness = 0.25f;

                float targetChange = (1f-targetBlackness) / ((float)gameData.dayLength / 5000);
                float targetChangeFog = (1f-targetFogness) / ((float)gameData.dayLength / 5000);
                float targetScaleChange = 3 / (((float)gameData.dayLength) / 5000);

                int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
                float finalChange = targetChange * deltaTimeClamped / 1000;
                float finalChangeFog = targetChangeFog * deltaTimeClamped / 1000;
                float finalScaleChange = targetScaleChange * deltaTimeClamped / 1000;
                blackThing.alpha = Math.Max(blackThing.alpha - finalChange, targetBlackness);
                fogOfWar.alpha = Math.Max(fogOfWar.alpha - finalChangeFog, targetFogness);
                fogScale = Math.Min(8, fogScale + finalScaleChange);
                fogOfWar.SetScaleXY(fogScale);
            }

            if (Time.time - startTime < (float)gameData.dayLength + (float)gameData.nightLength / 100*15) {
                float targetBlackness = 0.7f;
                float targetFogness = 1f;


                float targetChange = (targetBlackness - blackThingAlphaTarget) / ((float)gameData.dayLength / (100f/15*1000));
                float targetChangeFog = (targetFogness - fogOfWarAlphaTarget) / ((float)gameData.dayLength / (100f / 15 * 1000));
                float targetScaleChange = 2 / (((float)gameData.dayLength) / (100f / 15 * 1000));


                int deltaTimeClamped = Math.Min(Time.deltaTime, 40);
                float finalChange = targetChange * deltaTimeClamped / 1000;
                float finalChangeFog = targetChangeFog * deltaTimeClamped / 1000;
                float finalScaleChange = targetScaleChange * deltaTimeClamped / 1000;
                blackThing.alpha = Math.Min(blackThing.alpha + finalChange, targetBlackness);
                fogOfWar.alpha = Math.Min(fogOfWar.alpha + finalChangeFog, targetFogness);
                fogScale = Math.Max(3, fogScale - finalScaleChange);
                fogOfWar.SetScaleXY(fogScale);
            }
        }
        if (currentLevelName== "mainMenu.tmx") {
            ChooseRandomLevel();
        }

        if (gameData.stage != -1 && currentLevelName != "mainMenu.tmx"&&currentLevelName!= "MinotaurLevel.tmx")
            if (Time.time - startTime >= gameData.dayLength + gameData.nightLength){
                gameData.scoreMultiplier += gameData.scoreMultiplierIncrease;
                gameData.gameState=gameData.DAY;
                if (gameData.stage % 5 == 4){
                    gameData.nextLevel="MinotaurLevel.tmx";
                }
                else {
                    //gameData.nextLevel = "Level1.tmx";
                    ChooseRandomLevel();
                }
                ((MyGame)game).LoadLevel("ShopAndShit.tmx");
            }
        if (currentLevelName == "MinotaurLevel.tmx") {
            //gameData.nextLevel = "Level1.tmx";
            ChooseRandomLevel();
            if (continueLevel)
                ((MyGame)game).LoadLevel("ShopAndShit.tmx");
        }
    }

    void UpdateCameraLocation() {
        if (player!=null)
            camera.SetXY(player.x,player.y);
    }


    void ChooseRandomLevel() {
        if (gameData.stage <= 4) {
            switch (gameData.stage) {
                case 0:
                    gameData.nextLevel = "Level1.tmx";
                    gameData.spawnXLeft =256;
                    gameData.spawnYLeft = 288;
                    gameData.spawnXRight = 960;
                    gameData.spawnYRight = 992;
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:

                    break;
            }
        }
    
    }

}
