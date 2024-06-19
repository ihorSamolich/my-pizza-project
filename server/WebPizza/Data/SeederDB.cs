using Microsoft.EntityFrameworkCore;
using WebPizza.Data.Entities;

namespace WebPizza.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PizzaDbContext>();
                context.Database.Migrate();

                if (!context.Categories.Any())
                {
                    var supy = new CategoryEntity
                    {
                        Name = "Супи",
                        Image = "supy.webp"
                    };
                    var pizza = new CategoryEntity
                    {
                        Name = "Піца",
                        Image = "pizza.webp"
                    }; 
                    var sushi = new CategoryEntity
                    {
                        Name = "Суші",
                        Image = "sushi.webp"
                    };

                    context.Categories.Add(supy);
                    context.Categories.Add(pizza);
                    context.Categories.Add(sushi);
                    context.SaveChanges();
                }

            }
        }
    }
}
