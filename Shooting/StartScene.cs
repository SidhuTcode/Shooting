using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio; 
using static Shooting.Game1;

namespace Shooting
{
    public class StartScene : IScene
    {
        private Game1 game;
        private GraphicsDeviceManager graphics;
        private ContentManager content;

        private SpriteFont startFont;
        private Vector2 startTextPosition;

        // Animation variables
        private Texture2D[] animationFrames;
        private int currentFrame;
        private float frameTimer = 0.01f; // Adjust based on your desired animation speed
        private float elapsed;

        private double timer = 3;

        float xOffset = 380;

        // Sound effects
        private SoundEffect hitSound;
        private SoundEffect missSound;

        // Property to store the requested scene type
        public GameSceneType? RequestedScene { get; private set; }

        // Initializes a new instance of the StartScene class.
        public StartScene(Game1 game, GraphicsDeviceManager graphics, ContentManager content)
        {
            this.game = game;
            this.graphics = graphics;
            this.content = content;
        }

        public void LoadContent()
        {
            startFont = content.Load<SpriteFont>("galleryFont");

            // Load hit and miss sounds
            hitSound = content.Load<SoundEffect>("Sounds/hitSound");
            missSound = content.Load<SoundEffect>("Sounds/missSound");

            // Set start text position in the center of the screen
            
            startTextPosition = new Vector2(
                (graphics.PreferredBackBufferWidth - startFont.MeasureString("Loading...").X) - xOffset,
                graphics.PreferredBackBufferHeight / 2
            );


            // Load individual PNG images for animation frames
            int numFrames = 30; // Adjust based on the actual number of frames
            animationFrames = new Texture2D[numFrames];

            for (int i = 0; i < numFrames; i++)
            {
                animationFrames[i] = content.Load<Texture2D>($"Loading/frame-{i + 1}");
            }
        }


        public void Update(GameTime gameTime)
        {
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Update the current frame
            frameTimer -= elapsed;
            if (frameTimer <= 0)
            {
                currentFrame = (currentFrame + 1) % animationFrames.Length; // Loop through frames
                frameTimer = 0.01f; 
            }

            // Decrement the timer
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            // If the timer reaches 0 or less, redirect to PlayScene
            if (timer <= 0)
            {
               
                RequestedScene = GameSceneType.Play;
            }
        }

        // Draws the start scene, including the loading animation and text.
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 centerPosition = new Vector2(
          (graphics.PreferredBackBufferWidth - animationFrames[currentFrame].Width) - xOffset,
          (graphics.PreferredBackBufferHeight - animationFrames[currentFrame].Height) / 2
           );

            // Offset the position to move the animation above the text
            Vector2 adjustedPosition = new Vector2(centerPosition.X, centerPosition.Y - 70);
            // Draw the current frame
            spriteBatch.Draw(animationFrames[currentFrame], adjustedPosition, Color.White);
            spriteBatch.DrawString(startFont, "Loading...", startTextPosition, Color.Black);
        }

        // Resets the start scene state, typically after a scene change.
        public void Reset()
        {
            RequestedScene = null;

        }
    }
}
