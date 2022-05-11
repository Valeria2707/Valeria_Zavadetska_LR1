using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace лаба11
{
    class Program
    {
        class Polinomial
        {
            private List<Monomial> monomials;

            public Polinomial(List<Monomial> monomials)
            {
                this.monomials = monomials;
            }

            public List<Monomial> Monomials
            { 
                get => monomials;
                set => monomials = value;
            }

            public Polinomial sum(Polinomial other)
            {
                List<Monomial> result = new List<Monomial>();
                for (int i = 0; i < Monomials.Count; i++)
                {
                    Monomial left = Monomials[i];
                    Monomial right = other.Monomials[i];

                    Monomial sum = new Monomial(left.Power, left.Name, left.Coeficient + right.Coeficient);
                    result.Add(sum);
                }
                
                return new Polinomial(result);
            }

            public Polinomial subtraction(Polinomial other)
            {
                List<Monomial> result = new List<Monomial>();
                for (int i = 0; i < Monomials.Count; i++)
                {
                    Monomial left = Monomials[i];
                    Monomial right = other.Monomials[i];

                    Monomial sum = new Monomial(left.Power, left.Name, left.Coeficient - right.Coeficient);
                    result.Add(sum);
                }

                return new Polinomial(result);
            }

            public bool contains(Monomial monomial)
            {
                int index = Monomials.FindIndex(e => e.Equals(monomial));
                return index != -1;
            }

            public Polinomial multiply(Polinomial other)
            {
                List<Monomial> result = new List<Monomial>();
                for (int i = 0; i < Monomials.Count; i++)
                {
                    Monomial left = Monomials[i];
                    Monomial right = other.Monomials[i];

                    Monomial sum = new Monomial(left.Power + right.Power, left.Name, left.Coeficient * right.Coeficient);
                    result.Add(sum);
                }

                return new Polinomial(result);
            }

            public Polinomial divide(Polinomial other)
            {
                List<Monomial> result = new List<Monomial>();
                for (int i = 0; i < Monomials.Count; i++)
                {
                    Monomial left = Monomials[i];
                    Monomial right = other.Monomials[i];

                    Monomial sum = new Monomial(left.Power - right.Power, left.Name, left.Coeficient / right.Coeficient);
                    result.Add(sum);
                }

                return new Polinomial(result);
            }

            public bool  equals(Polinomial other)
            {
                for (int i = 0; i < Monomials.Count; i++)
                {
                    Monomial left = Monomials[i];
                    Monomial right = other.Monomials[i];

                    if (!left.Equals(right))
                    {
                        return false;
                    }
                }

                return true;
            }

            public Polinomial compact()
            {
                List<Monomial> result = new List<Monomial>();
                Dictionary<int, Boolean> taken = new Dictionary<int, bool>();
                for (int i = 0; i < Monomials.Count; i++)
                {
                    Monomial m = Monomials[i];

                    Monomial r = new Monomial(m.Power, m.Name, m.Coeficient);
                    for (int j = i + 1; j < Monomials.Count; j++)
                    {
                        Monomial other = Monomials[j];

                        if (!taken[j] && m.Name == other.Name && m.Power == other.Power)
                        {
                            r.Coeficient += other.Coeficient;
                            taken[j] = true;
                        }
                    }

                    result.Add(r);
                }

                return new Polinomial(result);
            }

            public void print()
            {
                Console.WriteLine("----------------------");
                Console.WriteLine("Polinomial");
                for (int i = 0; i < Monomials.Count; i++)
                {
                    Console.Write(Monomials[i].ToString());
                    Console.WriteLine();
                }
                Console.WriteLine("----------------------");
            }

            public void writeToFile(string filename)
            {
                string toWrite = JsonConvert.SerializeObject(this);
                File.WriteAllText(filename, toWrite);
            }

            public static Polinomial readFromFile(string filename)
            {
                StreamReader r = new StreamReader(filename);
                string jsonString = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Polinomial>(jsonString);
            }

        }

        class Monomial
        {
            private int power;
            private string name;
            private int coeficient;

            public Monomial(int power, string name, int coeficient)
            {
                Power = power;
                Name = name;
                Coeficient = coeficient;
            }

            public int Power 
            { 
                get => power;
                set => power = value; 
            }
            public string Name 
            {
                get => name;
                set => name = value; 
            }
            public int Coeficient
            { get => coeficient;
              set => coeficient = value;
            }

            override public string ToString()
            {
                return $"Monomial, Power = {Power}, Name = {Name}, Coeficient = {Coeficient}";
            }
        }

        static void Main(string[] args)
        {
           
            

            Polinomial p = new Polinomial(new List<Monomial> { new Monomial(2, "a", 5), new Monomial(2, "a", 8) });

            p.writeToFile("file-1.json");

            Polinomial p1 = Polinomial.readFromFile("file-1.json");
            p1.print();
         
            Polinomial p2 = new Polinomial(new List<Monomial> { new Monomial(2, "a", 2), new Monomial(2, "a", 10) });

            Polinomial p3 = new Polinomial(new List<Monomial> { new Monomial(5, "a", 10), new Monomial(2, "b", 5), new Monomial(6, "a", 10) });

            Polinomial p4 = new Polinomial(new List<Monomial> { new Monomial(9, "a", 11), new Monomial(2, "b", 7), new Monomial(6, "a", 13) });






            Polinomial p5 = p.sum(p2);
            Console.WriteLine("Suma: ");
            p5.print();
 
            Polinomial p6 = p2.subtraction(p);
            Console.WriteLine("subtraction: ");
            p6.print();


            Polinomial p7 = p3.multiply(p4);
            Console.WriteLine("multiply: ");
            p7.print();


            Polinomial p8 = p4.divide(p3);
            Console.WriteLine("divide: ");
            p8.print();

           

            bool p9 = p.equals(p2);
            Console.WriteLine($" equals: {p9}");


           

        }
    }


}