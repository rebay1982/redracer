using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
	interface IGame
	{


    /// <summary>
    /// Update the game state by dt.
    /// </summary>
    /// <param name="dt">Time delta.</param>
    void Update(int dt);


	}
}
