using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IISMSBackend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBarcodeToByteArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
{
    // Add a temporary column to store barcode as bytea
    migrationBuilder.AddColumn<byte[]>(
        name: "productBarcode_temp",
        table: "Products",
        type: "bytea",
        nullable: true);

    // Convert existing data from text to bytea (Base64 decoding)
    migrationBuilder.Sql("UPDATE \"Products\" SET \"productBarcode_temp\" = decode(\"productBarcode\", 'base64');");

    // Remove the old text column
    migrationBuilder.DropColumn(name: "productBarcode", table: "Products");

    // Rename the temporary column to the original column name
    migrationBuilder.RenameColumn(
        name: "productBarcode_temp",
        table: "Products",
        newName: "productBarcode");
}

protected override void Down(MigrationBuilder migrationBuilder)
{
    // Revert the changes if needed
    migrationBuilder.AddColumn<string>(
        name: "productBarcode_temp",
        table: "Products",
        type: "text",
        nullable: true);

    migrationBuilder.Sql("UPDATE \"Products\" SET \"productBarcode_temp\" = encode(\"productBarcode\", 'base64');");

    migrationBuilder.DropColumn(name: "productBarcode", table: "Products");

    migrationBuilder.RenameColumn(
        name: "productBarcode_temp",
        table: "Products",
        newName: "productBarcode");
}

    }
}
