using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace naves_game
{
    internal class Ventana
    {
        public int Ancho { get; set; }
        public int Altura { get; set; }
        public ConsoleColor Color { get; set; }
        public Point limiteSuperior { get; set; }
        public Point limiteInferior { get; set; }
        public Ventana(int ancho, int altura,ConsoleColor color, 
            Point Limitesuperior, Point Limiteinferior)
        {
            Color = color;
            Ancho = ancho;
            Altura = altura;
            limiteInferior = Limiteinferior;
            limiteSuperior = Limitesuperior;
            Init();
        }
        private void Init()
        {
            Console.SetWindowSize(Ancho, Altura);
            Console.Title = "Nave";
            Console.BackgroundColor = Color;
            Console.Clear();
            Console.CursorVisible = false;
        }
        public void DibujarMarco()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = limiteSuperior.X; i <= limiteInferior.X; i++)
            {
                Console.SetCursorPosition(i, limiteInferior.Y);
                Console.Write("═");
                Console.SetCursorPosition(i, limiteSuperior.Y);  
                Console.Write("═");
                
            }
            for (int i = limiteSuperior.Y; i <= limiteInferior.Y; i++)
            {
                Console.SetCursorPosition(limiteSuperior.X, i);
                Console.Write("║");
                Console.SetCursorPosition(limiteInferior.X, i);
                Console.Write("║");
            }

            Console.SetCursorPosition(limiteSuperior.X, limiteSuperior.Y);
            Console.Write("╔");
            Console.SetCursorPosition(limiteSuperior.X, limiteInferior.Y);
            Console.Write("╚");
            Console.SetCursorPosition(limiteInferior.X, limiteSuperior.Y);
            Console.Write("╗");
            Console.SetCursorPosition(limiteInferior.X, limiteInferior.Y);
            Console.Write("╝");
               
        }
    }
}


