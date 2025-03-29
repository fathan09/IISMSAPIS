using Microsoft.EntityFrameworkCore;
using IISMSBackend.Data;
using IISMSBackend.Mapping;
using IISMSBackend.Dtos;
using IISMSBackend.Entities;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp;
using SkiaSharp;


namespace IISMSBackend.Endpoints;

public static class ProductEndpoint {
    const string GetProductEndpointName = "GetProduct";

    public static RouteGroupBuilder MapProductEndpoint(this WebApplication app) {
        var group = app.MapGroup("product").WithParameterValidation();

        group.MapGet("/all", async(IISMSContext dbContext) => 
            await dbContext.Products
                .Select(product => product.ToProductDetailsDto())
                .AsNoTracking()
                .ToListAsync()
        );

        group.MapGet("/{id}", async(int id, IISMSContext dbContext) => {
            Product? product = await dbContext.Products.FindAsync(id);
            return product is null ? Results.NotFound() : Results.Ok(product.ToProductDetailsDto());
        }).WithName(GetProductEndpointName);

        group.MapPost("/create", async(CreateProductDto newProduct, IISMSContext dbContext) => {
            string barcodeInfo = $"{newProduct.productName}";
            
            DateTime timestamp = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);


            var barcodeWriter = new ZXing.SkiaSharp.BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = 100,
                    Width = 300,
                    Margin = 1
                }
            };

            SKBitmap skBitmap = barcodeWriter.Write(barcodeInfo);

        
            byte[] newBarcode;
            using (var image = skBitmap.Encode(SKEncodedImageFormat.Png, 100))
            using (var ms = new MemoryStream())
            {
                image.AsStream().CopyTo(ms);
                newBarcode = ms.ToArray();
            }

            Product product = newProduct.ToEntity(newBarcode, timestamp);
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetProductEndpointName, new {id = product.productId}, product.ToProductDetailsDto());
        }).WithParameterValidation();


        group.MapPut("/update/{id}", async (int id, UpdateProductDto updatedProduct, IISMSContext dbContext) => {
           
            var exisitingProduct = await dbContext.Products.FindAsync(id);
            if(exisitingProduct is null) {
                return Results.NotFound();
            }
            byte[]? existingBarcode = exisitingProduct.productBarcode;
            DateTime existingTimestamp = exisitingProduct.firstCreationTimestamp;
            dbContext.Entry(exisitingProduct).CurrentValues.SetValues(updatedProduct.ToEntity(id, existingBarcode, existingTimestamp));
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });

        group.MapDelete("/delete/{id}", async(int id, IISMSContext dbContext) => {
            await dbContext.Products.Where(product => product.productId == id).ExecuteDeleteAsync();
            return Results.NoContent();
        });

         group.MapGet("/search", async (HttpContext httpContext, IISMSContext dbContext) => {
    string? name = httpContext.Request.Query["name"];

    if (string.IsNullOrWhiteSpace(name)) {
        return Results.BadRequest("Search query cannot be empty.");
    }

        var products = await dbContext.Products
            .Where(p => EF.Functions.ILike(p.productName, $"%{name}%")) 
            .Select(p => p.ToProductDetailsDto())
            .AsNoTracking()
            .ToListAsync();
    
        return products.Any() ? Results.Ok(products) : Results.NotFound("No products found.");
    });

         group.MapPost("/sales", async(CreateSalesRecordDto newSales, IISMSContext dbContext) => {

            Console.WriteLine("Received request data:");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(newSales));

            DateTime timestamp = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);


            int[] productId = new int[newSales.productName.Length];
            for(int i = 0; i < newSales.productName.Length; i++) {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.productName == newSales.productName[i]);

                if(product == null) {
                    return Results.NotFound($"Product not found : {newSales.productName[i]}");
                } else {
                    productId[i] = product.productId;
                }
            }

            Sales sale =  newSales.ToEntity(timestamp, productId);

            dbContext.Sales.Add(sale);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GetProductEndpointName, new {id = sale.salesId});

        });

        return group;
    }
}
