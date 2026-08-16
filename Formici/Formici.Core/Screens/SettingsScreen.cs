using System.Collections.Generic;
using System.Globalization;
using Formici.Core.Localization;
using Formici.Core.Settings;
using Formici.ScreenManagers;
using Microsoft.Xna.Framework;

namespace Formici.Screens
{
    /// <summary>
    /// Settings screen allowing the user to configure display mode and language preferences.
    /// </summary>
    class SettingsScreen : MenuScreen
    {
        private readonly MenuEntry fullscreenMenuEntry;
        private readonly MenuEntry languageMenuEntry;
        private readonly MenuEntry backMenuEntry;
        private static List<CultureInfo> languages;
        private static int currentLanguage = 0;
        private GraphicsDeviceManager gdm;
        private SettingsManager<FormiciSettings> settingsManager;

        public SettingsScreen() : base(Resources.Settings)
        {
            List<CultureInfo> cultures = LocalizationManager.GetSupportedCultures();
            languages = new List<CultureInfo>();
            for (int i = 0; i < cultures.Count; i++)
            {
                languages.Add(cultures[i]);
            }

            fullscreenMenuEntry = new MenuEntry(string.Empty);
            languageMenuEntry = new MenuEntry(string.Empty);
            backMenuEntry = new MenuEntry(string.Empty);

            fullscreenMenuEntry.Selected += FullScreenMenuEntrySelected;
            languageMenuEntry.Selected += LanguageMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;

            MenuEntries.Add(fullscreenMenuEntry);
            MenuEntries.Add(languageMenuEntry);
            MenuEntries.Add(backMenuEntry);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            gdm ??= ScreenManager.Game.Services.GetService<GraphicsDeviceManager>();
            settingsManager ??= ScreenManager.Game.Services.GetService<SettingsManager<FormiciSettings>>();

            if (settingsManager != null)
            {
                settingsManager.Settings.PropertyChanged += (s, e) =>
                {
                    SetLanguageText();
                    settingsManager.Save();
                };

                currentLanguage = settingsManager.Settings.Language;
                gdm.IsFullScreen = settingsManager.Settings.FullScreen;
            }

            SetLanguageText();
        }

        private void SetLanguageText()
        {
            fullscreenMenuEntry.Text = string.Format(Resources.DisplayMode, gdm.IsFullScreen ? Resources.FullScreen : Resources.Windowed);

            var selectedLanguage = languages[currentLanguage].DisplayName;
            if (selectedLanguage.Contains("Invariant"))
            {
                selectedLanguage = Resources.English;
            }
            languageMenuEntry.Text = Resources.Language + selectedLanguage;
            backMenuEntry.Text = Resources.Back;
            Title = Resources.Settings;
        }

        private void FullScreenMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            gdm.ToggleFullScreen();
            settingsManager.Settings.FullScreen = gdm.IsFullScreen;
        }

        private void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentLanguage = (currentLanguage + 1) % languages.Count;

            var selectedLanguage = languages[currentLanguage].Name;
            LocalizationManager.SetCulture(selectedLanguage);

            settingsManager.Settings.Language = currentLanguage;
        }
    }
}