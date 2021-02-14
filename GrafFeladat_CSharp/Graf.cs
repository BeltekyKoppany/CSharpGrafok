using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        ///<param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }
        public void Torles(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Remove(new El(cs1, cs2));
            elek.Remove(new El(cs2, cs1));
        }


        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }

        public void SzelessegiBejar(int kezdopont) {

            HashSet<int> bejar = new HashSet<int>();
            Queue<int> sor = new Queue<int>();

            sor.Enqueue(kezdopont);
            bejar.Add(kezdopont);

            int k;
            while (sor != null)
            {
                k = sor.Dequeue();
                Console.WriteLine("Itt a csúcs: {0}", k);
                foreach (var el in elek)
                {
                    if (el.Csucs1 == k && !(bejar.Contains(el.Csucs2)))
                    {
                        sor.Enqueue(el.Csucs2);
                        bejar.Add(el.Csucs1);
                    }
                }
            }
        }

        public void MelysegBejar(int kezdopont)
        {

            HashSet<int> bejar = new HashSet<int>();
            Stack<int> verem = new Stack<int>();

            verem.Push(kezdopont);
            bejar.Add(kezdopont);

            int k;
            while (verem != null)
            {
                k = verem.Pop();
                Console.WriteLine("Itt a csúcs: {0}", k);
                foreach (var el in elek)
                {
                    if (el.Csucs1 == k && !(bejar.Contains(el.Csucs2)))
                    {
                        verem.Push(el.Csucs2);
                        bejar.Add(el.Csucs1);
                    }
                }
            }
        }

        public bool Osszefuggoseg() {
            HashSet<int> bejar = new HashSet<int>(); 
            Queue<int> sor = new Queue<int>();

            sor.Enqueue(0);
            bejar.Add(0);

            int k;
            while (sor.Count != 0)
            {
                k = sor.Dequeue();

                foreach (var el in elek)
                {
                    if (el.Csucs1 == k && bejar.Contains(el.Csucs2))
                    {
                        sor.Enqueue(el.Csucs2);
                        bejar.Add(el.Csucs2);
                    }
                }
            }

            if (sor.Count == csucsokSzama)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MohoSzinezes() {
            
            Dictionary<int, int> szinezes = new Dictionary<int, int>();

            int maxSzin = csucsokSzama;
            for (int i = 0; i < csucsokSzama-1; i++)
            {
                HashSet<int> szinekValaszt = new HashSet<int>();
                foreach (var el in elek)
                {
                    if (el.Csucs1 == i)
                    {
                        int szin;
                        if (szinezes.ContainsKey(el.Csucs2))
                        {
                            szin = szinezes[el.Csucs2];
                            szinekValaszt.Remove(szin);
                        }
                    }
                }
                int szinValasztott = Math.Min(szinekValaszt);
                szinezes.Add(csucsok[i], Csucs[i], szinValasztott);
                return szinezes;
            }

        }

    }
}