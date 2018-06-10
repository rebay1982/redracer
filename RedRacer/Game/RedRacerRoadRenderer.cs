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
    int ddTextureZ = 1;

    // 200 = screen space reserved for drawing the road.
    int[] zmap = new int[200];

    public RedRacerRoadRenderer() : base()
    {
      Init();
    }

    protected override void Init()
    {
      Road = SpriteFactory.GetInstance().GetSprite("RoadWide.png");
      RoadDark = SpriteFactory.GetInstance().GetSprite("RoadWideDark.png");

      RoadData[0] = Road.GetCroppedSpriteData(640, 0, 640, 480);
      RoadData[1] = RoadDark.GetCroppedSpriteData(640, 0, 640, 480);

      //RoadData[0] = Road.SpriteData;
      //RoadData[1] = RoadDark.SpriteData;

      InitZMap();

      // Nothing to initialize.
      IsInitialized = true;
    }

    public override void RenderToFrameBuffer(byte[] buffer, int width, int height)
    {
      throw new NotImplementedException();
    }

    public override void RenderToFrameBuffer(byte[] buffer, int width, int height, IGameState gameState)
    {
      // Render only if initialized
      if (IsInitialized)
      {
        textureZ += dTextureZ;
        if (dTextureZ < 5)
        {
          dTextureZ += ddTextureZ;
        }

        int zmapSize = zmap.Length + 279;

        // Draw line per line -- this will allow curving, hills and the whole nine yards
        for (int y = 479; y > 279; --y)
        {
          int gfxDataOffset = (y * width) << 2;
          int z = textureZ - zmap[y - 280];
          Buffer.BlockCopy(
            RoadData[(z / 50) & 0x01],
            gfxDataOffset,
            buffer,
            gfxDataOffset,
            width << 2);
        }
      }
    }

    private void InitZMap()
    {
      int YrH = 480 >> 1;
      int Yw = -400;    // Negative because the road is at a negative Y in relation to the
                        // camera.

      for (int Ys = 280; Ys < 480; ++Ys)
      {
        zmap[Ys - 280] = (Yw * 100) / (Ys - YrH);
      }

      // Diagnostics
      for (int y = 0; y < 200; ++y)
      {
        System.Diagnostics.Debug.Write("ZMap index: " + y + " is " + zmap[y] + "\n");
      }
    }
  }
}
