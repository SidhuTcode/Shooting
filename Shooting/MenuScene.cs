using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using static Shooting.Game1;

namespace Shooting
{
    public class MenuScene : IScene
    {
        private Game1 game;
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        private SpriteFont menuFont;
        private string menuText = "Press Enter to Start \n\nPress L for all Levels\n\nPress H for Help \n\nPress A for About \n\nPress Space to Exit";
        private Vector2 menuPosition;

        // Property to store the requested scene type
        public GameSceneType? RequestedScene { get; private set; }

        // Initializes a new instance of the MenuScene class.
        public MenuScene(Game1 game, GraphicsDeviceManager graphics, ContentManager content)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = content;
        }

        // It Loads content specific to the menu scene.
        public void LoadContent()
        {
            menuFont = content.Load<SpriteFont>("galleryFont");

            // Set menu position in the center of the screen
            float xOffset = 150;
            menuPosition = new Vector2(
                (graphics.PreferredBackBufferWidth - menuFont.MeasureString(menuText).X) - xOffset,
                graphics.PreferredBackBufferHeight / 2
            );
        }

        // Updates the menu scene
        public void Update(GameTime gameTime)
        {
            // Handle user input to navigate to different scenes and set RequestedScene accordingly
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                RequestedScene = GameSceneType.Start;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.H))
            {
                RequestedScene = GameSceneType.Help;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                RequestedScene = GameSceneType.About;
            } 
            if (Keyboard.GetState().IsKeyDown(Keys.L))
            {
                RequestedScene = GameSceneType.Levels;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw menu-related graphics
            spriteBatch.DrawString(menuFont, menuText, menuPosition, Color.Black);
        }

        // Method to reset the menu state (e.g., after a scene change)
        public void Reset()
        {
            RequestedScene = null;
        }
    }

}


