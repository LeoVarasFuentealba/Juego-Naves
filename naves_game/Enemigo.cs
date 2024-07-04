using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace naves_game
{
    public enum TipoEnemigo
    {
        Normal, Boss
    }
    internal class Enemigo
    {
        enum Direccion
        {
            Derecha , Izquierda, Arriba, Abajo
        }
        public bool Vivo { get; set; }
        public float Vida { get; set; }
        public Point Posicion { get; set; }
        public Ventana VentanaC { get; set; }
        public ConsoleColor Color { get; set; }
        public TipoEnemigo TipoEnemigoE { get; set; }
        public List<Point> PosicionesEnemigo { get; set; }
        public List<Bala> Balas { get; set; }
        private Direccion direccion_act;
        private DateTime tiempo_direccion;
        private float tiempo_direccion_random;
        private DateTime tiempo_movimiento;
        private DateTime tiempo_bala;
        private float tiempo_bala_random;

        public Enemigo(Point posicion, ConsoleColor color, Ventana ventana,
            TipoEnemigo tipoEnemigo)
        {
            Posicion = posicion;
            Color = color;
            VentanaC = ventana;
            TipoEnemigoE = tipoEnemigo;
            Vida = 100;
            Vivo = true;
            PosicionesEnemigo = new List<Point>();
            direccion_act = Direccion.Derecha;
            tiempo_direccion = DateTime.Now;
            tiempo_direccion_random = 1000;
            tiempo_movimiento = DateTime.Now;
            Balas = new List<Bala>();
            tiempo_bala = DateTime.Now;
            tiempo_bala_random = 200;
        }
        public void Dibujar()
        {
            switch (TipoEnemigoE)
            {
                case TipoEnemigo.Normal:
                    DibujoNormal();
                    break;
                case TipoEnemigo.Boss:
                    DibujoBoss();
                    break;
            }
        }
        public void DibujoNormal()
        {
            int y, x;

            Console.ForegroundColor = Color;
            x = Posicion.X;
            y = Posicion.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("▄▄");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");

            PosicionesEnemigo.Clear();

            PosicionesEnemigo.Add(new Point(x + 1, y));
            PosicionesEnemigo.Add(new Point(x + 2, y));
            PosicionesEnemigo.Add(new Point(x, y+1));
            PosicionesEnemigo.Add(new Point(x + 1, y+1));
            PosicionesEnemigo.Add(new Point(x + 2, y+1));
            PosicionesEnemigo.Add(new Point(x + 3, y+1));
            PosicionesEnemigo.Add(new Point(x, y+2));
            PosicionesEnemigo.Add(new Point(x + 3, y+2));
        }

        public void DibujoBoss()
        {
            int y, x;

            Console.ForegroundColor = Color;
            x = Posicion.X;
            y = Posicion.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("█▄▄▄▄█");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("██ ██ ██");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("█▀▀▀▀▀▀█");

            PosicionesEnemigo.Clear();

            PosicionesEnemigo.Add(new Point(x + 1, y));
            PosicionesEnemigo.Add(new Point(x + 2, y));
            PosicionesEnemigo.Add(new Point(x + 3, y));
            PosicionesEnemigo.Add(new Point(x + 4, y)); 
            PosicionesEnemigo.Add(new Point(x + 5, y));
            PosicionesEnemigo.Add(new Point(x + 6, y));

            PosicionesEnemigo.Add(new Point(x, y + 1));
            PosicionesEnemigo.Add(new Point(x + 1, y + 1));
            PosicionesEnemigo.Add(new Point(x + 2, y + 1));
            PosicionesEnemigo.Add(new Point(x + 3, y + 1));
            PosicionesEnemigo.Add(new Point(x + 4, y + 1));
            PosicionesEnemigo.Add(new Point(x + 5, y + 1));
            PosicionesEnemigo.Add(new Point(x + 6, y + 1));

            PosicionesEnemigo.Add(new Point(x, y + 2));
            PosicionesEnemigo.Add(new Point(x + 1, y + 2));
            PosicionesEnemigo.Add(new Point(x + 2, y + 2));
            PosicionesEnemigo.Add(new Point(x + 3, y + 2));
            PosicionesEnemigo.Add(new Point(x + 4, y + 2));
            PosicionesEnemigo.Add(new Point(x + 5, y + 2));
            PosicionesEnemigo.Add(new Point(x + 6, y + 2));
            PosicionesEnemigo.Add(new Point(x + 7, y + 2));
        }

        public void Borrar()
        {
            foreach(Point item in PosicionesEnemigo)
            {
                Console.SetCursorPosition(item.X, item.Y);
                Console.Write(" ");
            }
        }
        public void Mover()
        {
            int tiempo = 60;
            if (TipoEnemigoE == TipoEnemigo.Boss)
            {
                tiempo = 50;
            }
            if(DateTime.Now> tiempo_movimiento.AddMilliseconds(tiempo))
            {
                Borrar();
                DireccionAleatoria();
                Point posicion_aux = Posicion;
                Movimiento(ref posicion_aux);
                Colisiones(posicion_aux);
                Dibujar();
                tiempo_movimiento = DateTime.Now;
            }
            CrearBalas();
            Disparar();
        }
        public void Colisiones(Point posicionAux) 
        {
            int ancho = 3;
            if(TipoEnemigoE == TipoEnemigo.Boss)
            {
                ancho = 7;
            }
            if (posicionAux.X <= VentanaC.limiteSuperior.X)
            {
                direccion_act = Direccion.Derecha;
                posicionAux.X = VentanaC.limiteSuperior.X + 1;
            }
            if (posicionAux.X+ancho >= VentanaC.limiteInferior.X)
            {
                direccion_act = Direccion.Izquierda;
                posicionAux.X = VentanaC.limiteInferior.X - 1 - ancho;
            }
            if (posicionAux.Y<= VentanaC.limiteSuperior.Y)
            {
                direccion_act = Direccion.Abajo;
                posicionAux.Y = VentanaC.limiteSuperior.Y + 1;
            }
            if (posicionAux.Y+2 >= VentanaC.limiteSuperior.Y + 9)
            {
                direccion_act = Direccion.Arriba;
                posicionAux.Y = VentanaC.limiteSuperior.Y + 7;
            }

            Posicion = posicionAux;
        }
        public void Movimiento(ref Point posicion_aux)
        {
            switch (direccion_act)
            {
                case Direccion.Derecha:
                    posicion_aux.X += 1;
                    break;
                case Direccion.Izquierda:
                    posicion_aux.X -= 1;
                    break;
                case Direccion.Arriba:
                    posicion_aux.Y -= 1;
                    break;
                case Direccion.Abajo:
                    posicion_aux.Y += 1;
                    break;
            }
        }
        public void DireccionAleatoria()
        {

            if (DateTime.Now > tiempo_direccion.AddMilliseconds(100)
                && (direccion_act == Direccion.Arriba || direccion_act == Direccion.Abajo))
            {
                Random random = new Random();
                int numAleatorio = random.Next(1,3);

                switch (numAleatorio)
                {
                    case 1:
                        direccion_act = Direccion.Derecha;
                        break;
                    case 2:
                        direccion_act = Direccion.Izquierda;
                        break;
                }

                tiempo_direccion = DateTime.Now;
            }

            if (DateTime.Now > tiempo_direccion.AddMilliseconds(tiempo_direccion_random)
                && (direccion_act == Direccion.Derecha || direccion_act == Direccion.Izquierda))
            {
                Random random = new Random();
                int numAleatorio = random.Next(1, 5);

                switch (numAleatorio)
                {
                    case 1:
                        direccion_act = Direccion.Derecha;
                        break;
                    case 2:
                        direccion_act = Direccion.Izquierda;
                        break;
                    case 3:
                        direccion_act = Direccion.Arriba;
                        break;
                    case 4:
                        direccion_act = Direccion.Abajo;
                        break;
                }

                tiempo_direccion = DateTime.Now;
                tiempo_direccion_random = random.Next(1000, 2000);
            }
        }
        public void CrearBalas()
        {
            if(DateTime.Now > tiempo_bala.AddMilliseconds(tiempo_bala_random))
            {
                Random random = new Random();
                if (TipoEnemigoE == TipoEnemigo.Normal)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 1, Posicion.Y + 2), Color, TipoBala.Enemigo);

                    Balas.Add(bala);
                    tiempo_bala_random = random.Next(500, 700);
                }
                if (TipoEnemigoE == TipoEnemigo.Boss)
                {
                    Bala bala = new Bala(new Point(Posicion.X + 4, Posicion.Y + 2), Color, TipoBala.Enemigo);

                    Balas.Add(bala);
                    tiempo_bala_random = random.Next(100, 150);
                }
                tiempo_bala = DateTime.Now;
            }
        }

        public void Disparar()
        {
            for (int i = 0; i < Balas.Count; i++)
            {
                if (Balas[i].Mover(1, VentanaC.limiteInferior.Y))
                {
                    Balas.Remove(Balas[i]);
                }
               
            }
        }

        public void Informacion(int distanciaX)
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(VentanaC.limiteSuperior.X + distanciaX, VentanaC.limiteSuperior.Y - 1);
            Console.Write("Enemigo: " + (int) Vida + " %  ");
        }
    }
}
