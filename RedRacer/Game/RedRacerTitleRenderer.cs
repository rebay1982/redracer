using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  class RedRacerTitleRenderer : AbstractRenderer
  {
    private byte color = 0x00;

    public RedRacerTitleRenderer() : base()
    {
      Init();
    }
    
    protected override void Init()
    {
      IsInitialized = true;
    }

    public override void RenderToFrameBuffer(byte[] buffer, int width, int height)
    {
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
