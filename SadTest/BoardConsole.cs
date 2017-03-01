using System;
using Library1;
using Microsoft.FSharp.Core;
using SadConsole;
using SadConsole.Consoles;
using SadConsole.Input;
using SFML.Graphics;
using SFML.Window;
using Console = SadConsole.Consoles.Console;
using Font = SadConsole.Font;

namespace SadTest
{
    internal class BoardConsole : Console
    {
        private Tetris.GameState _state;

        public BoardConsole(int width, int height) : base(width, height)
        {
            Engine.ActiveConsole = this;
        }

        public BoardConsole(int width, int height, Font font) : base(width, height, font)
        {
        }

        public BoardConsole(ITextSurfaceRendered textData) : base(textData)
        {
        }

        public Tetris.GameState State
        {
            get { return _state; }
            set
            {
                _state = value;
                DrawBoard(_state.Board);
                DrawShape(_state.CurrentShape);
            }
        }

        public void Start()
        {
            State = Tetris.NewGameState;
        }

        public override bool ProcessKeyboard(KeyboardInfo info)
        {
            if (info.IsKeyDown(Keyboard.Key.Down))
            {
                State = Tetris.DoMoveDown(State);
            }
            else if (info.IsKeyDown(Keyboard.Key.Left))
            {
                State = Tetris.DoMoveLeft(State);
            }
            else if (info.IsKeyDown(Keyboard.Key.Right))
            {
                State = Tetris.DoMoveRight(State);
            }
            else if (info.IsKeyDown(Keyboard.Key.Up))
            {
                State = Tetris.DoRotate(State);
            }

            return false;
        }

        public void DrawBoard(Tetris.Tile[,] board)
        {
            for (var r = 0; r < board.GetLength(0); r++)
            {
                for (var c = 0; c < board.GetLength(1); c++)
                {
                    var tile = board[r, c];
                    if (tile.IsEmpty)
                    {
                        SetGlyph(r, c, ' ', Color.White, Color.Black);
                    }

                    if (tile.IsWall)
                    {
                        SetGlyph(r, c, 'X', Color.White, Color.Black);
                    }

                    if (tile.IsFilled)
                    {
                        var t = (Tetris.Tile.Filled) tile;
                        var tetro = t.Item;

                        var color = GetColorForTetromino(tetro);

                        SetGlyph(r, c, '#', color);
                    }
                }
            }
        }

        public void DrawShape(Tetris.Shape shape)
        {
            var color = GetColorForTetromino(shape.name);
            DrawBlock(shape.coords.Item1, shape.pos, color);
            DrawBlock(shape.coords.Item2, shape.pos, color);
            DrawBlock(shape.coords.Item3, shape.pos, color);
            DrawBlock(shape.coords.Item4, shape.pos, color);
        }

        private void DrawBlock(Tuple<int, int> coord, Tuple<int, int> pos, Color color)
        {
            var x = coord.Item1 + pos.Item1;
            var y = coord.Item2 + pos.Item2;
            SetGlyph(x, y, '#', color);
        }


        private Color GetColorForTetromino(Tetris.Tetromino tetro)
        {
            if (tetro.IsI)
                return new Color(53, 197, 249);
            if (tetro.IsJ)
                return new Color(15, 90, 158);
            if (tetro.IsL)
                return new Color(226, 146, 7);
            if (tetro.IsO)
                return new Color(235, 199, 49);
            if (tetro.IsS)
                return new Color(116, 236, 0);
            if (tetro.IsT)
                return new Color(172, 90, 216);
            if (tetro.IsZ)
                return new Color(180, 55, 9);

            //shouldn't happen
            return new Color();
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}