using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using naves_game;

namespace NavesGame
{
    class Program
    {
        Ventana ventana;
        Nave nave;
        bool jugar = true;
        bool bossFinal = false;
        Enemigo enemigo1;
        Enemigo enemigo2;
        Enemigo boss1;

        public void Iniciar()
        {
            ventana = new Ventana(120, 100, ConsoleColor.Black, new Point(5, 5), new Point(112, 25));
            ventana.DibujarMarco();
            nave = new Nave(new Point(27, 20), ConsoleColor.White, ventana);
            nave.Dibujar();
            enemigo1 = new Enemigo(new Point(30, 10), ConsoleColor.Cyan, ventana, TipoEnemigo.Normal, nave);
            enemigo1.Dibujar();
            enemigo2 = new Enemigo(new Point(100, 7), ConsoleColor.Red, ventana, TipoEnemigo.Normal, nave);
            enemigo2.Dibujar();
            boss1 = new Enemigo(new Point(27, 7), ConsoleColor.Magenta, ventana, TipoEnemigo.Boss, nave);

            nave.Enemigos.Add(enemigo1);
            nave.Enemigos.Add(enemigo2);
            nave.Enemigos.Add(boss1);
        }

        public void Game()
        {
            while (jugar)
            {
                if (!enemigo1.Vivo && !enemigo2.Vivo){
                    bossFinal = true;
                }
                if (bossFinal)
                {
                    boss1.Mover();
                    boss1.Informacion(94);
                }
                else
                {
                    enemigo1.Mover();
                    enemigo1.Informacion(62);
                    enemigo2.Mover();
                    enemigo2.Informacion(78);
                }
 
                nave.Mover(2);
                nave.Disparar();
                if (nave.Vida <= 0)
                {
                    jugar = false;
                    nave.Muerte();
                }
            }
        }

        public void Run()
        {
            Iniciar();
            Game();
        }

        static void Main(string[] args)
        {
            Program programa = new Program();
            programa.Run();
        }
    }
}
