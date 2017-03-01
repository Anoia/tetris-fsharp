using System;
using Library1;
using SadConsole;
using SFML.System;

namespace SadTest
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Engine.Initialize("IBM.font", Tetris.gameDimensions.Item1, Tetris.gameDimensions.Item2);

            Engine.EngineStart += Engine_EngineStart;
            Engine.EngineUpdated += Engine_EngineUpdated;

            Engine.Run();
        }

        private static void Engine_EngineStart(object sender, EventArgs e)
        {
            Engine.ConsoleRenderStack.Clear();
            Engine.ActiveConsole = null;

            var viewConsole = new BoardConsole(Tetris.gameDimensions.Item1, Tetris.gameDimensions.Item2);
            viewConsole.Position = new Vector2i(0, 0);

            Engine.ConsoleRenderStack.Add(viewConsole);

            viewConsole.Start();
        }

        private static void Engine_EngineUpdated(object sender, EventArgs e)
        {
            // is called every frame
        }
    }
}