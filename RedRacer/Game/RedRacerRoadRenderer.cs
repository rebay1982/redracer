using System;

namespace RedRacer.Game
{
  class RedRacerRoadRenderer : AbstractRenderer
  {
    private Sprite Road;
    private Sprite RoadDark;
    byte[][] RoadData = new byte[2][];

    int textureZ = 0;
    int dTextureZ = 0;
    int ddTextureZ = 2;

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
        int z = textureZ;
        int dz = 0;
        int ddz = 2;

        dTextureZ += ddTextureZ;
        if (dTextureZ > 1024)
          dTextureZ = 1024;

        textureZ += dTextureZ;
        
        // Draw line per line -- this will allow curving, hills and the whole nine yards
        for (int ScreenY = 479; ScreenY > 279; --ScreenY)
        {
          int gfxDataOffset = (ScreenY * Road.Width) << 2;

          Buffer.BlockCopy(
            RoadData[paletteIndex],
            gfxDataOffset,
            buffer,
            gfxDataOffset,
            Road.Width << 2);


          // Equiv to (z % 4096) > 2048 ? 1 : 0;
          paletteIndex = ((z & 0x1FFF) & 0x800) >> 11;

          dz += ddz;
          z += dz;
        }
      }
    }
  }
}
