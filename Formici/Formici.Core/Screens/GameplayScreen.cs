using System;
using System.Collections.Generic;
using System.Diagnostics;
using Formici.Core.Diagnostics;
using Formici.Core.Entities;
using Formici.Core.Inputs;
using Formici.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Formici.Core.Screens
{
    /// <summary>
    /// Core gameplay screen for the 2D top-down ant colony game.
    /// Manages top-down world rendering, entity updates, and player camera/inputs.
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        private ContentManager content;
        private SpriteBatch spriteBatch;
        private Texture2D pixelTexture;
        private float pauseAlpha;

        private long simulationTick;
        private TimeSpan simulationAccumulator = TimeSpan.Zero;
        private static readonly float TickRate = 10.0f;
        private static readonly TimeSpan SimulationTickRate = TimeSpan.FromSeconds(1.0 / TickRate);

        private readonly DebugOverlay debugOverlay = new DebugOverlay();
        private readonly List<Ant> ants = new();

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.0);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void LoadContent()
        {
            base.LoadContent();

            content ??= new ContentManager(ScreenManager.Game.Services, "Content");
            spriteBatch = ScreenManager.SpriteBatch;

            // 1x1 white texture for drawing simple shapes and rectangle entities
            pixelTexture = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            pixelTexture.SetData(new[]
            {
                Color.White
            });

            // Initialize ant in the middle of the screen
            float centerX = ScreenManager.GraphicsDevice.Viewport.Width / 2f;
            float centerY = ScreenManager.GraphicsDevice.Viewport.Height / 2f;
            ants.Add(new Ant(new Vector2(centerX, centerY)));

            debugOverlay.SetLine("Tick", "Simulation Tick: 0");
            debugOverlay.SetLine("TickRate", $"Tick Rate: {TickRate} Hz");
            debugOverlay.SetLine("Ants", $"Ants: {ants.Count}");
            debugOverlay.SetLine("GameTime", $"Game Time: ... s");

            ScreenManager.Game.ResetElapsedTime();
        }

        public override void UnloadContent()
        {
            content?.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            simulationAccumulator += gameTime.ElapsedGameTime;

            while (simulationAccumulator >= SimulationTickRate)
            {
                RunSimulationTick(gameTime);
                simulationAccumulator -= SimulationTickRate;
            }
        }

        private void RunSimulationTick(GameTime gameTime)
        {
            simulationTick++;
            float deltaSeconds = (float)SimulationTickRate.TotalSeconds;

            foreach (var ant in ants)
            {
                ant.Tick(deltaSeconds);
            }

            debugOverlay.SetLine("Tick", $"Simulation Tick: {simulationTick}");
            debugOverlay.SetLine("Ants", $"Ants: {ants.Count}");
            debugOverlay.SetLine("GameTime", $"Game Time: {gameTime.TotalGameTime.TotalSeconds} s");
        }

        public override void HandleInput(GameTime gameTime, InputState inputState)
        {
            ArgumentNullException.ThrowIfNull(inputState);

            base.HandleInput(gameTime, inputState);

            if (inputState.IsPauseGame(ControllingPlayer))
            {
                ScreenManager.AddScreen(new PauseScreen(), ControllingPlayer);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            // Draw top-down world entities
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, ScreenManager.GlobalTransformation);
            foreach (var ant in ants)
            {
                ant.Draw(spriteBatch, pixelTexture);
            }
            spriteBatch.End();

            // Draw upper-left debug overlay tab
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, ScreenManager.GlobalTransformation);
            debugOverlay.Draw(spriteBatch, ScreenManager.Font);
            spriteBatch.End();

            base.Draw(gameTime);

            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);
                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}