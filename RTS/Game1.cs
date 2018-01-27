using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

using RTS.Controls;
using RTS.RTSMath;
using RTS.Render;

namespace RTS
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager	mGraphics;
        SpriteBatch				mSpriteBatch;
        RTSMouse				mMouse;
		SimpleDraw              mSimpleDraw;

        public Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
			// TODO: Add your initialization logic here
			var current_display_mode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
			tCoord native_dim = new tCoord( current_display_mode.Width, current_display_mode.Height );

			mGraphics.IsFullScreen = false; // true;
			mGraphics.PreferredBackBufferWidth = native_dim.x;
			mGraphics.PreferredBackBufferHeight = native_dim.y;
			mGraphics.ApplyChanges();

			mSimpleDraw = new SimpleDraw( GraphicsDevice );
			mMouse = new RTSMouse( native_dim, mSimpleDraw );

			base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            mSpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime game_time)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mMouse.Update( game_time );

            base.Update( game_time );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

			// TODO: Add your drawing code here
			mMouse.Render( gameTime );

			// simple draw render call for all systems above that might use it.
			mSimpleDraw.DrawAllPrimitives();

            base.Draw(gameTime);
        }
    }
}
