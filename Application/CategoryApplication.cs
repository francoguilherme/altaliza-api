using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AltalizaApi.Models;

namespace AltalizaApi.Application
{
    public class CategoryApplication
    {
        private ApiContext _context;

        public CategoryApplication(ApiContext context)
        {
            _context = context;
        }

        public string InsertCategory(Category category)
        {
            try {
                if (category != null) {
                    _context.Add(category);
                    _context.SaveChanges();
                    return "Category added successfully";
                } else {
                    return "Category null";
                }
            } catch (Exception) {
                return "Error on adding Category";
            }
        }
    }
}
