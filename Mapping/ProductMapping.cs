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
            expirationDate = product.expirationDate,
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
            expirationDate = product.expirationDate,
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
            product.expirationDate,
            product.firstCreationTimestamp
        );
    }

    
}
