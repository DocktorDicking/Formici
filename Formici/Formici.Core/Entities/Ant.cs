using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Formici.Core.Entities
{
    /// <summary>
    /// Represents an individual ant entity in the simulation.
    /// Tracks position, orientation direction, movement speed, energy, and age.
    /// </summary>
    public class Ant
    {
        public Vector2 Position { get; set; }
        
        /// <summary>
        /// Orientation direction angle in radians.
        /// </summary>
        public float Direction { get; set; }
        
        public float Speed { get; set; }
        
        public float Energy { get; set; }
        
        public float Age { get; set; }

        public Vector2 Size { get; } = new(4, 8);

        public Ant(Vector2 startPosition)
        {
            Position = startPosition;
            Direction = 0f;
            Speed = 20f;
            Energy = 100f;
            Age = 0f;
        }

        /// <summary>
        /// Updates ant state per simulation tick.
        /// </summary>
        /// <param name="deltaSeconds">Elapsed simulation time in seconds.</param>
        public void Tick(float deltaSeconds)
        {
            Age += deltaSeconds;
            // Basic energy decay over time
            Energy = MathF.Max(0f, Energy - (0.5f * deltaSeconds));
        }

        /// <summary>
        /// Draws the aunt as a tiny black rectangle.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Texture2D pixelTexture)
        {
            if (spriteBatch == null || pixelTexture == null)
                return;

            Vector2 origin = new Vector2(0.5f, 0.5f); // Center origin for scaling and rotation
            Rectangle destinationRectangle = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                (int)Size.X,
                (int)Size.Y
            );

            spriteBatch.Draw(
                pixelTexture,
                destinationRectangle,
                null,
                Color.Black,
                Direction,
                origin,
                SpriteEffects.None,
                0f
            );
        }
        
        public void Rotate(int radians)
        {
            
        }
        
        public void Move()
        {

        }
    }
}
