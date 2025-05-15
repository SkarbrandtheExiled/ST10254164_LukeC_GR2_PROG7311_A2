﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10254164_LukeC_GR2_PROG7311_A2.Models;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.farmerServices;
using ST10254164_LukeC_GR2_PROG7311_A2.Services.productServices;
using System.Security.Claims;

namespace ST10254164_LukeC_GR2_PROG7311_A2.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class productController : Controller
    {
        private readonly IProductServices _productService;
        private readonly IFarmerServices _farmerService;
        private readonly ILogger<productController> _logger;

        public productController(
            IProductServices productService,
            IFarmerServices farmerService,
            ILogger<productController> logger)
        {
            _productService = productService;
            _farmerService = farmerService;
            _logger = logger;
        }

        private async Task<farmerModel?> GetCurrentFarmerAsync()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
                return null;
            return await _farmerService.GetFarmerByUserIdAsync(userId);
        }

        // GET: Product/FarmerProducts (Dashboard)
        public async Task<IActionResult> FarmerProducts()
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null)
            {
                _logger.LogWarning("Farmer profile not found for logged-in user. Redirecting to login.");
                return RedirectToAction("Login", "Account");
            }

            var products = await _productService.GetProductsForFarmerAsync(farmer.Id);
            ViewBag.FarmerName = farmer.Name;

            var viewModel = new farmerDashboardModel
            {
                NewProduct = new productModel { ProductionDate = DateTime.Today },
                MyProducts = products
            };

            return View("~/Views/Farmer/farmerDashboard.cshtml", viewModel);
        }

        // POST: Product/Add (from dashboard)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(farmerDashboardModel viewModel)
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null)
                return RedirectToAction("Login", "Account");

            var productToAdd = viewModel.NewProduct;

            ModelState.Remove("NewProduct.Id");
            ModelState.Remove("NewProduct.FarmerId");
            ModelState.Remove("NewProduct.Farmer");
            ModelState.Remove("NewProduct.AddedDate");
            ModelState.Remove("MyProducts");

            if (ModelState.IsValid && productToAdd != null)
            {
                try
                {
                    await _productService.AddProductForFarmerAsync(productToAdd, farmer.Id);
                    TempData["SuccessMessage"] = "Product added successfully!";
                    return RedirectToAction(nameof(FarmerProducts));
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Error adding product for farmer {FarmerId}", farmer.Id);
                    ModelState.AddModelError("", "An error occurred while adding the product. Please try again.");
                }
            }

            var existingProducts = await _productService.GetProductsForFarmerAsync(farmer.Id);
            ViewBag.FarmerName = farmer.Name;
            var newViewModel = new farmerDashboardModel
            {
                NewProduct = productToAdd ?? new productModel { ProductionDate = DateTime.Today },
                MyProducts = existingProducts
            };
            return View("~/Views/Home/FarmerDashboard.cshtml", newViewModel);
        }

        // GET: Product/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null) return RedirectToAction("Login", "Account");

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null || product.FarmerId != farmer.Id)
                return NotFound();

            return View("EditProduct", product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, productModel product)
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null) return RedirectToAction("Login", "Account");

            if (id != product.Id)
                return NotFound();

            var originalProduct = await _productService.GetProductByIdAsync(id);
            if (originalProduct == null || originalProduct.FarmerId != farmer.Id)
                return NotFound();

            product.FarmerId = originalProduct.FarmerId;
            product.AddedDate = originalProduct.AddedDate;
            product.Farmer = null;

            ModelState.Remove("Farmer");

            if (ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateProductAsync(product);
                    TempData["SuccessMessage"] = "Product updated successfully!";
                    return RedirectToAction(nameof(FarmerProducts));
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError("", "The product was modified by another user. Please try again.");
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Error updating product {ProductId}", product.Id);
                    ModelState.AddModelError("", "An error occurred while updating the product.");
                }
            }
            return View("EditProduct", product);
        }

        // GET: Product/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null) return RedirectToAction("Login", "Account");

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null || product.FarmerId != farmer.Id)
                return NotFound();

            return View("DeleteProduct", product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmer = await GetCurrentFarmerAsync();
            if (farmer == null) return RedirectToAction("Login", "Account");

            var product = await _productService.GetProductByIdAsync(id);
            if (product == null || product.FarmerId != farmer.Id)
                return NotFound();

            await _productService.DeleteProductAsync(id);
            TempData["SuccessMessage"] = "Product deleted successfully!";
            return RedirectToAction(nameof(FarmerProducts));
        }
    }
}