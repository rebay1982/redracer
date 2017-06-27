using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
	interface IRenderMngr
  {
    event RendererFrameReady FrameReadyEvent;

    byte[] GetFrameBuffer();
    void Render(IGameState gameState);

    // Will register the renderer in the rendering queue and return a renderer Id?
    Guid RegisterRenderer(IRenderer renderer);

    // Will unregister a renderer from the rendering queue.
    IRenderer UnregisterRenderer(Guid guid);
	}
}
