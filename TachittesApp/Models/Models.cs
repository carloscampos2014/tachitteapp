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

    public decimal TotalPrice => Item.Price + Customizations.Sum(o => o.Price);
}
