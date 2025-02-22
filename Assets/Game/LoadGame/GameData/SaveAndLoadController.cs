using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SaveAndLoadController {
    private static readonly string _levelDataSavePath = Path.Combine(Application.persistentDataPath, "levelData.json");
    private static readonly string _playerDataSavePath = Path.Combine(Application.persistentDataPath, "playerData.json");

    private static readonly string _encryptionKey = "F_3jr8{fge}12qad";

    #region Save
        public static void Save(LevelData levelData) {
            string json = JsonUtility.ToJson(levelData, true);
            string encryptedJson = Encrypt(json);

            File.WriteAllText(_levelDataSavePath, encryptedJson);
            Debug.Log($"[SaveAndLoadController] Level data saved: {_levelDataSavePath}");
        }

        public static void Save(PlayerData playerData) {
            string json = JsonUtility.ToJson(playerData, true);
            string encryptedJson = Encrypt(json);

            File.WriteAllText(_playerDataSavePath, encryptedJson);
            Debug.Log($"[SaveAndLoadController] Player data saved: {_playerDataSavePath}");
        }
    #endregion

    #region Load
        public static LevelData LoadLevelData() {
            if (!File.Exists(_levelDataSavePath)) {
                Debug.LogWarning("[SaveAndLoadController] Level data save file not found!");
                return new LevelData();
            } else {
                string encryptedJson = File.ReadAllText(_levelDataSavePath);
                string json = Decrypt(encryptedJson);

                return JsonUtility.FromJson<LevelData>(json);
            }
        }

        public static PlayerData LoadPlayerData() {
            if (!File.Exists(_playerDataSavePath)) {
                Debug.LogWarning("[SaveAndLoadController] Player data save file not found!");
                return new PlayerData();
            } else {
                string encryptedJson = File.ReadAllText(_playerDataSavePath);
                string json = Decrypt(encryptedJson);

                return JsonUtility.FromJson<PlayerData>(json);
            }
        }
    #endregion

    // Encryption
    private static string Encrypt(string plainText) {
        using (Aes aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.IV = new byte[16];

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV)) {
                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    // Decryption
    private static string Decrypt(string encryptedText) {
        using (Aes aes = Aes.Create()) {
            aes.Key = Encoding.UTF8.GetBytes(_encryptionKey);
            aes.IV = new byte[16];

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV)) {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}