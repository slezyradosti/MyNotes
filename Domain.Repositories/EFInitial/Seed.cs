using Domain.Models;

namespace Domain.Repositories.EFInitial
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Notes.Any())
            {
                var notes = new List<Note>
                {
                    new Note
                    {
                        Record = "Note1",
                    },
                    new Note
                    {
                        Record = "Note2",
                    },
                    new Note
                    {
                        Record = "Note3",
                    }
                };

                await context.Notes.AddRangeAsync(notes);
            }

            //var pages = new List<Page>
            //{
            //    new Page
            //    {
            //        Name = "Page1",
            //    },
            //    new Page
            //    {
            //        Name = "Page2",
            //    },
            //    new Page
            //    {
            //        Name = "Page3",
            //    }
            //};

            //var units = new List<Unit>
            //{
            //    new Unit
            //    {
            //        Name = "Unit1",
            //    },
            //    new Unit
            //    {
            //        Name = "Unit2",
            //    },
            //    new Unit
            //    {
            //        Name = "Unit3",
            //    }
            //};

            if (!context.Notebooks.Any())
            {
                var notebooks = new List<Notebook>
                {
                    new Notebook
                    {
                        Name = "Notebook1",
                    },
                    new Notebook
                    {
                        Name = "Notebook2",
                    },
                    new Notebook
                    {
                        Name = "Notebook3",
                    }
                };
                await context.Notebooks.AddRangeAsync(notebooks);
            }

            //await context.Notes.AddRangeAsync(notes);
            //await context.Pages.AddRangeAsync(pages);
            //await context.Units.AddRangeAsync(units);

            await context.SaveChangesAsync();

        }
    }
}
