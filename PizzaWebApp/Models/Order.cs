using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


//Custom function for conditional required fields
public class RequiredIfAttribute : ValidationAttribute
{
    RequiredAttribute _innerAttribute = new RequiredAttribute();
    public string _dependentProperty { get; set; }
    public object _targetValue { get; set; }

    public RequiredIfAttribute(string dependentProperty, object targetValue)
    {
        this._dependentProperty = dependentProperty;
        this._targetValue = targetValue;
    }
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var field = validationContext.ObjectType.GetProperty(_dependentProperty);
        if (field != null)
        {
            var dependentValue = field.GetValue(validationContext.ObjectInstance, null);
            if ((dependentValue == null && _targetValue == null) || (dependentValue.Equals(_targetValue)))
            {
                if (!_innerAttribute.IsValid(value))
                {
                    string name = validationContext.DisplayName;
                    string specificErrorMessage = ErrorMessage;
                    if (specificErrorMessage.Length < 1)
                        specificErrorMessage = $"{name} is required.";

                    return new ValidationResult(specificErrorMessage, new[] { validationContext.MemberName });
                }
            }
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(FormatErrorMessage(_dependentProperty));
        }
    }
}

namespace PizzaWebApp.Models
{
    public class Order
    {
       
        
        
        public  decimal Total { get; set; }
        public  decimal Tax { get; set; }
        public  decimal GrandTotal {  get; set; }

        [Required (ErrorMessage ="Please enter a name for the order.") ]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please select the Delivery or Pick-up option.")]
        public string IsDelivery { get; set; }

        [RequiredIf("IsDelivery", "delivery", ErrorMessage = "Please enter Street Address for Delivery")]
        public string? Street {  get; set; }

        [RequiredIf("IsDelivery", "delivery", ErrorMessage = "Please enter City for Delivery")]
        public string? City { get; set; }

        [RequiredIf("IsDelivery", "delivery", ErrorMessage = "Please enter State for Delivery")]
        public string? State { get; set; }

        [RequiredIf("IsDelivery", "delivery", ErrorMessage = "Please enter Zip Code for Delivery")]
        public string? Zip { get; set; }

       public Order(string name, string isDelivery, string street, string city, string state, string zip)
        {
            Name = name;
            IsDelivery = isDelivery;
            Street = street;
            City = city;
            State = state;
            Zip = zip;
            CalculateCost();
        }

        public Order(string name, string isDelivery)
        {
            Name = name;
            IsDelivery = isDelivery;
            CalculateCost();
        }

        public Order()
        {
            Name = string.Empty;
            IsDelivery = string.Empty;
            CalculateCost();
        }

        public void CalculateCost()
        {
            Total = 0;
            Tax = 0;
            GrandTotal = 0;
            foreach(Pizza p in Repository.Pizzas)
            {
                Total += p.Price;
            }

            Tax = Total * 0.0725M;

            GrandTotal = Tax + Total;
        }
    }
}
