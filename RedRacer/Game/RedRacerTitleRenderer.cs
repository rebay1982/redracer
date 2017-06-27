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

    public override void RenderToFrameBuffer(byte[] buffer, int height, int width)
    {
      // Don't render anthing if not initialized.
      if (IsInitialized)
      {
        for (int i = 0; i < height * width; i += 4)
        {
          buffer[i] = color;
          buffer[i + 1] = color;
          buffer[i + 2] = color;
          buffer[i + 3] = 0xFF;
        }

        color += 0x01;
      }
    }
    
    public override void RenterToFrameBuffer(byte[] buffer, int height, int width, IGameState gameState)
    {
      RenderToFrameBuffer(buffer, height, width);
    }
  }
}
