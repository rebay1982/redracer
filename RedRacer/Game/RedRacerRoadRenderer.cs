using System;

namespace RedRacer.Game
{
  class RedRacerRoadRenderer : AbstractRenderer
  {
    private Sprite Road;
    private Sprite RoadDark;


    public RedRacerRoadRenderer() : base()
    {
      Init();
    }

    protected override void Init()
    {
      Road = SpriteFactory.GetInstance().GetSprite("Road.png");
      RoadDark = SpriteFactory.GetInstance().GetSprite("RoadDark.png");

      // Nothing to initialize.
      IsInitialized = true;
    }

    public override void RenderToFrameBuffer(byte[] buffer, int width, int height)
    {
      throw new NotImplementedException();
    }

    public override void RenderToFrameBuffer(byte[] buffer, int width, int height, IGameState gameState)
    {
      if (IsInitialized)
      {

        Boolean isDarkLine = false;
        for (int z = 480; z > 280; z -= 20)
        {
          byte colorFilter = isDarkLine ? (byte)0x0F : (byte)0xFF;




          isDarkLine = !isDarkLine;
        }


        // Render the road here.
        Buffer.BlockCopy(
          Road.SpriteData,
          0,
          buffer,
          0,
          (Road.Width * Road.Height) << 2);
      }
    }
  }
}
