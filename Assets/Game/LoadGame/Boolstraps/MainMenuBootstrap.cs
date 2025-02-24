using UnityEngine;
using Zenject;

public sealed class MainMenuBootstrap : MonoBehaviour { // Entry point pattern
    #region DI
        private IControlUIMainMenu _iControlUIMainMenu;
        private IControlBlackWindowView _iControlBlackWindowView;
        private IControlPlayerManagerInMainMenu _iControlPlayerManagerInMainMenu;
    #endregion

    [Inject]
    private void Construct (
        IControlUIMainMenu iControlUIMainMenu, 
        IControlBlackWindowView iControlBlackWindowView,
        IControlPlayerManagerInMainMenu iControlPlayerManagerInMainMenu
        ) {
        // Set DI
        _iControlUIMainMenu = iControlUIMainMenu;
        _iControlBlackWindowView = iControlBlackWindowView;
        _iControlPlayerManagerInMainMenu = iControlPlayerManagerInMainMenu;
    }

    private void Start() {
        // SaveAndLoadController.DeleteSave("player");
        // SaveAndLoadController.DeleteSave("firstLevel");
        // SaveAndLoadController.DeleteSave("secondLevel");
        // SaveAndLoadController.DeleteSave("thirdLevel");

        _iControlPlayerManagerInMainMenu.LoadPlayerData();

        _iControlBlackWindowView.SetBlackWindowActive(false);

        // Enable root menu
        _iControlUIMainMenu.RootMenuEnable();
    }
}