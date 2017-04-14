﻿using RedRacer.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    private readonly IGame gameMngr;

    private readonly IInput inputMngr;
    private readonly IRenderer renderMngr;

    private bool Quit = false;


    public MainPage()
    {
      this.InitializeComponent();

      // Initialize managers.
      gameMngr = new RedRacerGame();
      
      //Run();
      		
    }

    public void Run()
    {
      while (!Quit)
      {
        // Manage inputs here.

        // Update the game engine.
        gameMngr.Update(0);

        // Render the picture
        renderMngr.Render();
      }
    }

		private void canvas_Draw(Microsoft.Graphics.Canvas.UI.Xaml.CanvasControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasDrawEventArgs args)
		{

		}
	}
}
