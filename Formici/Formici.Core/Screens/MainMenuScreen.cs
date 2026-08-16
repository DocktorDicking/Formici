using System;
using Formici.Core;
using Formici.Core.Inputs;
using Formici.Core.Localization;
using Formici.Core.Screens;
using Formici.Core.Settings;
using Formici.ScreenManagers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Formici.Screens
{
    /// <summary>
    /// Main menu screen for Formici top-down ant colony game.
    /// </summary>
    class MainMenuScreen : MenuScreen
    {
        private ContentManager content;
        private SettingsManager<FormiciSettings> settingsManager;
        private readonly MenuEntry playMenuEntry;
        private readonly MenuEntry settingsMenuEntry;
        private readonly MenuEntry aboutMenuEntry;
        private readonly MenuEntry exitMenuEntry;

        public MainMenuScreen() : base("Formici")
        {
            playMenuEntry = new MenuEntry(Resources.Play);
            settingsMenuEntry = new MenuEntry(Resources.Settings);
            aboutMenuEntry = new MenuEntry(Resources.About);
            exitMenuEntry = new MenuEntry(Resources.Exit);

            playMenuEntry.Selected += PlayMenuEntrySelected;
            settingsMenuEntry.Selected += SettingsMenuEntrySelected;
            aboutMenuEntry.Selected += AboutMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playMenuEntry);
            MenuEntries.Add(settingsMenuEntry);
            MenuEntries.Add(aboutMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        private void SetLanguageText()
        {
            aboutMenuEntry.Text = Resources.About;
            playMenuEntry.Text = Resources.Play;
            settingsMenuEntry.Text = Resources.Settings;
            exitMenuEntry.Text = Resources.Exit;
            Title = "Formici";
        }

        public override void LoadContent()
        {
            base.LoadContent();

            content ??= new ContentManager(ScreenManager.Game.Services, "Content");
            settingsManager ??= ScreenManager.Game.Services.GetService<SettingsManager<FormiciSettings>>();

            if (settingsManager != null)
            {
                settingsManager.Settings.PropertyChanged += (s, e) => SetLanguageText();
            }

            SetLanguageText();
        }

        public override void UnloadContent()
        {
            content?.Unload();
        }

        private void PlayMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }

        private void SettingsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new SettingsScreen(), e.PlayerIndex);
        }

        private void AboutMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new AboutScreen(), e.PlayerIndex);
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            string message = Resources.ExitQuestion;
            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);
            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;
            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        private void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }
    }
}