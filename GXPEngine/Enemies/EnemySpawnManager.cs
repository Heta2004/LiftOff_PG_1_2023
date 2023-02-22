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
    int[,] spawnChances = new int[10, 3] {

                                        {100,0,0},
                                        {90,100,0},
                                        {70,90,100},
                                        {65,85,100},
                                        {60,80,100},
                                        {60,80,100},
                                        {60,80,100},
                                        {60,80,100},
                                        {60,80,100},
                                        {60,80,100}

                                        };


    int maxEnemyPerStage = 1;
    int delayPerStage = 10000;
    int selectedSpawnPatern;

    Player player;
    
    public EnemySpawnManager(string filename, int cols, int rows, TiledObject obj = null):base(filename,cols,rows) {
        
    }

    void Update() {
        Console.WriteLine(selectedSpawnPatern);
        SpawnEnemy();
    }

    void SpawnEnemy() {

        if (Time.time > delayPerStage +lastSpawnTime && enemyNumber < maxEnemyPerStage &&player!=null) {
            lastSpawnTime= Time.time;
            enemyNumber++;
            var rand = new Random();
            int randomNumber = rand.Next(1,101);
            if (randomNumber <= spawnChances[selectedSpawnPatern, 0]){
                EnemyMarker em = new EnemyMarker("standard",gameData,this,player);
                parent.AddChild(em);
            }
            else
                if (randomNumber <= spawnChances[selectedSpawnPatern, 1]){
                EnemyMarker em = new EnemyMarker("tank", gameData, this, player);
                parent.AddChild(em);
            }
                else {
                EnemyMarker em = new EnemyMarker("shooter", gameData, this, player);
                parent.AddChild(em);
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

