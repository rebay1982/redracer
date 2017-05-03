using Microsoft.Graphics.Canvas;
using RedRacer.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RedRacer
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    // Assortment of managers.
    private readonly IGameMngr gameMngr;
    private readonly IInputMngr inputMngr;
    private readonly IRenderMngr renderMngr;


    private bool Quit = false;

    public MainPage()
    {
      this.InitializeComponent();
      //canvas.ClearColor = Color.FromArgb(255, 0, 0, 0);
      // Initialize managers.
      gameMngr = new RedRacerGame();
      
      //Run();
    }

    /// <summary>
    // Using game loop inspired from
    // http://gafferongames.com/game-physics/fix-your-timestep/
    // http://gameprogrammingpatterns.com/game-loop.html
    /// </summary>
    public void Run()
    {
      const double timeStep = 10;   // MS

      double currentTime = Utils.GetCurrentTimeStamp();
      double timeAccumulator = 0;   // MS

      // Initial gamestate.
      IGameState previousState = gameMngr.GetGameState();

      while (!Quit)
      {
        double newTime = Utils.GetCurrentTimeStamp();
        double frameTime = newTime - currentTime;

        // Clamp frameTime to 250ms.
        if (frameTime > 250)
          frameTime = 250;

        currentTime = newTime;
        timeAccumulator += frameTime;

        // Update user input state.
        inputMngr.Update();

        while(timeAccumulator >= timeStep)
        {
          // I've got a feeling this extrapolation of states is overkill for the job at hand.
          previousState = gameMngr.GetGameState();
          gameMngr.Update(inputMngr.GetInputState(), timeStep);

          timeAccumulator -= timeStep;
        }

        double alpha = timeAccumulator / timeStep;
        IGameState extrapolatedState = gameMngr.ExtrapolateGameState(
          inputMngr.GetInputState(),
          previousState,
          alpha);

        // Render the picture
        renderMngr.Render(extrapolatedState);
      }
    }

		private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
		{
      byte[] imgBytes = new byte[40000];

      for (int i = 0; i < 40000; ++i)
      {
        imgBytes[i] = (byte)((i % 4 == 3) ? 0xFF : 0x00);
      }

      Color.FromArgb(255, 0, 0, 0);
      CanvasBitmap bitmap = CanvasBitmap.CreateFromBytes(sender.Device, imgBytes, 100, 100, Windows.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized);
      args.DrawingSession.DrawImage(bitmap, 100, 100);
    }
	}
}
