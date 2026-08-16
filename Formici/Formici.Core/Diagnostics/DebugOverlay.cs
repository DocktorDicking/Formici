using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Formici.Core.Diagnostics
{
    /// <summary>
    /// Debug overlay displayed as a semi-transparent tab in the upper-left corner of the game screen.
    /// Draws debug lines in yellow over a semi-transparent black background.
    /// </summary>
    public class DebugOverlay
    {
        private readonly List<string> lines = new List<string>();
        private readonly Dictionary<string, string> keyedEntries = new Dictionary<string, string>();
        private readonly List<string> orderedKeys = new List<string>();

        private Texture2D pixelTexture;
        private Vector2 position = new Vector2(10, 10);
        private int paddingHorizontal = 10;
        private int paddingVertical = 8;
        private float backgroundOpacity = 0.75f;
        private Color textColor = Color.Yellow;

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public float BackgroundOpacity
        {
            get => backgroundOpacity;
            set => backgroundOpacity = MathHelper.Clamp(value, 0f, 1f);
        }

        public Color TextColor
        {
            get => textColor;
            set => textColor = value;
        }

        /// <summary>
        /// Sets or updates a named debug entry line.
        /// </summary>
        public void SetLine(string key, string text)
        {
            if (!keyedEntries.ContainsKey(key))
            {
                orderedKeys.Add(key);
            }
            keyedEntries[key] = text;
        }

        /// <summary>
        /// Clears all stored debug lines.
        /// </summary>
        public void Clear()
        {
            keyedEntries.Clear();
            orderedKeys.Clear();
            lines.Clear();
        }

        /// <summary>
        /// Draws the debug overlay tab in the upper left corner.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            if (spriteBatch == null || font == null)
                return;

            // Collect lines to render
            lines.Clear();
            foreach (var key in orderedKeys)
            {
                if (keyedEntries.TryGetValue(key, out string value))
                {
                    lines.Add(value);
                }
            }

            if (lines.Count == 0)
                return;

            // Ensure 1x1 white pixel texture exists for background rendering
            if (pixelTexture == null)
            {
                pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                pixelTexture.SetData(new[] { Color.White });
            }

            // Calculate total overlay size based on text measurements
            float maxWidth = 0;
            float totalHeight = 0;
            float lineHeight = font.LineSpacing;

            foreach (var line in lines)
            {
                Vector2 size = font.MeasureString(line);
                if (size.X > maxWidth)
                    maxWidth = size.X;
            }

            totalHeight = lines.Count * lineHeight;

            int tabWidth = (int)maxWidth + (paddingHorizontal * 2);
            int tabHeight = (int)totalHeight + (paddingVertical * 2);

            Rectangle backgroundRect = new Rectangle((int)position.X, (int)position.Y, tabWidth, tabHeight);
            Color backgroundColor = Color.Black * backgroundOpacity;

            // Draw semi-transparent black background tab
            spriteBatch.Draw(pixelTexture, backgroundRect, backgroundColor);

            // Draw yellow debug text
            Vector2 textPos = new Vector2(position.X + paddingHorizontal, position.Y + paddingVertical);
            for (int i = 0; i < lines.Count; i++)
            {
                spriteBatch.DrawString(font, lines[i], textPos, textColor);
                textPos.Y += lineHeight;
            }
        }
    }
}
