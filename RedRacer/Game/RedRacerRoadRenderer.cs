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
        // Will need to dehardcode this stuff.
        int paletteIndex = 0;
        int z = 0;
        int y = 480;


        while (y > 280)
        {
          int gfxDataOffset = ((y - (20 - z)) * Road.Width) << 2;

          Buffer.BlockCopy(
            RoadData[++paletteIndex & 0x01],
            gfxDataOffset,
            buffer,
            gfxDataOffset,
            ((20 - z) * Road.Width) << 2);

          y -= (20 - z++);
          
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
