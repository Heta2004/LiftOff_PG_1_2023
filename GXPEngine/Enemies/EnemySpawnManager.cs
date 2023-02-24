using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GXPEngine;
using TiledMapParser;

public class EnemySpawnManager : AnimationSprite {


    GameData gameData;

    int lastSpawnTime = 0;
    int enemyNumber = 0;
    int[,] spawnChances = new int[10, 4] {

                                        {90,0,100,0},
                                        {70,80,100,0},
                                        {60,70,90,100},
                                        {50,70,90, 100},
                                        {25,25,25,25},
                                        {20,30,50, 100},
                                        {50,70,90, 100},
                                        {0,100,0, 0},
                                        {20,50,90, 100},
                                        {70,80,90, 100}

                                        };


    int maxEnemyPerStage = 1;
    int delayPerStage = 10000;
    int selectedSpawnPatern;
    bool halfedDelay = false;

    Player player;
    
    public EnemySpawnManager(string filename, int cols, int rows, TiledObject obj = null):base(filename,cols,rows) {
        
    }

    void Update() {
        if (gameData != null) {
            if (gameData.gameState == gameData.NIGHT&&!halfedDelay) {
                delayPerStage /= 2;
                halfedDelay= true;
            }

            if (gameData.stage > 4) {
                var rand = new Random();
                selectedSpawnPatern = rand.Next(4, 10);
            }
        }
        //Console.WriteLine(selectedSpawnPatern);
        SpawnEnemy();
    }

    void SpawnEnemy() {

        if (Time.time > delayPerStage +lastSpawnTime && enemyNumber < maxEnemyPerStage &&player!=null) {
            lastSpawnTime= Time.time;
            enemyNumber++;
            var rand = new Random();
            int randomNumber = rand.Next(1,101);
            if (randomNumber <= spawnChances[selectedSpawnPatern, 0])
            {
                EnemyMarker em = new EnemyMarker("standard", gameData, this, player);
                parent.AddChild(em);
                em.ChooseSpawnLocation();
            }
            else
                if (randomNumber <= spawnChances[selectedSpawnPatern, 1])
            {
                EnemyMarker em = new EnemyMarker("tank", gameData, this, player);
                parent.AddChild(em);
                em.ChooseSpawnLocation();
            }
            else
                if (randomNumber <= spawnChances[selectedSpawnPatern, 2])
            {
                EnemyMarker em = new EnemyMarker("shooter", gameData, this, player);
                parent.AddChild(em);
                em.ChooseSpawnLocation();
            }
            else {
                EnemyMarker em = new EnemyMarker("discGuy", gameData, this, player);
                parent.AddChild(em);
                em.ChooseSpawnLocation();
            }

        }


    }

    public void SetTarget(Player pPlayer) { 
        player=pPlayer;
    
    }
    public void DecreaseEnemies() { 
        enemyNumber--;
    }

    public void SetGameData(GameData pGameData)
    {
        gameData = pGameData;
        if (gameData.stage <= 4){
            selectedSpawnPatern = gameData.stage-1;
        }
        else{
            var rand = new Random();
            selectedSpawnPatern = rand.Next(4, 10);
        }
        maxEnemyPerStage=Math.Max((gameData.stage+1)*5,75);//maybe 3
        delayPerStage = Math.Max(2000-gameData.stage*100,600);

    }

}

