using System;

namespace RedRacer.Game
{
  class RedRacerRoadRenderer : AbstractRenderer
  {
    private Sprite Road;
    private Sprite RoadDark;
    byte[][] RoadData = new byte[2][];

    public RedRacerRoadRenderer() : base()
    {
      Init();
    }

    protected override void Init()
    {
      Road = SpriteFactory.GetInstance().GetSprite("Road.png");
      RoadDark = SpriteFactory.GetInstance().GetSprite("RoadDark.png");

      RoadData[0] = Road.SpriteData;
      RoadData[1] = RoadDark.SpriteData;

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

        int paletIndex = 0;
        for (int z = 480; z > 280; z -= 20)
        {
          int gfxDataOffset = ((z - 20) * Road.Width) << 2;

          Buffer.BlockCopy(
            RoadData[++paletIndex & 0x01],
            gfxDataOffset,
            buffer,
            gfxDataOffset,
            (20 * Road.Width) << 2);
        }


        //// Render the road here.
        //Buffer.BlockCopy(
        //  Road.SpriteData,
        //  0,
        //  buffer,
        //  0,
        //  (Road.Width * Road.Height) << 2);
      }
    }
  }
}
