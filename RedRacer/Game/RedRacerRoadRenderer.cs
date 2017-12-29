using System;

namespace RedRacer.Game
{
  class RedRacerRoadRenderer : AbstractRenderer
  {

    public RedRacerRoadRenderer() : base()
    {
      Init();
    }

    protected override void Init()
    {
      // Nothing to initialize.
      IsInitialized = true;
    }

    public override void RenderToFrameBuffer(byte[] buffer, int width, int height)
    {
      throw new NotImplementedException();
    }

    public override void RenterToFrameBuffer(byte[] buffer, int width, int height, IGameState gameState)
    {
      if (IsInitialized)
      {
        // Render the road here.

      }
    }
  }
}
