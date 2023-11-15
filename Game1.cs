using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spiderman
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D spidermanBGTexture;
        SpriteFont presskeyFont;

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
            this.Window.Title = "Spiderman Swings to the Rescue!";
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screen = Screen.Intro;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            spidermanBGTexture = Content.Load<Texture2D>("SpidermanBG");
            presskeyFont = Content.Load<SpriteFont>("PressKey");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {

            }
            else if (screen == Screen.Swinging)
            {

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