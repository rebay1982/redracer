﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
	interface IRenderMngr
	{

    void Render(IGameState gameState);
	}
}