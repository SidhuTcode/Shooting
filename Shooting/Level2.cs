using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shooting.Game1;

namespace Shooting
{
    public class Level2 : IScene
    {
        private Game1 game;
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        private Texture2D targetSprite;
        private Vector2 targetPosition;
        private const int targetRadius = 45;
        private Texture2D aimSprite;
        Texture2D backgroundSprite;

        private SpriteFont playFont;
        private int score = 0;
        private double timer = 13;

        private SoundEffect hitSound;
        private SoundEffect missSound;

        // Gets or sets the type of scene requested to transition
        public GameSceneType? RequestedScene { get; private set; }

        // Gets or sets the type of scene requested to transition
        public Level2 (Game1 game, GraphicsDeviceManager graphics, ContentManager content)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = content;
        }

        public void LoadContent()
        {
            targetSprite = content.Load<Texture2D>("Images/target");
            aimSprite = content.Load<Texture2D>("Images/aim");
            backgroundSprite = content.Load<Texture2D>("Images/PlayBackground");
            playFont = content.Load<SpriteFont>("galleryFont");

            hitSound = content.Load<SoundEffect>("Sounds/hitSound");
            missSound = content.Load<SoundEffect>("Sounds/missSound");


            // Initialize target position
            Random rand = new Random();
            targetPosition = new Vector2(rand.Next(0, graphics.PreferredBackBufferWidth), rand.Next(0, graphics.PreferredBackBufferHeight));
        }

        public void Update(GameTime gameTime)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (timer < 0)
            {
                timer = 0;
                
            }

            // Check for user input to interact with the target
            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                float mouseTargetDist = Vector2.Distance(targetPosition, mouseState.Position.ToVector2());
                if (mouseTargetDist < targetRadius && timer > 0)
                {
                    // Player hit the target
                    score++;
                    hitSound.Play();

                    // Move the target to a new random position
                    Random rand = new Random();
                    targetPosition.X = rand.Next(0, graphics.PreferredBackBufferWidth);
                    targetPosition.Y = rand.Next(0, graphics.PreferredBackBufferHeight);
                }
                else if (mouseTargetDist > targetRadius && timer > 0)
                {
                    missSound.Play();
                }

            }

            if (score >= 10 || timer == 0)
            {
                
                // Transition to EndScene after hitting the required targets
                RequestedScene = GameSceneType.End;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw play scene-related graphics
            spriteBatch.Draw(backgroundSprite, Vector2.Zero, Color.White);
            spriteBatch.Draw(targetSprite, new Vector2(targetPosition.X - targetRadius, targetPosition.Y - targetRadius), Color.White);
            spriteBatch.Draw(aimSprite, new Vector2(Mouse.GetState().X - 25, Mouse.GetState().Y - 25), Color.White);
            spriteBatch.DrawString(playFont, "Score: " + score.ToString(), new Vector2(10, 10), Color.Black);
            spriteBatch.DrawString(playFont, "Time: " + Math.Ceiling(timer).ToString(), new Vector2(10, 40), Color.Black);
            spriteBatch.DrawString(playFont, "Scores Needed: 10 ", new Vector2(10, 70), Color.Black);
        }

        public void Reset()
        {
            RequestedScene = null;
            timer = 13;
            score = 0;
        }
    }
}
