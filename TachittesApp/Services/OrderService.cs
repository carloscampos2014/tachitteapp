using TachittesApp.Models;
using System.Text;
using System.Linq;

namespace TachittesApp.Services;

public class OrderService
{
    public Order CurrentOrder { get; set; } = new Order();
    public MenuData? MenuData { get; set; }

    public decimal GetDeliveryFee()
    {
        return MenuData?.DeliveryFee ?? 0;
    }

    public decimal GetTotal(CartService cartService)
    {
        var subtotal = cartService.TotalPrice;
        if (CurrentOrder.DeliveryOption == "Entrega")
        {
            return subtotal + GetDeliveryFee();
        }
        return subtotal;
    }

    public string GenerateWhatsAppMessage(CartService cartService)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Olá! Gostaria de fazer um pedido:");
        sb.AppendLine($"Nome: {CurrentOrder.CustomerName}");
        sb.AppendLine($"Telefone: {CurrentOrder.PhoneNumber}");
        sb.AppendLine();
        sb.AppendLine("--- Detalhes do Pedido ---");

        foreach (var item in cartService.Items)
        {
            sb.AppendLine($"- {item.Quantity}x {item.Item.Name} - {item.Item.Price.ToString("C")}");
            if (item.Customizations.Any())
            {
                var customizations = string.Join(", ", item.Customizations.Select(c => c.Name));
                sb.AppendLine($"  > Customizações: {customizations}");
            }
        }

        sb.AppendLine();
        sb.AppendLine($"Subtotal: {cartService.TotalPrice.ToString("C")}");
        sb.AppendLine($"Entrega: {(CurrentOrder.DeliveryOption == "Entrega" ? GetDeliveryFee().ToString("C") : "Retirada")}");
        sb.AppendLine($"Total a Pagar: {GetTotal(cartService).ToString("C")}");
        sb.AppendLine();

        sb.AppendLine("--- Forma de Pagamento ---");
        sb.AppendLine($"Método: {CurrentOrder.PaymentMethod}");

        if (CurrentOrder.PaymentMethod == "Cartões")
        {
            sb.AppendLine($"Tipo: {CurrentOrder.CardType}");
        }
        else if (CurrentOrder.PaymentMethod == "Dinheiro")
        {
            if (CurrentOrder.ChangeValue > GetTotal(cartService))
            {
                var troco = CurrentOrder.ChangeValue - GetTotal(cartService);
                sb.AppendLine($"Valor informado: {CurrentOrder.ChangeValue.ToString("C")}");
                sb.AppendLine($"Valor do troco: {troco.ToString("C")}");
            }
        }
        else if (CurrentOrder.PaymentMethod == "PIX")
        {
            //sb.AppendLine($"Chave: {MenuData?.PixKey}");
            //sb.AppendLine($"Nome: {MenuData?.PixName}");
            //sb.AppendLine($"Cidade: {MenuData?.PixCity}");
        }

        return System.Web.HttpUtility.UrlEncode(sb.ToString());
    }
}