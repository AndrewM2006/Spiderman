using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        Texture2D roofTexture;
        Texture2D venom1Texture;
        Texture2D venom2Texture;
        Rectangle city1Rect;
        Rectangle city2Rect;
        Vector2 citySpeed;
        Vector2 spidermanSpeed;
        Vector2 venomSpeed;
        Rectangle spidermanRect;
        Rectangle venomRect;
        List<Texture2D> spiderman = new List<Texture2D>();
        List<Texture2D> spiderman2 = new List<Texture2D>();
        SpriteFont presskeyFont;
        int frame, swings;
        bool nextFrame;
        bool drawtext, landed;
        float seconds;
        float startTime;
        KeyboardState keyboardState;
        SoundEffectInstance spidersonginstance;
        enum Screen
        {
            Intro,
            Swinging,
            Landing,
            GoodEnd,
            BadEnd
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
            spidermanSpeed = new Vector2(3, 2);
            spidermanRect = new Rectangle(0, -30, 140, 150);
            swings = 0;
            drawtext = false;
            venomRect = new Rectangle(600, 300, 180, 140);
            venomSpeed = new Vector2(3, 0);
            landed = true;
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
            for (int i =1; i<12; i++)
            {
                spiderman2.Add(Content.Load<Texture2D>("Spiderman" + i));
            }
            cityBGTexture = Content.Load<Texture2D>("CityBG");
            var spidersong = Content.Load<SoundEffect>("SpidermanSong");
            roofTexture = Content.Load<Texture2D>("Rooftop");
            venom1Texture = Content.Load<Texture2D>("Venom1");
            venom2Texture = Content.Load<Texture2D>("Venom2");
            spidersonginstance = spidersong.CreateInstance();
            spidersonginstance.Play();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {
                var keys = Keyboard.GetState().GetPressedKeys();
                if (spidersonginstance.State == SoundState.Stopped)
                {
                    screen = Screen.Swinging;
                }
                if (keys.Count() > 0)
                {
                    screen = Screen.Swinging;
                    spidersonginstance.Stop();
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
                        if (swings > 5)
                        {
                            screen = Screen.Landing;
                            frame = 0;
                            startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                        }
                    }
                   
                }
                nextFrame = ! nextFrame;
               
            }
            else if (screen == Screen.Landing)
            {
                if (spidermanRect.Left < 400 && frame<2)
                {
                    spidermanRect.X += (int)spidermanSpeed.X;
                    spidermanRect.Y += (int)spidermanSpeed.Y;
                }
                if (seconds > 0.7 && frame<2)
                {
                    frame++;
                    startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                }
                else if (frame == 2)
                {
                    drawtext = true;
                    if (keyboardState.IsKeyDown(Keys.Space) || seconds > 2)
                    {
                        drawtext = false;
                        if (seconds < 2)
                        {
                            frame++;
                            startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                            spidermanRect = new Rectangle(550, 300, 140, 150);
                        }
                        else
                        {
                            frame = 8;
                            landed = false;
                            startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                            spidermanRect = new Rectangle(450, 330, 140, 150);
                        }
                    }
                }
                else if (frame > 2 && frame<7)
                {
                    if (seconds > 0.4)
                    {
                        frame++;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
                else if (frame == 7)
                {
                    venomRect.X += (int)venomSpeed.X;
                    if (venomRect.Left>_graphics.PreferredBackBufferWidth)
                    {
                        screen = Screen.GoodEnd;
                    }
                }
                else if (frame < 10 && frame>7)
                {
                    spidermanSpeed = new Vector2(2, 0);
                    spidermanRect.X += (int)spidermanSpeed.X;
                    if (seconds > 0.7)
                    {
                        frame++;
                        startTime = (float)gameTime.TotalGameTime.TotalSeconds;
                    }
                }
                else if (frame == 10)
                {
                    spidermanRect.X += (int)spidermanSpeed.X;
                    if (spidermanRect.Left > _graphics.PreferredBackBufferWidth)
                    {
                        screen = Screen.BadEnd;
                    }
                }
            }
            else if (screen == Screen.GoodEnd)
            {

            }
            else
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
                _spriteBatch.Draw(cityBGTexture, new Rectangle(0, 0, 1800, 500), Color.White);
                _spriteBatch.Draw(roofTexture, new Rectangle(400, 400, 500, 100), Color.White);
                if (frame >= 7 && landed)
                {
                    _spriteBatch.Draw(venom2Texture, venomRect, Color.White);
                }
                else
                {
                    _spriteBatch.Draw(venom1Texture, new Rectangle(600, 300, 180, 140), Color.White);
                }
                _spriteBatch.Draw(spiderman2[frame], spidermanRect, Color.White);
                if (drawtext)
                {
                    _spriteBatch.DrawString(presskeyFont, "Press Space to Land", new Vector2 (120, 100), Color.White);
                }
            }
            else if (screen == Screen.GoodEnd)
            {

            }
            else
            {

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}