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
        int y = 20;


        // Draw line per line -- this will allow curving, hills and the whole nine yards
        for (int i = 479; i > 279; --i)
        {
          int gfxDataOffset = (i * Road.Width) << 2;

          Buffer.BlockCopy(
            RoadData[paletteIndex & 0x01],
            gfxDataOffset,
            buffer,
            gfxDataOffset,
            Road.Width << 2);

          // This doesn't work well when we try to animate.  Will have to find
          // alternative solution.
          if (++z == y)
          {
            z -= y--;
            ++paletteIndex;
          }
        }
      }
    }
  }
}
