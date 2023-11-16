using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Spiderman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D spidermanBGTexture;
        Texture2D cityBGTexture;
        Rectangle city1Rect;
        Rectangle city2Rect;
        Vector2 citySpeed;
        List<Texture2D> spiderman = new List<Texture2D>();
        SpriteFont presskeyFont;
        int frame, swings;
        bool nextFrame;
        enum Screen
        {
            Intro,
            Swinging,
            Landing,
            Punch,
            End
        }
        Screen screen;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 900;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            this.Window.Title = "Spiderman Swings to the Rescue!";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Intro;
            frame = 0;
            nextFrame = true;
            city1Rect = new Rectangle(0, 0, 1800, 500);
            city2Rect = new Rectangle(-1800, 0, 1800, 500);
            citySpeed = new Vector2(10, 0);
            swings = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spidermanBGTexture = Content.Load<Texture2D>("SpidermanBG");
            presskeyFont = Content.Load<SpriteFont>("PressKey");
            for (int i = 0; i < 69; i++)
            {
                if (i < 10)
                {
                    spiderman.Add(Content.Load<Texture2D>("frame_0" + i + "_delay-0.03s"));
                }
                else
                {
                    spiderman.Add(Content.Load<Texture2D>("frame_" + i + "_delay-0.03s"));
                }
            }
            cityBGTexture = Content.Load<Texture2D>("CityBG");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {
                var keys = Keyboard.GetState().GetPressedKeys();
                if (keys.Count() > 0)
                {
                    screen = Screen.Swinging;
                }
            }
            else if (screen == Screen.Swinging)
            {
                city1Rect.X += (int)citySpeed.X;
                city2Rect.X += (int)citySpeed.X;
                if (city1Rect.Left >= -5 && city1Rect.Left <=5)
                {
                    city2Rect = new Rectangle(-1800, 0, 1800, 500);
                }
                if (city2Rect.Left >= -5 && city2Rect.Left <= 5)
                {
                    city1Rect = new Rectangle(-1800, 0, 1800, 500);
                }
                if (nextFrame)
                {
                    frame++;
                    if (frame > 68)
                    {
                        frame = 0;
                        swings++;
                        if (swings > 10)
                        {
                            screen = Screen.Landing;
                        }
                    }
                    nextFrame = false;
                }
                else
                {
                    nextFrame = true;
                }
            }
            else if (screen == Screen.Landing)
            {

            }
            else if (screen == Screen.Punch)
            {

            }
            else if (screen == Screen.End)
            {

            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(spidermanBGTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
                _spriteBatch.DrawString(presskeyFont, "Press Any Key to Advance!", new Vector2 (50, 400), Color.Blue);
            }
            else if (screen == Screen.Swinging)
            {
                _spriteBatch.Draw(cityBGTexture, city1Rect, Color.White);
                _spriteBatch.Draw(cityBGTexture, city2Rect, Color.White);
                _spriteBatch.Draw(spiderman[frame], new Rectangle (130, 0, 500, 500), Color.White);
            }
            else if (screen == Screen.Landing)
            {

            }
            else if (screen == Screen.Punch)
            {

            }
            else if (screen == Screen.End)
            {

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}