// See https://aka.ms/new-console-template for more information
// paginator on console
using paginator;
using Spectre.Console;

var paginator = new Paginator();
int currentPage = 1;
options();
paginator.DisplayProductsPerPage(currentPage);
AnsiConsole.MarkupLine($"[bold yellow]Page {currentPage} of {paginator.totalPages}[/]");
do
{
    ConsoleKeyInfo keyInfo = Console.ReadKey();
    // add delay to avoid multiple key press
    Thread.Sleep(100);
    Console.Clear();
    switch (keyInfo.Key)
    {
        case ConsoleKey.RightArrow:
            currentPage++;
            options();
            paginator.DisplayProductsPerPage(currentPage);
            // Show the current page
            AnsiConsole.MarkupLine($"[bold yellow]Page {currentPage} of {paginator.totalPages}[/]");
            break;
        case ConsoleKey.LeftArrow:
            currentPage--;
            options();
            paginator.DisplayProductsPerPage(currentPage);
            // Show the current page
            AnsiConsole.MarkupLine($"[bold yellow]Page {currentPage} of {paginator.totalPages}[/]");
            break;
        case ConsoleKey.A:
            Paginator.DisplayAllProducts();
            Console.Clear();
            options();
            paginator.DisplayProductsPerPage(currentPage);
            AnsiConsole.MarkupLine($"[bold yellow]Page {currentPage} of {paginator.totalPages}[/]");
            break;
        case ConsoleKey.Escape:
            return;
    }
    
} while (true);

static void options()
{
    // Show Key Options
    AnsiConsole.MarkupLine("[bold yellow]Press ESC to exit[/]");
    AnsiConsole.MarkupLine("[bold yellow]Press Right Arrow to go to the next page[/]");
    AnsiConsole.MarkupLine("[bold yellow]Press Left Arrow to go to the previous page[/]");
    AnsiConsole.MarkupLine("[bold yellow]Press A to show all products[/]");
}