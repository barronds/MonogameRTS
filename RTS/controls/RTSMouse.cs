using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using RTS.RTSMath;

namespace RTS.Controls
{
    class RTSMouse
    {
        tCoord mScreenDim;

        public RTSMouse( tCoord screen_dim )
        {
			mScreenDim = screen_dim;
		}

        public void Update( float dt )
        {
            MouseState state = Mouse.GetState();

            ButtonState button_state = state.LeftButton;
            string button_value = button_state.ToString();

            Point pos = state.Position;
            string pos_value = pos.ToString();

            Console.WriteLine( pos_value + " " + button_value);
        }
    }
}
