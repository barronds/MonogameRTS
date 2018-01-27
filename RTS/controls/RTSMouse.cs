using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using RTS.RTSMath;
using RTS.Render;

namespace RTS.Controls
{
    class RTSMouse
    {
        tCoord mScreenDim;
		SimpleDraw mSimpleDraw;

        public RTSMouse( tCoord screen_dim, SimpleDraw simple_draw )
        {
			mScreenDim = screen_dim;
			mSimpleDraw = simple_draw;
		}

        public void Update( GameTime game_time )
        {
            MouseState state = Mouse.GetState();

            ButtonState button_state = state.LeftButton;
            string button_value = button_state.ToString();

            Point pos = state.Position;
            string pos_value = pos.ToString();

            Console.WriteLine( pos_value + " " + button_value);
        }

		public void Render( GameTime game_time )
		{
			// need concept of a camera here to understand what world pos to use
			//mSimpleDraw.DrawLine( )
		}
    }
}
