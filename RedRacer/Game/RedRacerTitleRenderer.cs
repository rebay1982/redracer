using System;

namespace RedRacer.Game
{
  class RedRacerTitleRenderer : AbstractRenderer
  {
    private Sprite TitleScreen;

    public RedRacerTitleRenderer() : base()
    {
      Init();
    }
    
    protected override void Init()
    {
      // Don't hold onto the instance, we only need it in init.
      TitleScreen = SpriteFactory.GetInstance().GetSprite("RedRacer.png");

      IsInitialized = true;
    }

    public override void RenderToFrameBuffer(byte[] buffer, int width, int height)
    {
      if (IsInitialized)
      {
        // Good only if sprite is the same width as buffer.
        Buffer.BlockCopy(
          TitleScreen.SpriteData,
          0,
          buffer,
          0,
          (TitleScreen.Width * TitleScreen.Height) << 2);

      }
    }
    
    public override void RenderToFrameBuffer(byte[] buffer, int width, int height, IGameState gameState)
    {
      RenderToFrameBuffer(buffer, width, height);
    }
  }
}
