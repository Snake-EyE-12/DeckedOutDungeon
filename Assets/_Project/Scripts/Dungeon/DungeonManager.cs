using System.Collections;
using System.Collections.Generic;
using Guymon.DesignPatterns;
using UnityEngine;

public class DungeonManager : Singleton<DungeonManager>
{
    [SerializeField] private PlayerInfoBlock playerData;
    [SerializeField][Range(0,1)] private float overallNoiseGenerationChance;
    [SerializeField][Range(0,1)] private float overallHazardGenerationChance;
    [SerializeField][Range(0,1)] private float overallTrapGenerationChance;
    [SerializeField][Range(0,1)] private float overallFrostGenerationChance;
    [SerializeField][Range(0,1)] private float overallTreasureGenerationChance;
    [SerializeField][Range(0,1)] private float overallSandGenerationChance;
    [SerializeField][Min(0)] private float trapActivationInterval;
    private float elapsedTrapTime;




    private void Update() {
        updateTrapInterval();
    }









    private void updateTrapInterval() {
        if(elapsedTrapTime <= 0) {
            generateTrap();
            elapsedTrapTime = trapActivationInterval;
            return;
        }
        elapsedTrapTime -= Time.deltaTime;
    }
    public int getTrapDisplayTime() {
        return (int)elapsedTrapTime;
    }








    public void generateNoise(bool manatory = false) {
        if(Random.Range(0f, 1f) < overallNoiseGenerationChance || manatory) EventHandler.Invoke("DungeonModification/Noise");
    }
    public void generateHazard(bool manatory = false) {
        if(Random.Range(0f, 1f) < overallHazardGenerationChance || manatory) EventHandler.Invoke("DungeonModification/Hazard");
    }
    public void generateTrap(bool manatory = false) {
        if(Random.Range(0f, 1f) < overallTrapGenerationChance || manatory) EventHandler.Invoke("DungeonModification/Trap");
    }
    public void generateFrost(bool manatory = false) {
        if(Random.Range(0f, 1f) < overallFrostGenerationChance || manatory) EventHandler.Invoke("ItemProduction/Frost");
    }
    public void generateTreasure(bool manatory = false) {
        if(Random.Range(0f, 1f) < overallTreasureGenerationChance || manatory) EventHandler.Invoke("ItemProduction/Treasure");
    }
    public void generateSand(bool manatory = false) {
        if(Random.Range(0f, 1f) < overallSandGenerationChance || manatory) EventHandler.Invoke("ItemProduction/Sand");
    }
}
