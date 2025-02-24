using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class SaveAndLoadController {
    private static readonly string _playerDataSavePath = Path.Combine(Application.persistentDataPath, "playerData.json");

    private static readonly string _encryptionKey = "F_3jr8{fge}12qad";

    #region Save
        public static void Save(LevelData levelData, string levelName) {
            string json = JsonUtility.ToJson(levelData, true);
            string encryptedJson = Encrypt(json);

            string levelDataSavePath = Path.Combine(Application.persistentDataPath, levelName + "Data.json");

            File.WriteAllText(levelDataSavePath, encryptedJson);
            Debug.Log($"[SaveAndLoadController] {levelName} data saved: {levelDataSavePath}");
        }

        public static void Save(PlayerData playerData) {
            string json = JsonUtility.ToJson(playerData, true);
            string encryptedJson = Encrypt(json);

            File.WriteAllText(_playerDataSavePath, encryptedJson);
            Debug.Log($"[SaveAndLoadController] Player data saved: {_playerDataSavePath}");
        }
    #endregion

    #region Load
        public static LevelData LoadLevelData(string levelName) {
            string levelDataSavePath = Path.Combine(Application.persistentDataPath, levelName + "Data.json");

            if (!File.Exists(levelDataSavePath)) {
                Debug.LogWarning($"[SaveAndLoadController] {levelName} data save file not found!");
                return new LevelData();
            } else {
                string encryptedJson = File.ReadAllText(levelDataSavePath);
                string json = Decrypt(encryptedJson);

                return JsonUtility.FromJson<LevelData>(json);
            }
        }

        public static PlayerData LoadPlayerData() {
            if (!File.Exists(_playerDataSavePath)) {
                Debug.LogWarning("[SaveAndLoadController] Player data save file not found!");

                return new PlayerData (
                    0,
                    BallSkinType.Default,
                    new List<SerializableSkin> {
                        new SerializableSkin(BallSkinType.Default, true)
                    }
                );
            } else {
                string encryptedJson = File.ReadAllText(_playerDataSavePath);
                string json = Decrypt(encryptedJson);

                return JsonUtility.FromJson<PlayerData>(json);
            }
        }
    #endregion

    #region Delete
        public static void DeleteSave(string saveName) {
            string levelDataSavePath = Path.Combine(Application.persistentDataPath, saveName + "Data.json");

            if (File.Exists(levelDataSavePath)) {
                File.Delete(levelDataSavePath);

                Debug.Log($"[SaveAndLoadController] {saveName} Save deleted");
            } else Debug.LogWarning($"[SaveAndLoadController] {saveName} data delete save file not found!");
        }
    #endregion

    #region Encryption
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
    #endregion
}