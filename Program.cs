using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2020JedlikMintaErettsegi
{
    class JelszóGeneráló
    {
        private Random Rnd;

        public JelszóGeneráló(Random r)
        {
            Rnd = r;
        }

        public string Jelszó(int jelszóHossz)
        {
            string jelszó = "";
            while (jelszó.Length < jelszóHossz)
            {
                char c = (char)Rnd.Next(48, 123);
                if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z'))
                {
                    jelszó += c;
                }
            }
            return jelszó;
        }
    }
    class Iskola
    {
        static List<Iskola> Adat = new List<Iskola>();
        public int Ev;
        public string Osztaly;
        public string DiakNeve;

        public Iskola(int ev, string osztaly, string diakNeve)
        {
            Ev = ev;
            Osztaly = osztaly;
            DiakNeve = diakNeve;
        }

        public static void MasodikFeladat(string fajl)
        {
            //2006;c;Bodnar Szilvia
            using (StreamReader olvas = new StreamReader(fajl))
            {
                while (!olvas.EndOfStream)
                {
                    string[] split = olvas.ReadLine().Split(';');
                    int ev = Convert.ToInt32(split[0]);
                    string osztaly = split[1];
                    string neve = split[2];

                    Iskola iskola = new Iskola(ev, osztaly, neve);

                    Adat.Add(iskola);
                }
            }
        }

        public static void HarmadikFeladat() => Console.WriteLine($"3. feladat: Az iskolába {Adat.Count} tanuló jár.");

        public static void NegyedikFeladat()
        {
            string neve = "";
            int max = 0;

            for (int i = 0; i < Adat.Count; i++)
            {
                string nevek = Adat[i].DiakNeve.Replace(" ", "");

                if (max < nevek.Length)
                {
                    max = nevek.Length;

                    neve = Adat[i].DiakNeve;
                }
            }

            Console.WriteLine($"4. feladat: A leghosszabb ({max} karakter) nevű tanuló(k):\n\t{neve}");
        }

        public static void OtodikFeladat()
        {
            string elso = Adat.Select(x => x.DiakNeve).First();
            string utolso = Adat.Select(x => x.DiakNeve).Last();

            string keszEJelszo = "";
            string keszUJelszo = "";

            for (int i = 0; i < Adat.Count; i++)
            {
                char evUtolso = Adat[i].Ev.ToString()[Adat[i].Ev.ToString().Length - 1];
                string osztaly = Adat[i].Osztaly;
                string[] split = Adat[i].DiakNeve.Split(' ');
                string vez = split[0];
                string ker = split[1];

                string vezH = vez.Substring(0, 3).ToLower();
                string kerH = ker.Substring(0, 3).ToLower();

                if (elso == Adat[i].DiakNeve)
                    keszEJelszo = $"{evUtolso + osztaly + vezH + kerH}";
                
                if(utolso == Adat[i].DiakNeve)
                    keszUJelszo = $"{evUtolso + osztaly + vezH + kerH}";
            }

            Console.WriteLine($"5. feladat: Azonosítók:\n\tElső: {elso} - {keszEJelszo}\n\tUtolsó: {utolso} - {keszUJelszo}");
        }

        public static void HatodikFeladat()
        {
            Console.Write("6. feladat: Kérek egy azonosítót [pl.: 4dvavkri]: ");
            string megadottAzon = Console.ReadLine();

            List<string> adatok = new List<string>();

            string diakAdat = "";

            string talalt = "";

            for (int i = 0; i < Adat.Count; i++)
            {
                char evUtolso = Adat[i].Ev.ToString()[Adat[i].Ev.ToString().Length - 1];
                string osztaly = Adat[i].Osztaly;
                string[] split = Adat[i].DiakNeve.Split(' ');
                string vez = split[0];
                string ker = split[1];

                string vezH = vez.Substring(0, 3).ToLower();
                string kerH = ker.Substring(0, 3).ToLower();

                talalt = $"{Adat[i].Ev} {Adat[i].Osztaly} {Adat[i].DiakNeve}:{evUtolso + osztaly + vezH + kerH}";

                adatok.Add(talalt);
            }

            for (int i = 0; i < adatok.Count; i++)
            {
                string[] split = adatok[i].Split(':');
                string diakAdatok = split[0];
                string azon = split[1];

                if (azon == megadottAzon)
                    diakAdat = diakAdatok;
            }

            Console.WriteLine($"\t{diakAdat}");
        }

        public static void HetedikFeladat()
        {
            Random rnd = new Random();
            JelszóGeneráló gen = new JelszóGeneráló(rnd);

            int i = rnd.Next(Adat.Count);

            string nev = Adat[i].DiakNeve;

            Console.WriteLine($"7. feladat: Jelszó generálása: \n\t{nev} - {gen.Jelszó(8)}");
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Feladatok();

            Console.ReadKey();
        }

        private static void Feladatok()
        {
            Iskola.MasodikFeladat("nevek.txt");
            Iskola.HarmadikFeladat();
            Iskola.NegyedikFeladat();
            Iskola.OtodikFeladat();
            Iskola.HatodikFeladat();
            Iskola.HetedikFeladat();
        }
    }
}
