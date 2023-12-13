//Video Walkthrough:
//https://www.loom.com/share/2f6cbc12b38849fdaf4da4e6afbf2487

namespace PizzaWebApp.Models
{
    public class Repository
    {
        private static List<Pizza> pizzas = new List<Pizza>();

        public static Order Order { get; set; }

        public static IEnumerable<Pizza> Pizzas
        {
            get { return pizzas; }
        }

        public static void AddPizza(Pizza pizza)
        {
            pizzas.Add(pizza);
        }

        public static void RemovePizza(int id)
        {
            pizzas.RemoveAt(id);
        }

        

    }
}
