using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  public delegate void RendererFrameReady();

  class RedRacerRenderMngr : IRenderMngr
  {
    // PUBLIC
    public event RendererFrameReady FrameReadyEvent;



    // PRIVATE
    private const int BUFFER_WIDTH = 320;
    private const int BUFFER_HEIGHT = 200;

    private Dictionary<Guid, IRenderer> Renderers;

    private byte[] ActiveBuffer;
    private byte[] ShadowBuffer;

    private object syncRoot = new object();



    // PUBLIC
    public RedRacerRenderMngr()
    {
      Renderers = new Dictionary<Guid, IRenderer>();
      ActiveBuffer = new byte[(BUFFER_WIDTH * BUFFER_HEIGHT) << 2];
      ShadowBuffer = new byte[(BUFFER_WIDTH * BUFFER_HEIGHT) << 2];
    }

    // IRenderMngr
    public byte[] GetFrameBuffer()
    {
      // TODO: Don't return the internal FB.  Create a copy -- check how much this costs (speed wise).
      byte[] returnedBuffer = new byte[(BUFFER_WIDTH * BUFFER_HEIGHT) << 2];

      // Lock to avoid it being switched in the middle of copying.
      lock (syncRoot) // Maybe not the best of ideas to have the renderer wait on this?
      {
        Buffer.BlockCopy(ActiveBuffer, 0, returnedBuffer, 0, ((BUFFER_WIDTH * BUFFER_HEIGHT) << 2));
      }

      return returnedBuffer;
    }

    public void Render(IGameState gameState)
    {
      foreach (IRenderer renderer in Renderers.Values)
      {
        renderer.RenderToFrameBuffer(ShadowBuffer, BUFFER_WIDTH, BUFFER_HEIGHT);
      }

      SwitchBuffers();

      // Fire event to notify of new frame buffer ready if someone is listening.
      FrameReadyEvent?.Invoke();
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



    // PRIVATE
    private void SwitchBuffers()
    {
      lock (syncRoot)
      {
        byte[] switchBuffer = ActiveBuffer;
        ActiveBuffer = ShadowBuffer;
        ShadowBuffer = switchBuffer;
      }
    }
  }
}
