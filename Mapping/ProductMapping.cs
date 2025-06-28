using IISMSBackend.Dtos;
using IISMSBackend.Entities;

namespace IISMSBackend.Mapping;


public static class ProductMapping {
    
    public static Product ToEntity(this CreateProductDto product, byte[] barcode, DateTime timestamp) {
        return new Product() {
            productImage = product.productImage,
            productName = product.productName,
            productBarcode = barcode,
            category = product.category,
            size = product.size,
            unit = product.unit,
            price = product.price,
            quantity = product.quantity,
            manufactureDate = product.manufactureDate,
            expirationDate = product.expirationDate,
            productDescription = product.productDescription,
            firstCreationTimestamp = timestamp
        };
    }

    public static Product ToEntity(this UpdateProductDto product, int id, byte[] barcode, DateTime timestamp) {
        return new Product() {
            productId = id,
            productImage = product.productImage,
            productName = product.productName,
            productBarcode = barcode,
            category = product.category,
            size = product.size,
            unit = product.unit,
            price = product.price,
            quantity = product.quantity,
            manufactureDate = product.manufactureDate,
            expirationDate = product.expirationDate,
            productDescription = product.productDescription,
            firstCreationTimestamp = timestamp
        };
    }

     public static Sales ToEntity(this CreateSalesRecordDto sales, DateTime timestamp, int[] productId) {

        var salesEntity = new Sales {
            totalCartQuantity = sales.totalCartQuantity,
            totalCartPrice = sales.totalCartPrice,
            salesTimestamp = timestamp,
            SalesProducts = new List<SalesProduct>()
        };
        for(int i = 0; i < sales.productName.Length; i++) {
            var salesProduct = new SalesProduct {
                productId = productId[i],
                salesQuantity = sales.quantity[i],
                unitPrice = sales.unitPrice[i],
                totalUnitPrice = sales.totalUnitPrice[i],
                Sale = salesEntity,
            };
            salesEntity.SalesProducts.Add(salesProduct);
        }
       return salesEntity;
    }

    public static Order ToEntity(this CreateOrderDto order, int[] productId)
    {

        var orderEntity = new Order
        {
            customerName = order.customerName,
            address = order.address,
            deliveryDate = DateTime.SpecifyKind(order.deliveryDate, DateTimeKind.Utc),
            status = order.status,
            OrderProducts = new List<OrderProduct>()
        };
        for (int i = 0; i < order.productName.Length; i++)
        {
            var salesProduct = new OrderProduct
            {
                productId = productId[i],
                orderQuantity = order.quantity[i],
                Order = orderEntity
            };
            orderEntity.OrderProducts.Add(salesProduct);
        }
        return orderEntity;
    }

    public static void ApplyUpdate(this UpdateOrderDto updatedOrder, Order existingOrder, int[] productIds)
    {
        existingOrder.customerName = updatedOrder.customerName;
        existingOrder.address = updatedOrder.address;
        existingOrder.deliveryDate = DateTime.SpecifyKind(updatedOrder.deliveryDate, DateTimeKind.Utc);
        existingOrder.status = updatedOrder.status;

        existingOrder.OrderProducts ??= new List<OrderProduct>();
        existingOrder.OrderProducts.Clear();

        for (int i = 0; i < updatedOrder.productName.Length; i++)
        {
            var orderProduct = new OrderProduct
            {
                productId = productIds[i],
                orderQuantity = updatedOrder.quantity[i],
                Order = existingOrder
            };
            existingOrder.OrderProducts.Add(orderProduct);
        }
    }

    public static Inventory ToEntity(this CreateInventoryDto inventory, DateTime timestamp) {
        return new Inventory() {
            productId = inventory.productId,
            operation = inventory.operation,
            quantity = inventory.quantity,
            inventoryTimestamp = timestamp
        };
    }



     public static ProductDetailsDto ToProductDetailsDto(this Product product) {
        return new(
            product.productId,
            product.productImage,
            product.productName,
            product.productBarcode,
            product.category,
            product.size,
            product.unit,
            product.price,
            product.quantity,
            product.manufactureDate,
            product.expirationDate,
            product.productDescription,
            product.firstCreationTimestamp
        );
    }


     public static SalesDetailsDto ToSalesDetailsDto(this Sales sale) {
        return new(
            sale.salesId,
            sale.totalCartPrice,
            sale.totalCartQuantity,
            sale.salesTimestamp,
            sale.SalesProducts
        );
    }

    public static InventoryDetailsDto ToInventoryDetailsDto(this Inventory inventory) {
        return new(
            inventory.inventoryId,
            inventory.productId,
            inventory.operation,
            inventory.quantity,
            inventory.inventoryTimestamp
        );
    }

     public static OrderDetailsDto ToOrderDetailsDto(this Order order) {
        return new(
            order.orderId,
            order.customerName,
            order.customerSignature,
            order.address,
            order.deliveryDate,
            order.status,
            order.OrderProducts
        );
    }
}
