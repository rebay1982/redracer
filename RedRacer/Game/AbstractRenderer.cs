using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  abstract class AbstractRenderer : IRenderer
  {
    public bool IsInitialized { get; protected set; } = false;

    public abstract void Init();

    // IRenderer
    public abstract void RenderToFrameBuffer(byte[] buffer, int height, int width);
    public abstract void RenterToFrameBuffer(byte[] buffer, int height, int width, IGameState gameState);
  }
}
