using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  interface IRenderer
  {
    void RenderToFrameBuffer(byte[] buffer, int height, int width);
    void RenderToFrameBuffer(byte[] buffer, int height, int width, IGameState gameState);
  }
}
