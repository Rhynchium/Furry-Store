﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MSIT147thGraduationTopic.EFModels;
using MSIT147thGraduationTopic.Models.ViewModels;

namespace MSIT147thGraduationTopic.Controllers
{
    public class MerchandisesController : Controller
    {
        private readonly GraduationTopicContext _context;
        private readonly IWebHostEnvironment _host;

        public MerchandisesController(GraduationTopicContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }

        // GET: Merchandises
        public async Task<IActionResult> Index(string txtKeyword, int searchCondition)//todo 增加分頁功能(應該增加pageIndex即可)
        {
            IEnumerable<MerchandiseSearch> datas;
            datas = from m in _context.MerchandiseSearches
                    select m;
            if (!string.IsNullOrEmpty(txtKeyword))
            {
                if (searchCondition == 1)
                    datas = datas.Where(ms => ms.MerchandiseName.Contains(txtKeyword));
                if (searchCondition == 2)
                {
                    datas = null;
                    var merchandiseIdFormSpec = _context.Specs
                        .Where(s => s.SpecName.Contains(txtKeyword)).Select(s => s.MerchandiseId).Distinct();
                    
                    List<MerchandiseSearch> templist = new List<MerchandiseSearch>();
                    foreach (int id in merchandiseIdFormSpec)
                    {
                        MerchandiseSearch unit = _context.MerchandiseSearches.Where(ms => ms.MerchandiseId == id).FirstOrDefault();
                        if (unit != null)
                            templist.Add(unit);
                    }
                    datas = templist;
                }
                if (searchCondition == 3)
                    datas = datas.Where(ms => ms.BrandName.Contains(txtKeyword));
                if (searchCondition == 4)
                    datas = datas.Where(ms => ms.CategoryName.Contains(txtKeyword));
            }

            List<MerchandiseSearchVM> list = new List<MerchandiseSearchVM>();
            foreach (MerchandiseSearch ms in datas)
            {
                MerchandiseSearchVM merchandisesearchvm = new MerchandiseSearchVM();
                merchandisesearchvm.merchandisesearch = ms;
                list.Add(merchandisesearchvm);
            }

            return View(list);
        }

        // GET: Merchandises/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            MerchandiseVM merchandisevm = new MerchandiseVM();
            return View(merchandisevm);
        }

        // POST: Merchandises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create
            ([Bind("MerchandiseId,MerchandiseName,BrandId,CategoryId,Description,ImageUrl,Display,photo")]
                MerchandiseVM merchandisevm)
        {

            if (ModelState.IsValid)
            {
                if (merchandisevm.photo != null)
                {
                    merchandisevm.ImageUrl = Guid.NewGuid().ToString() + merchandisevm.photo.FileName;
                    saveMerchandiseImageToUploads(merchandisevm.ImageUrl, merchandisevm.photo);
                }

                _context.Add(merchandisevm.merchandise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", merchandisevm.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", merchandisevm.CategoryId);
            return View(merchandisevm);
        }

        // GET: Merchandises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Merchandises == null) return NotFound();

            var merchandise = await _context.Merchandises.FindAsync(id);
            if (merchandise == null) return NotFound();

            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", merchandise.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", merchandise.CategoryId);

            MerchandiseVM merchandisevm = new MerchandiseVM();
            merchandisevm.merchandise = merchandise;
            return View(merchandisevm);
        }

        // POST: Merchandises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, 
            [Bind("MerchandiseId,MerchandiseName,BrandId,CategoryId,Description,ImageUrl,Display,photo,deleteImageIndicater")] 
                    MerchandiseVM merchandisevm)
        {
            if (id != merchandisevm.MerchandiseId) return NotFound();

            if (ModelState.IsValid)
            {
                //(始終沒圖) or (有圖→沒變) => 不用動
                //沒圖→有圖
                if (merchandisevm.ImageUrl == null && merchandisevm.photo != null)
                {
                    merchandisevm.ImageUrl = Guid.NewGuid().ToString() + merchandisevm.photo.FileName;
                    saveMerchandiseImageToUploads(merchandisevm.ImageUrl, merchandisevm.photo);
                }
                //有圖→新圖
                if (merchandisevm.ImageUrl != null && merchandisevm.photo != null)
                {
                    deleteMerchandiseImageFromUploads(merchandisevm.ImageUrl);
                    merchandisevm.ImageUrl = Guid.NewGuid().ToString() + merchandisevm.photo.FileName;
                    saveMerchandiseImageToUploads(merchandisevm.ImageUrl, merchandisevm.photo);
                }
                //有圖→刪除
                if (merchandisevm.ImageUrl != null && merchandisevm.photo == null && merchandisevm.deleteImageIndicater == true)
                {
                    deleteMerchandiseImageFromUploads(merchandisevm.ImageUrl);
                    merchandisevm.ImageUrl = null;
                }

                try
                {
                    _context.Update(merchandisevm.merchandise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerchandiseExists(merchandisevm.MerchandiseId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "BrandId", "BrandName", merchandisevm.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", merchandisevm.CategoryId);
            return View(merchandisevm);
        }

        // GET: Merchandises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (_context.Specs.Where(s => s.MerchandiseId == id).Count() > 0)
                return RedirectToAction(nameof(Index));

            if (id == null || _context.Merchandises == null)
                return Problem("找不到商品資料");

            var merchandise = await _context.Merchandises
                .FirstOrDefaultAsync(m => m.MerchandiseId == id);
            if (merchandise == null) return Problem("找不到商品資料");

            if (!string.IsNullOrEmpty(merchandise.ImageUrl))
                deleteMerchandiseImageFromUploads(merchandise.ImageUrl);
            _context.Merchandises.Remove(merchandise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MerchandiseExists(int id)
        {
          return (_context.Merchandises?.Any(e => e.MerchandiseId == id)).GetValueOrDefault();
        }
        private void saveMerchandiseImageToUploads(string ImageUrl, IFormFile photo)
        {
            string savepath = Path.Combine(_host.WebRootPath, "uploads/merchandisePicture", ImageUrl);
            using (var fileStream = new FileStream(savepath, FileMode.Create))
            {
                photo.CopyTo(fileStream);
            }
        }
        private void deleteMerchandiseImageFromUploads(string ImageUrl)
        {
            string deletepath = Path.Combine(_host.WebRootPath, "uploads/merchandisePicture", ImageUrl);
            System.IO.File.Delete(deletepath);
        }
    }
}
