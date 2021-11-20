using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTraining.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly ProjectTrainingContext _context;
        public MenuViewComponent(ProjectTrainingContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listMenu = _context.Kind;
            return View(await listMenu.ToListAsync());
        }
    }
}
