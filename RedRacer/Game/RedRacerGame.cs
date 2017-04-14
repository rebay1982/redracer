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
    private readonly RedRacerGamePlayState playState; 

		public RedRacerGame()
		{
      playState = new RedRacerGamePlayState();
    }
 
    public IGameState Update(IInputState inputState, double timeStep)
    {
      return playState;
    }

    public IGameState ExtrapolateGameState(IInputState inputSate, IGameState previousState, double alphaTime)
    {
      // Not into "Fixed update timestep, variable rendering loop" for this game.
      // return current game state instead.
      return playState;  
    }

    public IGameState GetGameState()
    {
      return playState;
    }
  }
}
