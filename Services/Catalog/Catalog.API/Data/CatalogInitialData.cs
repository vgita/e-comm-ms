using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using IDocumentSession session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(token: cancellation))
            return;

        //Marten UPSERT operation
        session.Store<Product>(GetSeedingProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<Product> GetSeedingProducts()
    {
        return
        [
            new Product
        {
            Id = new("5b6e5f4b-0f1e-4b7b-8b3d-1b7f1e6e3f1e"),
            Name = "Keyboard",
            Categories = ["Electronics", "Hardware"],
            Description = "A mechanical keyboard",
            ImageFile = "keyboard.jpg",
            Price = 50
        },
            new Product
        {
            Id = new("5b6e5f4b-0f1e-4b7b-8b3d-1b7f1e6e3f1f"),
            Name = "Mouse",
            Categories = ["Electronics", "Hardware"],
            Description = "A gaming mouse",
            ImageFile = "mouse.jpg",
            Price = 20
        },
            new Product
        {
            Id = new("5b6e5f4b-0f1e-4b7b-8b3d-1b7f1e6e3f1c"),
            Name = "Monitor",
            Categories = ["Electronics", "Hardware"],
            Description = "A 4k monitor",
            ImageFile = "monitor.jpg",
            Price = 300
        },
            new Product
        {
            Id = new("5b6e5f4b-0f1e-4b7b-8b3d-1b7f1e6e3f1d"),
            Name = "Headphones",
            Categories = ["Electronics", "Hardware"],
            Description = "Wireless headphones",
            ImageFile = "headphones.jpg",
            Price = 100
        },
            new Product
            {
                Id = new("5b6e5f4b-0f1e-4b7b-8b3d-1b7f1e6e3f1a"),
                Name = "Laptop",
                Categories = ["Electronics", "Hardware"],
                Description = "A gaming laptop",
                ImageFile = "laptop.jpg",
                Price = 800
            }
        ];
    }


}
