using Kursach.Models.Catalog;
using Kursach.Models.Database;
using Kursach.Models.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursach.Controllers
{
    public class CatalogController : Controller
    {
        private DatabaseContext _context;

        public CatalogController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ShowCatalog(int? genre, string search, string sort)
        {
            List<Genre> glist = await _context.Genres.ToListAsync();
            glist.Insert(0, new Genre { Name = "все", Id = 0});
            SelectList slist = new SelectList(glist, "Id", "Name");
            ViewBag.GenreList = slist;

            IQueryable<CatalogItemViewModel> books = null;

            //filter & search
            if (genre == null || genre == 0)
            {
                if (!String.IsNullOrEmpty(search))
                {
                    books = (from b in _context.Books
                             join c in _context.Categories
                             on b.CategoryId equals c.Id
                             where b.Name.Contains(search)
                             select new CatalogItemViewModel
                             {
                                 BookName = b.Name,
                                 CategoryName = c.Name,
                                 Description = b.Description,
                                 Id = b.Id,
                                 Image = b.Image,
                                 PublicationName = b.Publication.Name,
                                 Year = b.Year
                             });
                }
                else
                    books = (from b in _context.Books
                             join c in _context.Categories
                             on b.CategoryId equals c.Id
                             select new CatalogItemViewModel
                             {
                                 BookName = b.Name,
                                 CategoryName = c.Name,
                                 Description = b.Description,
                                 Id = b.Id,
                                 Image = b.Image,
                                 PublicationName = b.Publication.Name,
                                 Year = b.Year
                             }); //сортировка по рейтингу
            }
            else if (genre != null && genre != 0)
            {
                var genres = (from f in _context.Filters
                              where f.GenreId == genre
                              group f by f.BookId into g
                              select new
                              {
                                  g.Key
                              });
                if (!String.IsNullOrEmpty(search))
                {
                    books = (from b in _context.Books
                                     join c in _context.Categories
                                     on b.CategoryId equals c.Id
                                     where b.Name.Contains(search)
                                     select new CatalogItemViewModel
                                     {
                                         BookName = b.Name,
                                         CategoryName = c.Name,
                                         Description = b.Description,
                                         Id = b.Id,
                                         Image = b.Image,
                                         PublicationName = b.Publication.Name,
                                         Year = b.Year
                                     });

                    books = (from b in books
                             join f in genres
                             on b.Id equals f.Key
                             select new CatalogItemViewModel
                             {
                                 BookName = b.BookName,
                                 CategoryName = b.CategoryName,
                                 Description = b.Description,
                                 Id = b.Id,
                                 Image = b.Image,
                                 PublicationName = b.PublicationName,
                                 Year = b.Year
                             });
                }
                else
                {
                    books = (from b in _context.Books
                             join c in _context.Categories
                             on b.CategoryId equals c.Id
                             select new CatalogItemViewModel
                             {
                                 BookName = b.Name,
                                 CategoryName = c.Name,
                                 Description = b.Description,
                                 Id = b.Id,
                                 Image = b.Image,
                                 PublicationName = b.Publication.Name,
                                 Year = b.Year
                             });
                    books = (from b in books
                             join f in genres
                             on b.Id equals f.Key
                             select new CatalogItemViewModel
                             {
                                 BookName = b.BookName,
                                 CategoryName = b.CategoryName,
                                 Description = b.Description,
                                 Id = b.Id,
                                 Image = b.Image,
                                 PublicationName = b.PublicationName,
                                 Year = b.Year
                             });
                }
            }
            else
                books = (from b in _context.Books
                         join c in _context.Categories
                         on b.CategoryId equals c.Id
                         select new CatalogItemViewModel
                         {
                             BookName = b.Name,
                             CategoryName = c.Name,
                             Description = b.Description,
                             Id = b.Id,
                             Image = b.Image,
                             PublicationName = b.Publication.Name,
                             Year = b.Year
                         }); //сортировка по рейтингу

            //sort
            if (!String.IsNullOrEmpty(sort))
            {
                if (sort == "по имени")
                {
                    books = books.OrderByDescending(b => b.BookName);
                }
                if (sort == "по публикации")
                {
                    books = books.OrderByDescending(b => b.PublicationName);
                }
            }
            return View(await books.ToListAsync());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id != null)
            {
                Book book = await _context.Books.FirstOrDefaultAsync(p => p.Id == id);
                if (book == null) return RedirectToAction("ShowCatalog", "Catalog");
                Publication publication = await _context.Publications.FirstOrDefaultAsync(p => p.BookId == book.Id);
                IQueryable<Literature> literature = _context.Literatures.Where(p => p.BookId == book.Id);
                IQueryable<AboutBookViewModel> about = (from a in _context.Authors
                                              join l in literature
                                              on a.Id equals l.AuthorId
                                              select new AboutBookViewModel()
                                              {
                                                  LiteratureName = l.Name,
                                                  AuthorName = a.Name
                                              });

                DescriptionViewModel description = new DescriptionViewModel
                {
                    Book = book ?? null,
                    Publication = publication ?? null,
                    AboutBooks = about != null ? await about.ToListAsync() : null
                };
                IQueryable<Copy> copies = _context.Copies.Where(p => p.PublicationId == publication.Id && p.Amount > 0 && !p.IsWrittenOff);
                var storages = (from s in _context.Storages
                                                         join c in copies
                                                         on s.Id equals c.StorageId
                                                         select new 
                                                         {
                                                             Copy = c,
                                                             Id = s.Id,
                                                             RoomId = s.RoomId
                                                         });
                var rooms = (from r in _context.Rooms
                             join s in storages
                             on r.Id equals s.RoomId
                             select new
                             {
                                 storage = s,
                                 Id = r.Id,
                                 LibId = r.LibraryId
                             });

                IQueryable<LocationViewModel> locations = (from l in _context.Libraries
                                                           join r in rooms
                                                           on l.Id equals r.LibId
                                                           select new LocationViewModel()
                                                           {
                                                               CopyAmount = r.storage.Copy.Amount,
                                                               CopyId = r.storage.Copy.Id,
                                                               LibraryAddress = l.Address,
                                                               LibraryName = l.Name,
                                                               LibraryId = l.Id,
                                                               RoomId = r.Id,
                                                               StorageId = r.storage.Id
                                                           });

                //with null check
                DetailViewModel model = new DetailViewModel
                {
                    Description = description ?? null,
                    Locations = locations != null ? await locations.ToListAsync() : null
                };

                return View(model);
            }
            return NotFound();
        }
    }
}
