using Common.Entities;
using Spectre.Console;
using Microsoft.EntityFrameworkCore;
using Spectre.Console.Rendering;

namespace paginator;
public class Paginator
{
    private static readonly NorthwindContext db = new();
    public readonly int totalPages;
    readonly int rowsPerPage;
    public Paginator()
    {
        rowsPerPage = Getrows();
        totalPages = GetTotalPages();
    }
    public static void DisplayAllProducts()
    {
        // Get all products and your respective categories names   
        IQueryable<Product> products = db.Products
            .OrderBy(p => p.ProductId)
            .Include(p => p.Category);
        DisplayTable(products);
        AnsiConsole.MarkupLine("[bold yellow]Press any key to continue[/]");
        Console.ReadKey();
    }
    public void DisplayProductsPerPage(int currentPage)
    {
        // Get all products and your respective categories names   
        IQueryable<Product> products = db.Products
            .OrderBy(p => p.ProductId)
            .Skip((currentPage - 1) * rowsPerPage)
            .Take(rowsPerPage)
            .Include(p => p.Category);
        DisplayTable(products);
    }
    static void DisplayTable(IQueryable<Product> products)
    {
        // Display all products into a table
        TableColumn[] columns = new TableColumn[]
        {
        new TableColumn("Product Id").Centered(),
        new TableColumn("Product Name").Centered(),
        new TableColumn("Units In Stock").Centered(),
        new TableColumn("Discontinued").Centered(),
        new TableColumn("Category").Centered()
        };
        var table = new Table();
        table.AddColumns(columns);
        foreach (var p in products)
        {
            table.AddRow(new List<IRenderable>
        {
            new Text(p.ProductId.ToString()).Centered(),
            new Text(p.ProductName).Centered(),
            new Text(p.UnitsInStock.ToString()!).Centered(),
            new Text(p.Discontinued.ToString()).Centered(),
            new Text(p.Category!.CategoryName).Centered()
        });
        }
        AnsiConsole.Write(table);
    }

    private int GetTotalPages()
    {
        return (int)((double)GetCount() / rowsPerPage) + 1;
    }
    private static int GetCount()
    {
        return db.Products.Count();
    }
    static int Getrows()
    {
        int pageSize;
        do
        {
            pageSize = AnsiConsole.Ask<int>("How many items per page?");
        } while (pageSize > 50 || pageSize < 1);
        return pageSize;
    }
}