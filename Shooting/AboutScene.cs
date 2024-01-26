using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using static Shooting.Game1;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;

namespace Shooting
{
    public class AboutScene : IScene
    {
        private Game1 game;
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        private SpriteFont aboutFont;
        private string aboutText = "Developed by : \nTajeshvir Singh \nVersion: 1.0 \n\n Press Escape retune to Main Menu\n\nPress Space to Exit";
        private Vector2 aboutTextPosition;

        // Property to store the requested scene type
        public GameSceneType? RequestedScene { get; private set; }

        // Initializes a new instance
        public AboutScene(Game1 game, GraphicsDeviceManager graphics, ContentManager content)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = content;
        }

        public void LoadContent()
        {
            aboutFont = content.Load<SpriteFont>("galleryFont");

            // Set about text position in the center of the screen
            float xOffset = 60;
            aboutTextPosition = new Vector2(
                (graphics.PreferredBackBufferWidth - aboutFont.MeasureString(aboutText).X) - xOffset,
                graphics.PreferredBackBufferHeight / 2
            );
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                RequestedScene = GameSceneType.Menu;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw about scene-related graphics
            spriteBatch.DrawString(aboutFont, aboutText, aboutTextPosition, Color.Black);
        }
        public void Reset()
        {
            RequestedScene = null;
        }
    }


}
