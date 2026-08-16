using System;
using System.Collections.Generic;
using System.Globalization;
using Formici.Core.Localization;
using Formici.Core.Settings;
using Formici.ScreenManagers;
using Formici.Screens;
using Microsoft.Xna.Framework;

namespace Formici.Core
{
    /// <summary>
    /// Main entry point class for Formici MonoGame application.
    /// Manages window initialization, settings, services, and screen transitions.
    /// </summary>
    public class FormiciGame : Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private readonly ScreenManager screenManager;
        private readonly SettingsManager<FormiciSettings> settingsManager;

        public static readonly bool IsMobile = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();
        public static readonly bool IsDesktop = OperatingSystem.IsMacOS() || OperatingSystem.IsLinux() || OperatingSystem.IsWindows();

        public FormiciGame()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Services.AddService(typeof(GraphicsDeviceManager), graphicsDeviceManager);

            ISettingsStorage storage = new DesktopSettingsStorage();
            graphicsDeviceManager.IsFullScreen = false;
            graphicsDeviceManager.PreferredBackBufferWidth = 1280;
            graphicsDeviceManager.PreferredBackBufferHeight = 768;
            IsMouseVisible = true;

            settingsManager = new SettingsManager<FormiciSettings>(storage);
            Services.AddService(typeof(SettingsManager<FormiciSettings>), settingsManager);

            Content.RootDirectory = "Content";
            graphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
        }

        protected override void Initialize()
        {
            base.Initialize();

            List<CultureInfo> cultures = LocalizationManager.GetSupportedCultures();
            var languages = new List<CultureInfo>();
            for (int i = 0; i < cultures.Count; i++)
            {
                languages.Add(cultures[i]);
            }
            var selectedLanguage = languages[settingsManager.Settings.Language].Name;
            LocalizationManager.SetCulture(selectedLanguage);

            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }
    }
}