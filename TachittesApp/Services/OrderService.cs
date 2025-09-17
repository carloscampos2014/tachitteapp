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
        sb.AppendLine("*NOVO PEDIDO TACHITTE*");
        sb.AppendLine();

        sb.AppendLine("*CLIENTE*");
        sb.AppendLine($"Nome: {CurrentOrder.CustomerName}");
        sb.AppendLine($"Telefone: {CurrentOrder.PhoneNumber}");

        if (CurrentOrder.DeliveryOption == "Entrega")
        {
            sb.AppendLine($"Endereço: {CurrentOrder.DeliveryAddress.Street}");
            if (!string.IsNullOrWhiteSpace(CurrentOrder.DeliveryAddress.ReferencePoint))
            {
                sb.AppendLine($"Ref.: {CurrentOrder.DeliveryAddress.ReferencePoint}");
            }
        }
        else
        {
            sb.AppendLine("Opção: *Retirada no local*");
        }


        sb.AppendLine();
        sb.AppendLine("*PEDIDO*");
        foreach (var item in cartService.Items)
        {
            sb.AppendLine($"*{item.Quantity}x {item.Item.Name}* - {item.TotalPrice.ToString("C")}");
            if (item.Customizations.Any())
            {
                var customizations = string.Join(", ", item.Customizations.Select(c => c.Name));
                sb.AppendLine($"  - _{customizations}_");
            }
        }

        sb.AppendLine();
        sb.AppendLine("*PAGAMENTO*");
        sb.AppendLine($"Forma: {CurrentOrder.PaymentMethod}");

        if (CurrentOrder.PaymentMethod == "Cartões")
        {
            sb.AppendLine($"Tipo: {CurrentOrder.CardType}");
        }
        else if (CurrentOrder.PaymentMethod == "Dinheiro")
        {
            if (CurrentOrder.ChangeValue > 0 && CurrentOrder.ChangeValue > GetTotal(cartService))
            {
                sb.AppendLine($"Troco para: {CurrentOrder.ChangeValue.ToString("C")}");
            }
        }

        sb.AppendLine();
        sb.AppendLine("-----------------");
        sb.AppendLine($"*TOTAL:* *{GetTotal(cartService).ToString("C")}*");

        return System.Web.HttpUtility.UrlEncode(sb.ToString());
    }
}