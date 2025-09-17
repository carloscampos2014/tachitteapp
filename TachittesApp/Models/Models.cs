using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace TachittesApp.Models;

public class MenuData
{
    [JsonPropertyName("categories")]
    public List<Category> Categories { get; set; } = new List<Category>();

    [JsonPropertyName("items")]
    public List<Item> Items { get; set; } = new List<Item>();

    [JsonPropertyName("customizationSteps")]
    public List<CustomizationGroup> CustomizationSteps { get; set; } = new List<CustomizationGroup>();

    [JsonPropertyName("deliveryFee")]
    public decimal DeliveryFee { get; set; }

    [JsonPropertyName("whatsappNumber")]
    public string WhatsappNumber { get; set; } = string.Empty;

    [JsonPropertyName("pixKey")]
    public string PixKey { get; set; } = string.Empty;

    [JsonPropertyName("pixName")]
    public string PixName { get; set; } = string.Empty;

    [JsonPropertyName("pixCity")]
    public string PixCity { get; set; } = string.Empty;
}

public class Category
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("itemIds")]
    public List<string> ItemIds { get; set; } = new List<string>();
}

public class Item
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;
}

public class CustomizationGroup
{
    [JsonPropertyName("productType")]
    public string ProductType { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("steps")]
    public List<CustomizationStep> Steps { get; set; } = new List<CustomizationStep>();
}

public class CustomizationStep
{
    [JsonPropertyName("stepName")]
    public string StepName { get; set; } = string.Empty;

    [JsonPropertyName("selectionType")]
    public string SelectionType { get; set; } = string.Empty;

    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("minRequired")]
    public int? MinRequired { get; set; }

    [JsonPropertyName("options")]
    public List<Option> Options { get; set; } = new List<Option>();
}

public class Option
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}

public class CartItem
{
    public Item Item { get; set; } = new Item();
    public List<Option> Customizations { get; set; } = new List<Option>();
    public int Quantity { get; set; } = 1;

    public decimal TotalPrice => (Item.Price + Customizations.Sum(o => o.Price)) * Quantity;
}

public class Order
{
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string DeliveryOption { get; set; } = "Entrega"; // 'Entrega' or 'Retirada'
    public Address? DeliveryAddress { get; set; }
    public string PaymentMethod { get; set; } = "Dinheiro"; // 'Dinheiro', 'PIX', 'Cartões'
    public string CardType { get; set; } = "Crédito"; // 'Débito', 'Crédito', 'Refeição', 'Alimentação'
    public decimal ChangeValue { get; set; } = 0;
}

public class Address
{
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string ReferencePoint { get; set; } = string.Empty;
}