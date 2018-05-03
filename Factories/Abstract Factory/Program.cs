using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Abstract_Factory
{
    // Interface de boisson chaude
    public interface IHotDrink
    {
        void Consume();
    }

    //Classes internes implémentant l'interface IHotDrink
    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("Nice Tea but milk missing");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("Best Coffee EUW");
        }
    }

    /* Abstract factory
     * Création de factory spécifique implémentant l'interface
     */

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Put in a tea bag, boil water, pour {amount} ml and enjoy ");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Grind beans, boil water, pour {amount} ml, add sugar if needed and enjoy");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        /*
         -- BREAKS Open Closed¨Principle -- (L'enum sera être modifié au fil du temps)
         
        public enum AvailableDrink
        {
            Coffee, Tea
        }

        /* Le dictionnaire va contenir chaque enum d'AvailableDrink et le factory associé. 
         * L'utilisation d'interface rend le tout transparent peu importe la boission,
         * tant que son factory implémente IHotDrinkFactory
         

        private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
            new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory) Activator.CreateInstance(
                    Type.GetType("Abstract_Factory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
                );
                factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
        */
        private List<Tuple<string,IHotDrinkFactory>> factories =
            new List<Tuple<string, IHotDrinkFactory>>();


        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes()) // Retourne tous élements de l'Assembly de la classe HotDrinkMachine
            {   
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) &&
                    !t.IsInterface) // test si l'instance t implémente bien l'interface IHotDrinkFactory et n'est pas l'interface elle même
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty), // Remplace Factory par un string vide dans l'instance t
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            WriteLine("Available drinks:");
            for (var index = 0; index < factories.Count; index++ )
            {
                var tuple = factories[index];
                WriteLine($"{index}: {tuple.Item1}");
            }

            while (true)
            {
                string s;
                if ((s = ReadLine()) != null
                    && int.TryParse(s, out int i)
                    && i >= 0
                    && i < factories.Count)
                {
                    WriteLine("Specify amount: ");
                    s = ReadLine();
                    if (s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }

                    WriteLine("Incorrect input, try again");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100); old way, breaks OCP
            drink.Consume();
            Read();
        }
    }
}
