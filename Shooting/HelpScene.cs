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

namespace Shooting
{
    public class HelpScene : IScene
    {
        private Game1 game;
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        private SpriteFont helpFont;
        private string helpText = "Use the mouse to aim \nand click to shoot the target. \nScore points by hitting the target! \n\nPress Escape retune to Main Menu\n\nPress Space to Exit";
        private Vector2 helpTextPosition;

        public GameSceneType? RequestedScene { get; private set; }

        public HelpScene(Game1 game, GraphicsDeviceManager graphics, ContentManager content)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = content;
        }

        public void LoadContent()
        {
            helpFont = content.Load<SpriteFont>("galleryFont");

            // Set help text position in the center of the screen
            float xOffset = 60;
            helpTextPosition = new Vector2(
                (graphics.PreferredBackBufferWidth - helpFont.MeasureString(helpText).X)- xOffset,
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
            // Draw help scene-related graphics
            spriteBatch.DrawString(helpFont, helpText, helpTextPosition, Color.Black);
        }
        public void Reset()
        {
            RequestedScene = null;
        }
    }


}
