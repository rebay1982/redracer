using System;

namespace RedRacer.Game
{
	interface IRenderMngr
  {
    byte[] GetFrameBuffer();
    void Render(IGameState gameState);
 
    // Will register the renderer in the rendering queue and return a renderer Id?
    Guid RegisterRenderer(IRenderer renderer);

    // Will unregister a renderer from the rendering queue.
    IRenderer UnregisterRenderer(Guid guid);
	}
}
