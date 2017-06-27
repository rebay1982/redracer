using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  // TODO: Overkill?
  abstract class AbstractRenderer : IRenderer
  {
    public bool IsInitialized { get; protected set; } = false;

    protected abstract void Init();

    // IRenderer
    public abstract void RenderToFrameBuffer(byte[] buffer, int width, int height);
    public abstract void RenterToFrameBuffer(byte[] buffer, int width, int height, IGameState gameState);
  }
}
