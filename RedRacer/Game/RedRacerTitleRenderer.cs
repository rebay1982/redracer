using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  class RedRacerTitleRenderer : IRenderer
  {
    public void RenderToFrameBuffer(byte[] buffer, int height, int width)
    {
      // Render stuff.
    }

    public void RenterToFrameBuffer(byte[] buffer, int height, int width, IGameState gameState)
    {
      RenderToFrameBuffer(buffer, height, width);
    }
  }
}
