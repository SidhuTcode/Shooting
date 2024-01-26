using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Shooting.Game1;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Shooting 
{
    public class EndScene : IScene
    {
        private Game1 game;
        private GraphicsDeviceManager graphics;
        private ContentManager content;
        
        private SpriteFont endFont;
        private string endText = "Game Over  \n\nPress Enter for Level-1 \nPress 2 for Level-2 \nPress 3 for Level-3 \nPress 4 for Level-4 \n\nPress Escape return to Main Menu\nPress Space to Exit";
        private Vector2 endTextPosition;
        private SoundEffect backgroundMusic;

        public GameSceneType? RequestedScene { get; private set; }

        public EndScene(Game1 game, GraphicsDeviceManager graphics, ContentManager content)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = content;        

        }

        public void LoadContent()
        {
            endFont = content.Load<SpriteFont>("galleryFont");
            backgroundMusic = content.Load<SoundEffect>("Sounds/gameOver");
            backgroundMusic.Play();

            //Set end text position in the center of the screen
            float xOffset = 50;
            endTextPosition = new Vector2(
               (graphics.PreferredBackBufferWidth - endFont.MeasureString(endText).X) - xOffset,
               graphics.PreferredBackBufferHeight / 2
           );
        }

        public void Update(GameTime gameTime)
        {
            
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                RequestedScene = GameSceneType.Level2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D3))
            {
                RequestedScene = GameSceneType.Level3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D4))
            {
                RequestedScene = GameSceneType.Level4;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                RequestedScene = GameSceneType.Play;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                RequestedScene = GameSceneType.Menu;
            } 
            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw end scene-related graphics
            spriteBatch.DrawString(endFont, endText, endTextPosition, Color.Black);
        }
    

        public void Reset()
        {
            RequestedScene = null;
        }
    }
}
