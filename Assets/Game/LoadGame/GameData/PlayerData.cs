using System;
using System.Collections.Generic;

[Serializable]
public struct PlayerData {
    public int numberOfCoinsPlayer;
    public BallSkinType currentBallSkinType;
    public List<SerializableSkin> availableSkins;

    public PlayerData (
        int numberOfCoinsPlayer,
        BallSkinType currentBallSkinType,
        List<SerializableSkin> availableSkins
        ) {
        this.numberOfCoinsPlayer = numberOfCoinsPlayer;
        this.currentBallSkinType = currentBallSkinType;
        this.availableSkins = availableSkins;
    }
}

[Serializable]
public struct SerializableSkin {
    public BallSkinType SkinType;
    public bool IsUnlocked;

    public SerializableSkin(BallSkinType skinType, bool isUnlocked) {
        SkinType = skinType;
        IsUnlocked = isUnlocked;
    }
}