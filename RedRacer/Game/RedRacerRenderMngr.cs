using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  class RedRacerRenderMngr : IRenderMngr
  {
    Dictionary<Guid, IRenderer> Renderers;// = new List<IRenderer>();

    private byte[] ActiveBuffer;
    private byte[] ShadowBuffer;

    // IRenderMngr
    public byte[] GetFrameBuffer()
    {
      // Don't return the internal FB.  Create a copy -- check how much this costs.
      return ActiveBuffer;
    }

    public void Render(IGameState gameState)
    {
      foreach (IRenderer renderer in Renderers.Values)
      {
        renderer.RenderToFrameBuffer(ShadowBuffer, 320, 200);
      }

      byte[] switchBuffer = ActiveBuffer;
      ActiveBuffer = ShadowBuffer;
      ShadowBuffer = ActiveBuffer;
    }

    public Guid RegisterRenderer(IRenderer renderer)
    {
      Guid guid = System.Guid.NewGuid();
      Renderers.Add(guid, renderer);

      return guid;
    }

    public IRenderer UnregisterRenderer(Guid guid)
    {
      IRenderer renderer = Renderers[guid];
      Renderers.Remove(guid);

      return renderer;
    }
  }
}
