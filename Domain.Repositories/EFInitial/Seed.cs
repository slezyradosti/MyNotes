using Domain.Models;

namespace Domain.Repositories.EFInitial
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            var notebooks = new List<Notebook>();
            var units = new List<Unit>();
            var pages = new List<Page>();
            var notes = new List<Note>();

            if (!context.Notebooks.Any())
            {
                notebooks = new List<Notebook>
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
            else
            {
                notebooks = context.Notebooks.ToList();
            }


            if (!context.Units.Any())
            {
                units = new List<Unit>
                {
                    new Unit
                    {
                        Name = "Unit1.0",
                        Notebook = notebooks[0]
                    },
                    new Unit
                    {
                        Name = "Unit1.1",
                        Notebook = notebooks[0]
                    },
                    new Unit
                    {
                        Name = "Unit2",
                        Notebook = notebooks[1]
                    },
                    new Unit
                    {
                        Name = "Unit3.0",
                        Notebook = notebooks[2]
                    },
                    new Unit
                    {
                        Name = "Unit3.1",
                        Notebook = notebooks[2]
                    }
                };

                await context.Units.AddRangeAsync(units);
            }

            if (!context.Pages.Any())
            {
                pages = new List<Page>
                {
                    new Page
                    {
                        Name = "Page1.0",
                        Unit = units[0]
                    },
                    new Page
                    {
                        Name = "Page1.1",
                        Unit = units[0]
                    },
                    new Page
                    {
                        Name = "Page1.2",
                        Unit = units[0]
                    },
                    new Page
                    {
                        Name = "Page2.0",
                        Unit = units[1]
                    },
                    new Page
                    {
                        Name = "Page3.0",
                        Unit = units[2]
                    },
                    new Page
                    {
                        Name = "Page3.1",
                        Unit = units[2]
                    }
                };

                await context.Pages.AddRangeAsync(pages);
            }

            if (!context.Notes.Any())
            {
                notes = new List<Note>
                {
                    new Note
                    {
                        Record = "Note1.0",
                        Page = pages[0]
                    },
                    new Note
                    {
                        Record = "Note1.1",
                        Page = pages[0]
                    },
                    new Note
                    {
                        Record = "Note1.2",
                        Page = pages[0]
                    },
                    new Note
                    {
                        Record = "Note1.3",
                        Page = pages[0]
                    },
                    new Note
                    {
                        Record = "Note2.0",
                        Page = pages[1]
                    },
                    new Note
                    {
                        Record = "Note2.1",
                        Page = pages[1]
                    },
                    new Note
                    {
                        Record = "Note2.2",
                        Page = pages[1]
                    },
                    new Note
                    {
                        Record = "Note3.0",
                        Page = pages[2]
                    }
                };

                await context.Notes.AddRangeAsync(notes);
            }

            await context.SaveChangesAsync();

        }
    }
}
