/*
 * Sources:
 *      https://stackoverflow.com/questions/442704/how-do-you-handle-multiple-submit-buttons-in-asp-net-mvc-framework
 *      https://www.c-sharpcorner.com/article/asp-net-mvc-passing-data-from-controller-to-view/
 */

namespace PizzaWebApp.Models
{
    public class Pizza
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public bool ExtraCheese { get; set; }
        public decimal Price { get; set; }

        public Pizza()
        {
            Name = "Cheese";
            Size = "Medium";
            Description = "Tomato Sauce, Mozzarella Cheese";
            ExtraCheese = false;
            Price = 10.00M;
        }

        public Pizza(string name, string size, string description, bool extraCheese, decimal price)
        {
            Name = name;
            Size = size;
            Description = description;
            ExtraCheese = extraCheese;
            Price = price;
        }
    }
}
