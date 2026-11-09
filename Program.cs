using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;


namespace _19241lab1
{
    internal class Program
    {
        static char VratiSoundExCode(char c) //pretvara u veliko slovo i kodira u broj
        {
            char velikoSlovo = char.ToUpper(c);
            if (velikoSlovo == 'B' || velikoSlovo == 'F' || velikoSlovo == 'P' || velikoSlovo == 'V')
                return '1';
            else if (velikoSlovo == 'C' || velikoSlovo == 'G' || velikoSlovo == 'J' || velikoSlovo == 'K' || velikoSlovo == 'Q' || velikoSlovo == 'S' || velikoSlovo == 'X' || velikoSlovo == 'Z')
                return '2';
            else if (velikoSlovo == 'D' || velikoSlovo == 'T')
                return '3';
            else if (velikoSlovo == 'L')
                return '4';
            else if (velikoSlovo == 'M' || velikoSlovo == 'N')
                return '5';
            else if (velikoSlovo == 'R')
                return '6';
            else if (velikoSlovo == 'A' || velikoSlovo == 'E' || velikoSlovo == 'I' || velikoSlovo == 'O' || velikoSlovo == 'U' || velikoSlovo == 'Y' || velikoSlovo == 'H' || velikoSlovo == 'W')
                return '0';
            else return '0';
        }

        static string GenerisiSoundEx(string rec)
        {
            if (rec == null)
                return "";
            else
            {
                char prethodnaCifra = VratiSoundExCode(rec[0]);
                string kodiranaRec = char.ToUpper(rec[0]).ToString();
                int count = 1;

                for (int i = 1; i < rec.Length; i++)
                {
                    char slovo = VratiSoundExCode(rec[i]);
                    if (slovo != '0' && slovo != prethodnaCifra)
                    {
                        kodiranaRec += slovo;
                        
                        ++count;
                    }

                    prethodnaCifra = slovo;
                }

                while (kodiranaRec.Length < 4)
                {
                    kodiranaRec += '0';
                }

                if (kodiranaRec.Length > 4)
                {
                    kodiranaRec = kodiranaRec.Substring(0, 4);
                }

                //if (count < 4)
                //{
                //    int duzina = 4 - kodiranaRec.Length;
                //    for (int i = 0; i < duzina; i++)
                //    {
                //        kodiranaRec += '0';
                //    }
                //}

                //if (count > 4)
                //{
                //    string newKodiranaRec = "";

                //    for (int i = 0; i < 4; i++)
                //    {
                //        newKodiranaRec += kodiranaRec[i];
                //    }

                //    kodiranaRec = newKodiranaRec;
                //}

                return kodiranaRec;
            }
        }

        static List<string> ucitajIzFajla(string imeFajla)
        {
            List<string> listaReci = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(imeFajla))
                {
                    string rec = "";
                    while ((rec = sr.ReadLine()) != null)
                    {
                        if (rec != null)
                        {
                            listaReci.Add(rec);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return listaReci;
        }

        static void dodajSoundKod(Dictionary<string, List<string>> parovi, string kod, string rec) //za dodavanje reci i njenog koda u parove
        {
            if (parovi.ContainsKey(kod))
            {
                parovi[kod].Add(rec);
            }
            else
            {
                parovi[kod] = new List<string>() { rec };
            }
        }

        static void Main(string[] args)
        {
            string filename = "100000reci_output.txt";
            List<string> reciIzFajla = ucitajIzFajla(filename);
            Stopwatch tajmer = new Stopwatch();

            //foreach(string rec in reciIzFajla)
            //{
            //    Console.WriteLine(rec);
            //}

            //foreach (var r in reciIzFajla)
            //{
            //    Console.WriteLine(r.ToString());
            //    string kodiranarec = GenerisiSoundEx(r);
            //    Console.WriteLine(kodiranarec);
            //}

            var ParoviSoundExReci = new Dictionary<string, List<string>>();

            tajmer.Start();

            foreach (var rec in reciIzFajla)
            {
                if (rec.Length >= 40 && rec.Length <= 60)
                {
                    string kodiranaRec = GenerisiSoundEx(rec);
                    dodajSoundKod(ParoviSoundExReci, kodiranaRec, rec);
                }
            }

            //foreach (var r in ParoviSoundExReci)
            //{
            //    Console.WriteLine($"{r.Key}: {string.Join(", ", r.Value)}");
            //}

            foreach (var kodiranaRec_Rec in ParoviSoundExReci)
            {
                List<string> slicneReci = kodiranaRec_Rec.Value;
                if (slicneReci.Count > 0)
                {
                    Console.WriteLine("SoundEx kod: " + kodiranaRec_Rec.Key + " " + "Reci: " + string.Join(", ", slicneReci));
                }
            }

            Console.WriteLine();

            tajmer.Stop();

            Console.WriteLine("Vreme izvrsenja: " + tajmer.Elapsed.TotalMilliseconds + " ms");
        }
    }
}