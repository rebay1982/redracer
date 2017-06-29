using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRacer.Game
{
  /// <summary>
  /// 
  /// Currently only supports ARGB 32bit format.
  /// 
  /// </summary>
  class Sprite
  {
    public int Width { get; private set; }
    public int Height { get; private set; }
    public byte[] SpriteData { get; private set; }

    public Sprite(int width, int height, byte[] spriteData)
    {
      this.Width = width;
      this.Height = height;

      Buffer.BlockCopy(spriteData, 0, this.SpriteData, 0, ((width * height) << 2));
    }
  }
}
