/* Developed by Tajeshvir Singh 
 * Game Programming with Data Structures Final Project
 * December 8 2023 - December 10 2023
 */
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Shooting
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

       
        Texture2D backgroundSprite;
        SpriteFont gameFont;
       

        Vector2 targetPosition = new Vector2(300, 300);
        const int targetRadius = 45;

        MouseState mState;
        bool mReleased = true;
        int score = 0;

        double timer = 10;

        // game scenes
        private MenuScene menuScene;
        private StartScene startScene;
        private AboutScene aboutScene;
        private PlayScene playScene;
        private HelpScene helpScene;
        private EndScene endScene;
        private Level3 level3;
        private Level4 level4;
        private Level2 level2;
        private Levels levels;

        // Current active scene
        private IScene currentScene;

        // Sound effects and background music
        SoundEffect backgroundMusic;
        SoundEffectInstance backgroundMusicInstance;

        // Initializes a new instance of the Game1 class.
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        // Initializes graphics-related settings.
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        // Loads game content during startup.
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
                        
            backgroundSprite = Content.Load<Texture2D>("Images/mainBackground");
            gameFont = Content.Load<SpriteFont>("galleryFont");
                       
            backgroundMusic = Content.Load<SoundEffect>("backgroundMusic");

            backgroundMusicInstance = backgroundMusic.CreateInstance();
            backgroundMusicInstance.IsLooped = true;
            backgroundMusicInstance.Play();

            // Initialize your scenes
            menuScene = new MenuScene(this, _graphics, Content);
            startScene = new StartScene(this, _graphics, Content);
            aboutScene = new AboutScene(this, _graphics, Content);
            playScene = new PlayScene(this, _graphics, Content);
            helpScene = new HelpScene(this, _graphics, Content);
            endScene = new EndScene(this, _graphics, Content);
            level2 = new Level2(this, _graphics, Content);
            level3 = new Level3(this, _graphics, Content);
            level4 = new Level4(this, _graphics, Content);
            levels = new Levels(this, _graphics, Content);

            // Set the initial active scene
            currentScene = menuScene;
            currentScene.LoadContent();
        }

        // Updates the game state.
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Exit();
            }
            currentScene.Update(gameTime);

            // Stop background music if in PlayScene
            if (currentScene is PlayScene playScene)
            {
                backgroundMusicInstance.Stop(); 
            }
           
            if (currentScene is EndScene endScene)
            {
                backgroundMusicInstance.Stop(); 
            }
            else
            {
                // Resume or start background music if not in PlayScene
                if (backgroundMusicInstance.State == SoundState.Paused)
                {
                    backgroundMusicInstance.Resume();
                }
                else if (backgroundMusicInstance.State == SoundState.Stopped)
                {
                    backgroundMusicInstance.Play();
                }
            }

            // Handle scene transitions
            if (currentScene is MenuScene menu && menu.RequestedScene != null)
            {
                ChangeScene(menu.RequestedScene.Value);
                menu.Reset();
            }
            if (currentScene is StartScene start && start.RequestedScene != null)
            {
                ChangeScene(start.RequestedScene.Value);
                start.Reset();
            }
            if (currentScene is HelpScene help && help.RequestedScene != null)
            {
                ChangeScene(help.RequestedScene.Value);
                help.Reset();
            }
            if (currentScene is PlayScene play && play.RequestedScene != null)
            {
                ChangeScene(play.RequestedScene.Value);
                play.Reset();
            }
            if (currentScene is AboutScene about && about.RequestedScene != null)
            {
                ChangeScene(about.RequestedScene.Value);
                about.Reset();
            }
            if (currentScene is EndScene end && end.RequestedScene != null)
            {
                ChangeScene(end.RequestedScene.Value);
                end.Reset();
            }
            if (currentScene is Level2 lvl2 && lvl2.RequestedScene != null)
            {
                ChangeScene(lvl2.RequestedScene.Value);
                lvl2.Reset();
            }
            if (currentScene is Level3 lvl3 && lvl3.RequestedScene != null)
            {
                ChangeScene(lvl3.RequestedScene.Value);
                lvl3.Reset();
            }
            if (currentScene is Level4 lvl4 && lvl4.RequestedScene != null)
            {
                ChangeScene(lvl4.RequestedScene.Value);
                lvl4.Reset();
            }
            if (currentScene is Levels levels && levels.RequestedScene != null)
            {
                ChangeScene(levels.RequestedScene.Value);
                levels.Reset();
            }


            base.Update(gameTime);
        }

        // Draws the game content.

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundSprite, Vector2.Zero, Color.White);
            currentScene.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        // Enumeration for game scene types...
        public enum GameSceneType
        {
            Menu,
            Start,
            About,
            Play,
            Help,
            End,
            Level2,
            Level3,
            Level4,
            Levels,
           
        }

        private void ChangeScene(GameSceneType sceneType)
        {
            // Switch the current active scene based on the requested scene type
            switch (sceneType)
            {
                case GameSceneType.Start:
                    currentScene = startScene;
                    break;

                case GameSceneType.About:
                    currentScene = aboutScene;
                    break;

                case GameSceneType.Play:
                    currentScene = playScene;
                    break;

                case GameSceneType.Help:
                    currentScene = helpScene;
                    break;

                case GameSceneType.Menu:
                    currentScene = menuScene;
                    break; 
                case GameSceneType.End:
                    currentScene = endScene;
                    break;
                case GameSceneType.Level2:
                    currentScene = level2;
                    break;
                case GameSceneType.Level3:
                    currentScene = level3;
                    break;
                case GameSceneType.Level4:
                    currentScene = level4;
                    break; 
                case GameSceneType.Levels:
                    currentScene = levels;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(sceneType), sceneType, null);
            }

            currentScene.LoadContent();
        }

    }

}
