using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DbInitializer
{
    // WebApplication app: Program.cs içindeki uygulamanın kendisini (container/servis sağlayıcı dahil) parametre alıyoruz.
    // Bu sınıfı da Program.cs içinden çağıracağız. Program.cs içindeki app nesnesi ile bu sınıfın InitDb metodunu çağıracağız.
    public static void InitDb(WebApplication app)
    {
        // 1. Bir bağımlılık çözme alanı (Scope) oluşturuyoruz ve 'using var' ile işi bitince hafızadan silinmesini sağlıyoruz.
        using var scope = app.Services.CreateScope();

        // 2. Oluşturduğumuz scope içinden veritabanı nesnemiz olan StoreContext'i (DbContext) talep ediyoruz.
        // Eğer servis kayıtlı değilse veya bulunamazsa '??' operatörü ile bir hata fırlatıyoruz.
        var context = scope.ServiceProvider.GetRequiredService<StoreContext>()
            ?? throw new InvalidOperationException("Failed to retrieve store context");

        // 3. Bulduğumuz context nesnesini aşağıdaki veriyi dolduracak (seed edecek) metoda gönderiyoruz.
        SeedData(context);
    }

    // private void: Bu metot sadece bu sınıf içinden çağrılabilir. StoreContext nesnesini parametre olarak bekler.
    /*
    SeedData metodu bir kere çalışıp bitecek, veritabanına verileri basacak bir "araç" (utility) metodudur. Kendi içinde durum (state) tutmaz.

    Onu static yaparak şunu garanti etmiş olursun: "Bu metot çalışırken DbInitializer sınıfının diğer nesnelerini etkileyemez, araya kaynak sızıntısı giremez." Sadece kendisine verilen context ile işini yapar ve temizce kapanır.
    */
    private static void SeedData(StoreContext context)
    {
        // 4. Henüz veritabanına uygulanmamış bir migration (geçiş) varsa bunu otomatik olarak veritabanına uygular.
        // Veritabanı yoksa sıfırdan oluşturur.
        context.Database.Migrate();

        // 5. Eğer 'Products' tablosunda halihazırda herhangi bir kayıt varsa metottan çık (return), alt satırlara geçme.
        if (context.Products.Any()) return;

        // 6. Eğer tablo boşsa, veritabanına ilk başta eklenecek örnek ürün listesini oluşturuyoruz.
        var products = new List<Product>
        {
                new() {
                    Name = "Angular Speedster Board 2000",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 20000,
                    PictureUrl = "/images/products/sb-ang1.png",
                    Brand = "Angular",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Green Angular Board 3000",
                    Description = "Nunc viverra imperdiet enim. Fusce est. Vivamus a tellus.",
                    Price = 15000,
                    PictureUrl = "/images/products/sb-ang2.png",
                    Brand = "Angular",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Core Board Speed Rush 3",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    PictureUrl = "/images/products/sb-core1.png",
                    Brand = "NetCore",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Net Core Super Board",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 30000,
                    PictureUrl = "/images/products/sb-core2.png",
                    Brand = "NetCore",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "React Board Super Whizzy Fast",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 25000,
                    PictureUrl = "/images/products/sb-react1.png",
                    Brand = "React",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Typescript Entry Board",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 12000,
                    PictureUrl = "/images/products/sb-ts1.png",
                    Brand = "TypeScript",
                    Type = "Boards",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Core Blue Hat",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1000,
                    PictureUrl = "/images/products/hat-core1.png",
                    Brand = "NetCore",
                    Type = "Hats",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Green React Woolen Hat",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 8000,
                    PictureUrl = "/images/products/hat-react1.png",
                    Brand = "React",
                    Type = "Hats",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Purple React Woolen Hat",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1500,
                    PictureUrl = "/images/products/hat-react2.png",
                    Brand = "React",
                    Type = "Hats",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Blue Code Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1800,
                    PictureUrl = "/images/products/glove-code1.png",
                    Brand = "VS Code",
                    Type = "Gloves",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Green Code Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1500,
                    PictureUrl = "/images/products/glove-code2.png",
                    Brand = "VS Code",
                    Type = "Gloves",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Purple React Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1600,
                    PictureUrl = "/images/products/glove-react1.png",
                    Brand = "React",
                    Type = "Gloves",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Green React Gloves",
                    Description =
                        "Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 1400,
                    PictureUrl = "/images/products/glove-react2.png",
                    Brand = "React",
                    Type = "Gloves",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Redis Red Boots",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 25000,
                    PictureUrl = "/images/products/boot-redis1.png",
                    Brand = "Redis",
                    Type = "Boots",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Core Red Boots",
                    Description =
                        "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Maecenas porttitor congue massa. Fusce posuere, magna sed pulvinar ultricies, purus lectus malesuada libero, sit amet commodo magna eros quis urna.",
                    Price = 18999,
                    PictureUrl = "/images/products/boot-core2.png",
                    Brand = "NetCore",
                    Type = "Boots",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Core Purple Boots",
                    Description =
                        "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
                    Price = 19999,
                    PictureUrl = "/images/products/boot-core1.png",
                    Brand = "NetCore",
                    Type = "Boots",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Angular Purple Boots",
                    Description = "Aenean nec lorem. In porttitor. Donec laoreet nonummy augue.",
                    Price = 15000,
                    PictureUrl = "/images/products/boot-ang2.png",
                    Brand = "Angular",
                    Type = "Boots",
                    QuantityInStock = 100
                },
                new Product
                {
                    Name = "Angular Blue Boots",
                    Description =
                        "Suspendisse dui purus, scelerisque at, vulputate vitae, pretium mattis, nunc. Mauris eget neque at sem venenatis eleifend. Ut nonummy.",
                    Price = 18000,
                    PictureUrl = "/images/products/boot-ang1.png",
                    Brand = "Angular",
                    Type = "Boots",
                    QuantityInStock = 100
                },
        };

        // 7. Oluşturulan bu listeyi topluca Products tablosuna ekleme sırasına alıyoruz.
        context.Products.AddRange(products);

        // 8. Şu ana kadar yapılan işlemleri (Migrate ve AddRange) veritabanına fiziksel olarak kaydetmek için SQL komutuna dönüştürüp çalıştırır.
        context.SaveChanges();
    }
}