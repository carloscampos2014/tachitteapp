using System.Collections.Generic;
using System.Linq;
using TachittesApp.Models;

namespace TachittesApp.Services;

public class CartService
{
    public List<CartItem> Items { get; } = new List<CartItem>();

    public event Action? OnChange;

    public void AddItem(CartItem item)
    {
        Items.Add(item);
        NotifyStateChanged();
    }

    public void RemoveItem(CartItem item)
    {
        Items.Remove(item);
        NotifyStateChanged();
    }

    public decimal TotalPrice => Items.Sum(item => item.TotalPrice);

    private void NotifyStateChanged() => OnChange?.Invoke();
}