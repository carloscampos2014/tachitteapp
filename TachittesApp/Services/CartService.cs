using System.Collections.Generic;
using System.Linq;
using TachittesApp.Models;
using System;

namespace TachittesApp.Services;

public class CartService
{
    public List<CartItem> Items { get; } = new List<CartItem>();

    public event Action? OnChange;

    public void AddItem(CartItem newItem)
    {
        var existingItem = Items.FirstOrDefault(i =>
            i.Item.Id == newItem.Item.Id &&
            i.Customizations.SequenceEqual(newItem.Customizations));

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            Items.Add(newItem);
        }
        NotifyStateChanged();
    }

    public void DecrementQuantity(CartItem item)
    {
        var existingItem = Items.FirstOrDefault(i => i.Equals(item));
        if (existingItem == null) return;

        existingItem.Quantity--;
        if (existingItem.Quantity <= 0)
        {
            Items.Remove(existingItem);
        }
        NotifyStateChanged();
    }

    public void RemoveItem(CartItem item)
    {
        Items.Remove(item);
        NotifyStateChanged();
    }

    public void ClearCart()
    {
        Items.Clear();
        NotifyStateChanged();
    }

    public decimal TotalPrice => Items.Sum(item => item.TotalPrice);

    private void NotifyStateChanged() => OnChange?.Invoke();
}