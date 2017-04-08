using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{

    // The game engine.
	class Game : IGame
	{
		private readonly IInput inputMngr;
		private readonly IRenderer renderMngr;

		public Game(IInput inputMngr, IRenderer renderMngr)
		{
			this.inputMngr = inputMngr;
			this.renderMngr = renderMngr;
		}
	}
}
