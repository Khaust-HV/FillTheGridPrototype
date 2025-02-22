using System;

[Serializable]
public struct PlayerData {
    public int numberOfCoinsPlayer;

    public PlayerData(int numberOfCoinsPlayer) {
        this.numberOfCoinsPlayer = numberOfCoinsPlayer;
    }
}