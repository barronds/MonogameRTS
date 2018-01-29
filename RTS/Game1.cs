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
		BasicEffect             mBasicEffect;
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
			mBasicEffect = new BasicEffect( GraphicsDevice );
			mBasicEffect.World = Matrix.Identity;           
			mBasicEffect.View = Matrix.CreateLookAt( new Vector3( 0, 0, 1f ), new Vector3( 0, 0, 0 ), new Vector3( 0, 1f, 0 ) );
			float aspect = (float)(mGraphics.PreferredBackBufferHeight) / mGraphics.PreferredBackBufferWidth;
			float viewport_scale = 10f;
			mBasicEffect.Projection = Matrix.CreateOrthographicOffCenter( -viewport_scale, viewport_scale, -viewport_scale * aspect, viewport_scale * aspect, 0f, 2f );
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

			// simple draw only clients
			mMouse.Render( gameTime );

			EffectTechnique effectTechnique = mBasicEffect.Techniques[0];
			EffectPassCollection effectPassCollection = effectTechnique.Passes;

			foreach( EffectPass pass in effectPassCollection )
			{
				// hopefully only one pass
				pass.Apply();

				// actually render simple draw stuff.  possible layers needed.
				mSimpleDraw.DrawAllPrimitives();

				// render clients who do their own rendering.  they should probably have pre-renders like simple draw, especially if there is more than one pass.
			}

			base.Draw( gameTime );
		}
	}
}
