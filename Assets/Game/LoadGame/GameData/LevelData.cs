using System;
using UnityEngine;

[Serializable]
public struct LevelData {
    public int[] paintedCellsIndexStorage;
    public int[] coinsOnCellsIndexStorage;
    public int coinNumberOnLevel;
    public int numberOfCoinsCollected;

    public Vector3 ballPosition;
    public bool isAbilityToReturn;
    public Vector3 direction;
    public bool isCoinTaked;

    public LevelData (
        int[] paintedCellsIndexStorage,
        int[] coinsOnCellsIndexStorage,
        int coinNumberOnLevel,
        int numberOfCoinsCollected,
        Vector3 ballPosition,
        bool isAbilityToReturn,
        Vector3 direction,
        bool isCoinTaked
    ){
        this.paintedCellsIndexStorage = paintedCellsIndexStorage;
        this.coinsOnCellsIndexStorage = coinsOnCellsIndexStorage;
        this.coinNumberOnLevel = coinNumberOnLevel;
        this.numberOfCoinsCollected = numberOfCoinsCollected;
        this.ballPosition = ballPosition;
        this.isAbilityToReturn = isAbilityToReturn;
        this.direction = direction;
        this.isCoinTaked = isCoinTaked;
    }
}