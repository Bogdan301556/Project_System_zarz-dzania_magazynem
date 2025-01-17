using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var warehouse = new Warehouse();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Główne Menu");
            Console.WriteLine("1. Zarządzanie Produktami");
            Console.WriteLine("2. Podgląd Stanu Magazynu");
            Console.WriteLine("3. Podgląd Operacji");
            Console.WriteLine("4. Wyjście");
            Console.Write("Wybierz opcję: ");
            string mainMenuChoice = Console.ReadLine();

            switch (mainMenuChoice)
            {
                case "1":
                    ZarzadzajProduktami(warehouse);
                    break;
                case "2":
                    PodgladStanuMagazynu(warehouse);
                    break;
                case "3":
                    PodgladOperacji(warehouse);
                    break;
                case "4":
                    Console.WriteLine("Zamykanie programu...");
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
        }
    }

    static void ZarzadzajProduktami(Warehouse warehouse)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Zarządzanie Produktami");
            Console.WriteLine("1. Dodaj Produkt");
            Console.WriteLine("2. Zaktualizuj Ilość Produktu");
            Console.WriteLine("3. Powrót do Głównego Menu");
            Console.Write("Wybierz opcję: ");
            string productMenuChoice = Console.ReadLine();

            switch (productMenuChoice)
            {
                case "1":
                    DodajProdukt(warehouse);
                    break;
                case "2":
                    ZaktualizujIloscProduktu(warehouse);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
        }
    }

    static void DodajProdukt(Warehouse warehouse)
    {
        Console.Clear();
        Console.WriteLine("Dodaj Produkt");

        Console.Write("Wprowadź nazwę produktu: ");
        string name = Console.ReadLine();

        Console.Write("Wprowadź ilość: ");
        int quantity = int.Parse(Console.ReadLine());

        warehouse.AddProduct(0, name, quantity);
        Console.WriteLine("Produkt został dodany pomyślnie.");
        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }

    static void ZaktualizujIloscProduktu(Warehouse warehouse)
    {
        Console.Clear();
        Console.WriteLine("Zaktualizuj Ilość Produktu");

        Console.Write("Wprowadź ID Produktu: ");
        int productId = int.Parse(Console.ReadLine());

        Console.Write("Wprowadź nową ilość: ");
        int newQuantity = int.Parse(Console.ReadLine());

        warehouse.UpdateProductQuantity(productId, newQuantity);
        Console.WriteLine("Ilość produktu została pomyślnie zaktualizowana.");
        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }

    static void PodgladStanuMagazynu(Warehouse warehouse)
    {
        Console.Clear();
        Console.WriteLine("Stan Magazynu");

        var products = warehouse.GetAllProducts();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Nazwa: {product.Name}, Ilość: {product.Quantity}");
        }

        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }

    static void PodgladOperacji(Warehouse warehouse)
    {
        Console.Clear();
        Console.WriteLine("Operacje na Magazynie");

        var operations = warehouse.GetAllOperations();
        foreach (var operation in operations)
        {
            Console.WriteLine($"{operation.Date}: Produkt ID {operation.ProductId}, Operacja: {operation.Type}, Ilość: {operation.Quantity}");
        }

        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }
}
