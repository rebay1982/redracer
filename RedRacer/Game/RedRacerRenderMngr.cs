using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  class RedRacerRenderMngr : IRenderMngr
  {
    private Dictionary<Guid, IRenderer> Renderers;

    private byte[] ActiveBuffer;
    private byte[] ShadowBuffer;

    public RedRacerRenderMngr()
    {
      Renderers = new Dictionary<Guid, IRenderer>();
      ActiveBuffer = new byte[(320 * 200) << 2];
      ShadowBuffer = new byte[(320 * 200) << 2];
    }

    // IRenderMngr
    public byte[] GetFrameBuffer()
    {
      // TODO: Don't return the internal FB.  Create a copy -- check how much this costs.
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
      ShadowBuffer = switchBuffer;
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
