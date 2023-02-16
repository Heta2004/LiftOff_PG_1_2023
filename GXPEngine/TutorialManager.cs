using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class TutorialManager : AnimationSprite {

    Player player;
    GameData gameData;
    Camera camera;

    int stage = 1;

    bool addedFirstMessage = false;
    bool gaveWeapon = false;
    bool addedEnemies = false;
    bool addedLastMessage = false;
    

    int startTime = Time.time;
    int firstMessageDelay = 100;

    Sprite message1Move = new Sprite("message1.png", false, false);
    Sprite message2 = new Sprite("message2.png",false,false);
    Sprite message3 = new Sprite("message3.png", false, false);
    Sprite message4 = new Sprite("message4.png", false, false);

    Wall wall1 = new Wall("wall.png", 1, 1);
    Wall wall2 = new Wall("wall.png", 1, 1);

    TrainingDummy trainingDummy;

    List<Enemy> enemies = new List<Enemy> { };

    public TutorialManager(string filename, int cols, int rows, TiledObject obj = null) : base(filename, cols, rows) {


    }

    void Update() {

        switch (stage) {
            case 1:
                AddFirstMessage();
                CheckPlayerMovement();
                break;
            case 2:
                GiveWeapon();
                CheckDummyForHp();
                break;
            case 3:
                SpawnEnemies();
                CheckEnemiesAlive();
                break;
            case 4:
                SpawnLastMessage();
                break;

        }

    }

    void AddFirstMessage() {
        if (Time.time - startTime > firstMessageDelay && !addedFirstMessage) {
            player.AddChild(message1Move);
            message1Move.SetXY(-105, -150);
            addedFirstMessage = true;
        }
    }

    void CheckPlayerMovement() {
        //compilation error if adding &&
        if (addedFirstMessage)
            if (player.CheckIsMoving()) {
                wall1.LateDestroy();
                message1Move.LateDestroy();
                stage++;
            }


    }

    void GiveWeapon() {
        if (player.x > 500 && !gaveWeapon) {//450
            player.AddChild(message2);
            message2.SetXY(-180, -160);
            gameData.gunArray.Add(1);
            WeaponManager wm = new WeaponManager("square.png", 1, 1);
            parent.AddChild(wm);
            wm.setCamera(camera);
            wm.setTarget(player);
            wm.SetGameData(gameData);

            wm.SetXY(-1000, -1000);
            gaveWeapon = true;
            trainingDummy = new TrainingDummy(player);
            parent.AddChild(trainingDummy);
            trainingDummy.SetXY(750, 120);
        }

    }

    void CheckDummyForHp() {
        if (gaveWeapon && trainingDummy.CheckIfLowHp()) {
            message2.LateDestroy();

            parent.RemoveChild(wall2);
            stage++;
        }
    }

    void SpawnEnemies(){
        if (player.x > 1025&!addedEnemies) {
            player.AddChild(message3);
            message3.SetXY(-180, -150);

            parent.AddChild(wall2);
            enemies.Add(new StandardEnemy(player));
            enemies.Add(new StandardEnemy(player));
            enemies.Add(new StandardEnemy(player));
            parent.AddChild(enemies[0]);
            parent.AddChild(enemies[1]);
            parent.AddChild(enemies[2]);
            enemies[0].SetXY(1295, 90);
            enemies[1].SetXY(1295, 120);
            enemies[2].SetXY(1295, 150);
            addedEnemies = true;
            

        }

    }

    void CheckEnemiesAlive() {
        if (addedEnemies) {
            int enemiesAlive = 0;
            for (int i=0;i<enemies.Count;i++){
                if (parent.HasChild(enemies[i]))
                    enemiesAlive++;

            }
            if (enemiesAlive == 0) {
                message3.LateDestroy();
                stage++;
            }
        
        }

    }

    void SpawnLastMessage(){
        if (!addedLastMessage) { 
            player.AddChild(message4);
            message4.SetXY(-180, -150);
            addedLastMessage= true;
        }
    }


    void AddStructures() {
        parent.AddChild(wall1);
        wall1.SetXY(444,  65);
        parent.AddChild(wall2);
        wall2.SetXY(860 , 65);

    }

    public void SetGameDataAndOtherStuff(GameData pGameData,Camera pCamera,Player pPlayer) {
        camera = pCamera;
        gameData = pGameData;
        player = pPlayer;
        AddStructures();
        gameData.gunArray.RemoveAt(0);
    }


}

