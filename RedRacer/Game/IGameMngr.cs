using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  interface IGameMngr
  {

    /// <summary>
    /// Updated the current game state based on input and a time step.
    /// </summary>
    /// <param name="inputState"></param>
    /// <param name="timeStep"></param>
    /// <returns>The updated game state.</returns>    
    IGameState Update(IInputState inputState, double timeStep);

    /// <summary>
    /// Extrapolated (but doesn't store) the current game state based on
    /// the previous state, the current state and input and an alpha time
    /// step.
    /// </summary>
    /// <param name="inputSate">The user input.</param>
    /// <param name="previousState">The previous state to base the extrapolation on.</param>
    /// <param name="alphaTime">The alpha time step.</param>
    /// <returns>The extrapolated game state.</returns>
    IGameState ExtrapolateGameState(
      IInputState inputSate,
      IGameState previousState,
      double alphaTime);

    /// <summary>
    /// Retrieve the current game state.
    /// </summary>
    /// <returns>The current game state.</returns>
    IGameState GetGameState();
  }
}
