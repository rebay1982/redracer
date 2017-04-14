using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{

    // The game engine.
	class RedRacerGame : IGameMngr
	{

		public RedRacerGame()
		{

    }
 
    public IGameState Update(IInputState inputState, double timeStep)
    {
      return null;
    }

    public IGameState ExtrapolateGameState(IInputState inputSate, IGameState previousState, double alphaTime)
    {
      // Not into "Fixed update timestep, variable rendering loop" for this game.
      // return current game state instead.
      return null;  
    }

    public IGameState GetGameState()
    {
      return null;
    }
  }
}
