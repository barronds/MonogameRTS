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
		BasicEffect             mBasicEffect_World;
		BasicEffect             mBasicEffect_Screen;
		SimpleDraw              mSimpleDraw_World;
		SimpleDraw              mSimpleDraw_Screen;
		RTSMouse                mMouse;
		float                   mCamDrift, mCamStartX, mCamStartY;

		public Game1()
        {
            mGraphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			mCamDrift = 0.1f;
			mCamStartX = 6.0f;
			mCamStartY = 4.0f;
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

			mGraphics.IsFullScreen = false;
			mGraphics.PreferredBackBufferWidth = native_dim.x;
			mGraphics.PreferredBackBufferHeight = native_dim.y;
			mGraphics.ApplyChanges();

			mSimpleDraw_World = new SimpleDraw( GraphicsDevice );
			mSimpleDraw_Screen = new SimpleDraw( GraphicsDevice );

			mMouse = new RTSMouse( native_dim, mSimpleDraw_World, mSimpleDraw_Screen );

			base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
			// world space rendering setup
			mBasicEffect_World = new BasicEffect( GraphicsDevice );
			mBasicEffect_World.World = Matrix.Identity;           
			mBasicEffect_World.View = Matrix.CreateLookAt( new Vector3( mCamStartX, mCamStartY, 1f ), new Vector3( mCamStartX, mCamStartY, 0f ), new Vector3( 0f, 1f, 0f ) );
			float aspect = (float)(mGraphics.PreferredBackBufferHeight) / mGraphics.PreferredBackBufferWidth;
			float viewport_scale = 10f;
			mBasicEffect_World.Projection = Matrix.CreateOrthographicOffCenter( -viewport_scale, viewport_scale, -viewport_scale * aspect, viewport_scale * aspect, 0f, 2f );

			// screen space rendering setup
			mBasicEffect_Screen = new BasicEffect( GraphicsDevice );
			mBasicEffect_Screen.World = Matrix.Identity;
			mBasicEffect_Screen.View = Matrix.CreateLookAt( new Vector3( 0f, 0f, 1f ), new Vector3( 0f, 0f, 0f ), new Vector3( 0f, 1f, 0f ) );
			mBasicEffect_Screen.Projection = Matrix.CreateOrthographicOffCenter( 0, mGraphics.PreferredBackBufferWidth, mGraphics.PreferredBackBufferHeight, 0, 0f, 2f );
			
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
			//System.Console.WriteLine( "update " + game_time.ElapsedGameTime.ToString() );

			float dx = mCamDrift * (game_time.TotalGameTime.Seconds + (game_time.TotalGameTime.Milliseconds) / 1000f);
			mBasicEffect_World.View = Matrix.CreateLookAt( new Vector3( mCamStartX + dx, mCamStartY, 1f ), new Vector3( mCamStartX + dx, mCamStartY, 0f ), new Vector3( 0f, 1f, 0f ) );

			if( GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            mMouse.Update( game_time );

            base.Update( game_time );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime game_time)
        {
			//System.Console.WriteLine( "draw " + game_time.ElapsedGameTime.ToString() );

			GraphicsDevice.Clear(Color.CornflowerBlue);

			RasterizerState rasterizerState = new RasterizerState();
			rasterizerState.CullMode = CullMode.None;
			GraphicsDevice.RasterizerState = rasterizerState;

			// simple draw only clients
			mMouse.RenderWorld( game_time );
			mMouse.RenderScreen( game_time );

			// simple draw world
			mBasicEffect_World.VertexColorEnabled = true;
			EffectTechnique effectTechnique = mBasicEffect_World.Techniques[0];
			EffectPassCollection effectPassCollection = effectTechnique.Passes;

			
			foreach( EffectPass pass in effectPassCollection )
			{
				// hopefully only one pass
				pass.Apply();

				// actually render simple draw stuff.  possible layers needed.
				mSimpleDraw_World.DrawAllPrimitives();

				// render clients who do their own rendering.  they should probably have pre-renders like simple draw, especially if there is more than one pass.
			}


			// simple draw screen
			mBasicEffect_Screen.VertexColorEnabled = true;
			effectTechnique = mBasicEffect_Screen.Techniques[0];
			effectPassCollection = effectTechnique.Passes;

			foreach( EffectPass pass in effectPassCollection )
			{
				// hopefully only one pass
				pass.Apply();

				// actually render simple draw stuff.  possible layers needed.
				mSimpleDraw_Screen.DrawAllPrimitives();

				// render clients who do their own rendering.  they should probably have pre-renders like simple draw, especially if there is more than one pass.
			}
			

			base.Draw( game_time );
		}
	}
}
