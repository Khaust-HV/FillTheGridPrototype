using System.Collections.Generic;
using System.Linq;
using GameConfigs;
using UnityEngine;
using Zenject;

namespace Managers {
    public sealed class MainMenuPlayerManager : IControlPlayerManagerInMainMenu {
        private int _numberOfCoinsPlayer;
        private BallSkinType _currentBallSkinType;
        private List<SerializableSkin> _availableSkins;

        private BallSkinType[] _allSkinsBall;
        private int _currentSelectedSkinBallIndex;

        #region DI
            private BallAndCellConfigs _ballAndCellConfigs;
            private IControlTheBallModel _iControlTheBallModel;
            private VisualEffectsConfigs _visualEffectsConfigs;
            private IControlUIMainMenu _iControlUIMainMenu;
        #endregion

        [Inject]
        private void Construct (
            BallAndCellConfigs ballAndCellConfigs, 
            IControlTheBallModel iControlTheBallModel,
            VisualEffectsConfigs visualEffectsConfigs,
            IControlUIMainMenu iControlUIMainMenu
            ) {
            // Set DI
            _ballAndCellConfigs = ballAndCellConfigs;
            _iControlTheBallModel = iControlTheBallModel;
            _visualEffectsConfigs = visualEffectsConfigs;
            _iControlUIMainMenu = iControlUIMainMenu;

            // Set configurations
            _allSkinsBall = _ballAndCellConfigs.AllSkinsBall;
        }

        public void LoadPlayerData() {
            PlayerData playerData = SaveAndLoadController.LoadPlayerData();

            _numberOfCoinsPlayer = playerData.numberOfCoinsPlayer;
            _currentBallSkinType = playerData.currentBallSkinType;
            _availableSkins = playerData.availableSkins;

            _iControlUIMainMenu.CoinCountUpdate(_numberOfCoinsPlayer);
        }

        private void SavePlayerProgress() {
            PlayerData playerData = new PlayerData (
                _numberOfCoinsPlayer,
                _currentBallSkinType,
                _availableSkins
            );

            SaveAndLoadController.Save(playerData);
        }

        public bool BuyCurrentBallSkin() {
            BallSkinType desiredSkin = _allSkinsBall[_currentSelectedSkinBallIndex];

            if (_availableSkins.Any(p => p.SkinType == desiredSkin)) return false;

            int skinCost = desiredSkin switch {
                BallSkinType.Default => 0,
                BallSkinType.Green => _ballAndCellConfigs.GreenSkinCost,
                BallSkinType.Orange => _ballAndCellConfigs.OrangeSkinCost,
                _ => throw new System.Exception("The skin was not found")
            };

            if (_numberOfCoinsPlayer < skinCost) return false;

            _numberOfCoinsPlayer -= skinCost;

            _iControlUIMainMenu.CoinCountUpdate(_numberOfCoinsPlayer);

            _availableSkins.Add(new SerializableSkin(desiredSkin, true));

            SavePlayerProgress();

            return true;
        }

        public bool SelectCurrentBallSkin() {
            BallSkinType desiredSkin = _allSkinsBall[_currentSelectedSkinBallIndex];

            if (_currentBallSkinType == desiredSkin) return false;

            var skin = _availableSkins.Find(p => p.SkinType == desiredSkin);
            if (skin.Equals(default(SerializableSkin)) || !skin.IsUnlocked) return false;

            _currentBallSkinType = desiredSkin;

            SavePlayerProgress();

            return true;
        }

        public bool ShowNextBallSkin(int indexDirection) {
            int checkingTheIndex = _currentSelectedSkinBallIndex + indexDirection;

            if (checkingTheIndex < 0 || checkingTheIndex >= _allSkinsBall.Length) return false;

            BallSkinType desiredSkin = _allSkinsBall[checkingTheIndex];

            Color color;
            int skinCost;

            switch (desiredSkin) {
                case BallSkinType.Default:
                    color = _visualEffectsConfigs.DefaultSkinColor;
                    skinCost = 0;
                break;

                case BallSkinType.Green:
                    color = _visualEffectsConfigs.GreenSkinColor;
                    skinCost = _ballAndCellConfigs.GreenSkinCost;
                break;

                case BallSkinType.Orange:
                    color = _visualEffectsConfigs.OrangeSkinColor;
                    skinCost = _ballAndCellConfigs.OrangeSkinCost;
                break;

                default:
                    throw new System.Exception("The skin was not found");
                // break;
            }

            _iControlTheBallModel.SetBallColor(color);

            _iControlTheBallModel.SpawnAnimationEnable();

            _currentSelectedSkinBallIndex = checkingTheIndex;

            _iControlUIMainMenu.BallSkinPriceCountUpdate(skinCost);

            return true;
        }

        public void ShowFirstBallSkin() {
            _iControlTheBallModel.SetBallColor(_visualEffectsConfigs.DefaultSkinColor);

            _iControlTheBallModel.SpawnAnimationEnable();

            _currentSelectedSkinBallIndex = 0;

            _iControlUIMainMenu.BallSkinPriceCountUpdate(0);
        }

        public void HideBallModel() {
            _iControlTheBallModel.HideBallModel();
        }
    }
}

public interface IControlPlayerManagerInMainMenu {
    public void LoadPlayerData();
    public bool BuyCurrentBallSkin();
    public bool SelectCurrentBallSkin();
    public bool ShowNextBallSkin(int indexDirection);
    public void ShowFirstBallSkin();
    public void HideBallModel();
}

public enum BallSkinType {
    Default,
    Green,
    Orange
}