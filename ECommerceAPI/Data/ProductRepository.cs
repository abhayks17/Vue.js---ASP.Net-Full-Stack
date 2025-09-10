using ECommerceAPI.Models;

namespace ECommerceAPI.Data
{
    public static class ProductRepository
    {
        public static readonly List<Product> Products = new()
        {
            new() { Id = 1, Name = "Laptop", Price = 1200, Category = "Electronics", Description = "A high-performance laptop for professional and personal use.", ImageUrl="https://p2-ofp.static.pub//fes/cms/2024/07/17/109vq5fdalv01w5jsu6vh35ncnk5jn890135.png", IsInStock=true },
            new() { Id = 2, Name = "Smartphone", Price = 800, Category = "Electronics", Description = "The latest smartphone with a stunning display and powerful camera.", ImageUrl="https://iplanet.one/cdn/shop/files/iPhone_15_Pink_PDP_Image_Position-1_alt__en-IN_8eb53143-72c3-4e23-aa93-71456d710a37.jpg?v=1695428220&width=1445", IsInStock=true },
            new() { Id = 3, Name = "Headphones", Price = 150, Category = "Electronics", Description = "Noise-cancelling headphones for an immersive audio experience.", ImageUrl="https://cdns3.thecosmicbyte.com/wp-content/uploads/white-bg-2.jpg.webp", IsInStock=false },
            new() { Id = 4, Name = "Running Shoes", Price = 100, Category = "Fashion", Description = "Comfortable and durable running shoes for all terrains.", ImageUrl="https://www.livemint.com/lm-img/img/2024/08/22/600x338/home_and_fitness_needs_1724317194884_1724317202090.jpg", IsInStock=true },
            new() { Id = 5, Name = "T-Shirt", Price = 25, Category = "Fashion", Description = "A classic cotton t-shirt, perfect for everyday wear.", ImageUrl="https://images-na.ssl-images-amazon.com/images/I/71+Pvh7WB6L._AC_UL375_SR375,375_.jpg", IsInStock=true },
            new() { Id = 6, Name = "Microwave Oven", Price = 300, Category = "Home", Description = "A smart microwave oven with multiple cooking presets.", ImageUrl="https://img.etimg.com/photo/msid-84709710,imgsize-240168/alexa-kindle-fire-tv.jpg", IsInStock=true },
            new() { Id = 7, Name = "Refrigerator", Price = 900, Category = "Home", Description = "A modern refrigerator with a large capacity and energy-efficient design.", ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdRh2xVHLYLtoiMGHkqAXMOX8DUAyVmGWRIg&s", IsInStock=true },
            new() { Id = 8, Name = "Blender", Price = 75, Category = "Home", Description = "A high-speed blender for making smoothies and shakes.", ImageUrl="https://apollo.olx.in/v1/files/m5d3gctm1je5-IN/image;s=300x600;q=60", IsInStock=true },
            new() { Id = 9, Name = "Gaming Console", Price = 500, Category = "Electronics", Description = "The ultimate gaming console for a next-level gaming experience.", ImageUrl="https://www.livemint.com/lm-img/img/2024/08/22/600x338/home_and_fitness_needs_1724317194884_1724317202090.jpg", IsInStock=true },
            new() { Id = 10, Name = "Keyboard", Price = 45, Category = "Electronics", Description = "A mechanical keyboard with a comfortable and responsive feel.", ImageUrl="https://images-na.ssl-images-amazon.com/images/I/71+Pvh7WB6L._AC_UL375_SR375,375_.jpg", IsInStock=true },
            new() { Id = 11, Name = "Office Chair", Price = 200, Category = "Furniture", Description = "An ergonomic office chair for all-day comfort and support.", ImageUrl="https://img.etimg.com/photo/msid-84709710,imgsize-240168/alexa-kindle-fire-tv.jpg", IsInStock=true },
            new() { Id = 12, Name = "Desk Lamp", Price = 40, Category = "Home", Description = "A modern desk lamp with adjustable brightness.", ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdRh2xVHLYLtoiMGHkqAXMOX8DUAyVmGWRIg&s", IsInStock=true },
            new() { Id = 13, Name = "Jeans", Price = 60, Category = "Fashion", Description = "Comfortable and stylish denim jeans with a modern fit.", ImageUrl="https://apollo.olx.in/v1/files/m5d3gctm1je5-IN/image;s=300x600;q=60", IsInStock=false },
            new() { Id = 14, Name = "Tablet", Price = 400, Category = "Electronics", Description = "A versatile tablet for entertainment and productivity on the go.", ImageUrl="https://www.livemint.com/lm-img/img/2024/08/22/600x338/home_and_fitness_needs_1724317194884_1724317202090.jpg", IsInStock=true },
            new() { Id = 15, Name = "Smartwatch", Price = 250, Category = "Electronics", Description = "A smartwatch with fitness tracking and smart notifications.", ImageUrl="https://images-na.ssl-images-amazon.com/images/I/71+Pvh7WB6L._AC_UL375_SR375,375_.jpg", IsInStock=true },
            new() { Id = 16, Name = "Cookware Set", Price = 180, Category = "Home", Description = "A complete cookware set for all your culinary needs.", ImageUrl="https://img.etimg.com/photo/msid-84709710,imgsize-240168/alexa-kindle-fire-tv.jpg", IsInStock=true },
            new() { Id = 17, Name = "Sofa", Price = 1200, Category = "Furniture", Description = "A comfortable and elegant sofa for your living room.", ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdRh2xVHLYLtoiMGHkqAXMOX8DUAyVmGWRIg&s", IsInStock=true },
            new() { Id = 18, Name = "Backpack", Price = 70, Category = "Fashion", Description = "A durable and spacious backpack for school or travel.", ImageUrl="https://apollo.olx.in/v1/files/m5d3gctm1je5-IN/image;s=300x600;q=60", IsInStock=true },
            new() { Id = 19, Name = "Washing Machine", Price = 1100, Category = "Home", Description = "A powerful and efficient washing machine with a large drum capacity.", ImageUrl="https://www.livemint.com/lm-img/img/2024/08/22/600x338/home_and_fitness_needs_1724317194884_1724317202090.jpg", IsInStock=false },
            new() { Id = 20, Name = "Book: C# Basics", Price = 30, Category = "Books", Description = "An introductory book to learn the fundamentals of C# programming.", ImageUrl="https://images-na.ssl-images-amazon.com/images/I/71+Pvh7WB6L._AC_UL375_SR375,375_.jpg", IsInStock=true }
        };
    }
}