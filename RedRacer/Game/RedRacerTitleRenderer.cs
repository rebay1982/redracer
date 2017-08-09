using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  class RedRacerTitleRenderer : AbstractRenderer
  {
    private Sprite TitleScreen;
    private byte color = 0x00;

    public RedRacerTitleRenderer() : base()
    {
      Init();
    }
    
    protected override void Init()
    {
      // Don't hold onto the instance, we only need it in init.
      //TitleScreen = SpriteFactory.GetInstance().GetSpriteFromFile("RedRacer.png");

      IsInitialized = true;
    }

    public override void RenderToFrameBuffer(byte[] buffer, int width, int height)
    {
      //if (IsInitialized)
      //{
      //  //if (TitleScreen.Width < width)
      //  //{
      //  //
      //  //}
      //  //else
      //  {
      //    // Good only if sprite is the same width as buffer.
      //    Buffer.BlockCopy(
      //      TitleScreen.SpriteData,
      //      0,
      //      buffer,
      //      0,
      //      (TitleScreen.Width * TitleScreen.Height) << 2);

      //  }
      //}

      // Don't render anything if not initialized.
      if (IsInitialized)
      {
        for (int x = 0; x < width; x++)
        {
          for (int y = 0; y < height; y++)
          {
            int i = ((x * height) + y) << 2;
            buffer[i] = color;
            buffer[i + 1] = color;
            buffer[i + 2] = color;
            buffer[i + 3] = 0xFF;
          }
        }

        color++;
      }
    }
    
    public override void RenterToFrameBuffer(byte[] buffer, int width, int height, IGameState gameState)
    {
      RenderToFrameBuffer(buffer, width, height);
    }
  }
}
