using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportShop.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();
            //context.Database.EnsureDeleted();
            context.Database.Migrate();


            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Sporty zespołowe"
                    },
                    new Category
                    {
                        Name = "Sporty wodne"
                    },
                    new Category
                    {
                        Name = "Szachy"
                    },
                };
                categories.ForEach( p => context.Categories.Add(p));
                context.SaveChanges();


            }

            

            var products = new List<Product>
            {
                new Product
                    {
                        Name = "Piłka do gry w piłkę nożną",
                        Description = "Skórzana rozmiar 5",
                        CategoryID = context.Categories.Single(s => s.Name == "Sporty zespołowe").CategoryID,
                        Price = 67,
                        Rating = 5
                    },
                new Product
                    {
                        Name = "Ochraniacze",
                        Description = "Chronią nogi",
                        CategoryID = context.Categories.Single(s => s.Name == "Sporty zespołowe").CategoryID,
                        Price = 45,
                        Rating = 5
                    },


            };

            var products2 = new List<Product>
            {
                 new Product
                    {
                        Name = "Kajak",
                        Description = "Łódka przeznaczona dla jednej osoby",
                        CategoryID = context.Categories.Single(s => s.Name == "Sporty wodne").CategoryID,
                        Price = 275,
                        Rating = 5
                    },
                    new Product
                    {
                        Name = "Kamizelka ratunkowa",
                        Description = "Chroni i dodaje uroku",
                        CategoryID = context.Categories.Single(s => s.Name == "Sporty wodne").CategoryID,
                        Price = 48.95m
                    },
            };

            var products3 = new List<Product>
            {
                new Product
                    {
                        Name = "Czapka",
                        Description = "Zwiększa efektywność mózgu o 75%",
                        CategoryID = context.Categories.Single(s => s.Name == "Szachy").CategoryID,
                        Price = 16
                    },
                    new Product
                    {
                        Name = "Niestabilne krzesło",
                        Description = "Zmniejsza szanse przeciwnika",
                        CategoryID = context.Categories.Single(s => s.Name == "Szachy").CategoryID,
                        Price = 29.95m
                    },
                    new Product
                    {
                        Name = "Ludzka szachownica",
                        Description = "Przyjemna gra dla całej rodziny!",
                        CategoryID = context.Categories.Single(s => s.Name == "Szachy").CategoryID,
                        Price = 75
                    },
                    new Product
                    {
                        Name = "Błyszczący król",
                        Description = "Figura pokryta złotem i wysadzana diamentami",
                        CategoryID =context.Categories.Single(s => s.Name == "Szachy").CategoryID,
                        Price = 1200
                    }
            };



            products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();

            if (!context.Manufacturers.Any())
            {
                context.Manufacturers.AddRange(
                    new Manufacturer
                    {
                        Name = "Nike+",
                        Country = "United Kingdom",
                        Products = products

                    },
                    new Manufacturer
                    {
                        Name = "Adidas",
                        Country = "Germany",
                        Products = products2
                        
                    },
                    new Manufacturer
                    {
                        Name = "Puma",
                        Country = "France",
                        Products = products3
                        
                    }
                    );
                context.SaveChanges();
            }

            //if (!context.Products.Any())
            //{
            //    context.Products.AddRange(
            //        new Product
            //        {
            //            Name = "Kajak",
            //            Description = "Łódka przeznaczona dla jednej osoby",
            //            Category = "Sporty wodne",
            //            Price = 275,
            //            Rating = 5
            //        },
            //        new Product
            //        {
            //            Name = "Kamizelka ratunkowa",
            //            Description = "Chroni i dodaje uroku",
            //            Category = "Sporty wodne",
            //            Price = 48.95m
            //        },
            //        new Product
            //        {
            //            Name = "Piłka",
            //            Description = "Zatwierdzone przez FIFA rozmiar i waga",
            //            Category = "Piłka nożna",
            //            Price = 19.50m
            //        },
            //        new Product
            //        {
            //            Name = "Flagi narożne",
            //            Description = "Nadadzą twojemu boisku profesjonalny wygląd",
            //            Category = "Piłka nożna",
            //            Price = 34.95m
            //        },
            //        new Product
            //        {
            //            Name = "Stadiom",
            //            Description = "Składany stadion na 35 000 osób",
            //            Category = "Piłka nożna",
            //            Price = 79500
            //        },
            //        new Product
            //        {
            //            Name = "Czapka",
            //            Description = "Zwiększa efektywność mózgu o 75%",
            //            Category = "Szachy",
            //            Price = 16
            //        },
            //        new Product
            //        {
            //            Name = "Niestabilne krzesło",
            //            Description = "Zmniejsza szanse przeciwnika",
            //            Category = "Szachy",
            //            Price = 29.95m
            //        },
            //        new Product
            //        {
            //            Name = "Ludzka szachownica",
            //            Description = "Przyjemna gra dla całej rodziny!",
            //            Category = "Szachy",
            //            Price = 75
            //        },
            //        new Product
            //        {
            //            Name = "Błyszczący król",
            //            Description = "Figura pokryta złotem i wysadzana diamentami",
            //            Category = "Szachy",
            //            Price = 1200
            //        }
            //    );
            //    context.SaveChanges();
            }
    }
}
