using System;
using System.Linq;
using Library1;
using SadConsole;
using Console = SadConsole.Consoles.Console;
using SadConsole.Consoles;
using SadConsole.SerializedTypes;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;

namespace SadTest
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Setup the engine and creat the main window.
            SadConsole.Engine.Initialize("IBM.font", Tetris.gameDimensions.Item1, Tetris.gameDimensions.Item2);          

            // Hook the start event so we can add consoles to the system.
            SadConsole.Engine.EngineStart += Engine_EngineStart;

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Engine.EngineUpdated += Engine_EngineUpdated;

            // Start the game.
            SadConsole.Engine.Run();
            
            
        }

        private static void Engine_EngineStart(object sender, EventArgs e)
        {
            SadConsole.Engine.ConsoleRenderStack.Clear();
            SadConsole.Engine.ActiveConsole = null;

            var viewConsole = new BoardConsole(Tetris.gameDimensions.Item1, Tetris.gameDimensions.Item2);
            viewConsole.Position = new Vector2i(0, 0);

            Engine.ConsoleRenderStack.Add(viewConsole);

            viewConsole.Start();

            //       viewConsole.Print(1, 1, "Welcome to SadConsole", ColorHelper.Aqua, ColorHelper.Black);
        }

        //private static int _counter = 0;
        private static void Engine_EngineUpdated(object sender, EventArgs e)
        {
            //var defaultConsole = (Console)SadConsole.Engine.ConsoleRenderStack.First();
            //defaultConsole.Print(1, 2, _counter.ToString(), Color.Red);
            //_counter++;
        }
    }
}
