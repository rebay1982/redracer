﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
	interface IInputMngr
	{

    void Update();

    IInputState GetInputState();
  }
}
