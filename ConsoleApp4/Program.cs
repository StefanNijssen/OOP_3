using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public abstract class Pokemon
    {
        public string name;
        public string strength;
        public string weakness;

        protected Pokemon(string name, string strength, string weakness)
        {
            this.name = name;
            this.strength = strength;
            this.weakness = weakness;
        }

        public abstract void BattleCry();
    }

    public class Squirtle : Pokemon
    {
        public Squirtle(string name) : base(name, "Water", "Leaf")
        {
        }

        public override void BattleCry()
        {
            Console.WriteLine(name + "!!!");
        }
    }

    public class Bulbasaur : Pokemon
    {
        public Bulbasaur(string name) : base(name, "Grass", "Fire")
        {
        }

        public override void BattleCry()
        {
            Console.WriteLine(name + "!!!");
        }
    }

    public class Charmander : Pokemon
    {
        public Charmander(string name) : base(name, "Fire", "Water")
        {
        }

        public override void BattleCry()
        {
            Console.WriteLine(name + "!!!");
        }
    }


    class Pokeball
    {
        public bool IsOpen;
        public Charmander EnclosedPokemon;

        public void Throw()
        {
            if (!IsOpen && EnclosedPokemon != null)
            {
                Console.WriteLine("Pokeball is gegooid!");
                IsOpen = true;
                ReleasePokemon(EnclosedPokemon);
            }
            else
            {
                Console.WriteLine("Pokeball is leeg of al open.");
            }
        }

        private void ReleasePokemon(Charmander charmander)
        {
            Console.WriteLine(charmander.name + ", ik kies jou!");
            charmander.BattleCry();
        }

        public void Return()
        {
            if (IsOpen && EnclosedPokemon != null)
            {
                Console.WriteLine(EnclosedPokemon.name + " kom terug!");
                IsOpen = false;
            }
            else
            {
                Console.WriteLine("Pokeball is al dicht of leeg.");
            }
        }

        public void EnclosePokemon(Charmander charmander)
        {
            if (!IsOpen && EnclosedPokemon == null)
            {
                EnclosedPokemon = charmander;
            }
            else
            {
                Console.WriteLine("Kan pokemon niet vangen. Pokeball is al open of er zit al een pokemon in.");
            }
        }
    }

    class Trainer
    {
        public string Name;
        public List<Pokeball> Belt;

        public Trainer(string name)
        {
            Name = name;
            Belt = new List<Pokeball>();
            InitializeBeltWithCharmanders();
        }

        private void InitializeBeltWithCharmanders()
        {
            for (int i = 0; i <= 6; i++)
            {
                Charmander charmander = new Charmander("Charmander" + (i + 1), "Strength", "Weakness");
                Pokeball pokeball = new Pokeball();
                pokeball.EnclosePokemon(charmander);
                Belt.Add(pokeball);
            }
        }

        public void ThrowPokeball(int index)
        {
            if (index < 0 || index >= Belt.Count)
            {
                Console.WriteLine("Ongeldige Pokeball-index.");
                return;
            }

            Pokeball pokeball = Belt[index];
            if (!pokeball.IsOpen && pokeball.EnclosedPokemon != null)
            {
                Console.WriteLine("Trainer " + Name + " gooit een pokeball!");
                pokeball.Throw();
                return;
            }

            Console.WriteLine("De geselecteerde Pokeball kan niet worden gegooid.");
        }
        public void ReturnPokemon()
        {
            foreach (Pokeball pokeball in Belt)
            {
                if (pokeball.IsOpen && pokeball.EnclosedPokemon != null)
                {
                    pokeball.Return();
                    Console.WriteLine(pokeball.EnclosedPokemon.name + " gaat terug naar trainer " + Name + " zijn pokeball.");
                    return;
                }
            }

            Console.WriteLine("Er is geen open pokeball om terug te geven.");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            bool replayGame = true;

            while (replayGame)
            {
                Console.WriteLine("Naam trainer 1:");
                string nameTrainer1 = Console.ReadLine();
                Trainer trainer1 = new Trainer(nameTrainer1);

                Console.WriteLine("Naam trainer 2:");
                string nameTrainer2 = Console.ReadLine();
                Trainer trainer2 = new Trainer(nameTrainer2);

                Console.WriteLine("Druk op Enter om het spel te starten...");
                Console.ReadLine();

                bool gameRunning = true;
                int i = 0;
                while (gameRunning)
                {
                    Console.WriteLine("===== Trainer 1 =====");
                    trainer1.ThrowPokeball(i);
                    Console.WriteLine();

                    Console.WriteLine("===== Trainer 2 =====");
                    trainer2.ThrowPokeball(i);
                    Console.WriteLine();

                    Console.WriteLine("Wil je doorgaan met het spel? (ja/nee)");
                    string continueResponse = Console.ReadLine();
                    if (continueResponse.ToLower() == "ja")
                    {
                        Console.WriteLine("===== Trainer 1 =====");
                        trainer1.ReturnPokemon();
                        Console.WriteLine();

                        Console.WriteLine("===== Trainer 2 =====");
                        trainer2.ReturnPokemon();
                        Console.WriteLine();
                    }
                    else
                    {
                        gameRunning = false;
                    }

                    Console.WriteLine("Wil je doorgaan met het spel? (ja/nee)");
                    continueResponse = Console.ReadLine();
                    if (continueResponse.ToLower() == "nee")
                    {
                        gameRunning = false;
                    }
                    i++;
                }

                Console.WriteLine("Wil je het spel opnieuw spelen? (ja/nee)");
                string replayResponse = Console.ReadLine();
                if (replayResponse.ToLower() != "ja")
                {
                    replayGame = false;
                }
            }

            Console.WriteLine("Het spel is beëindigd.");
            Console.ReadLine();
        }
    }
}
