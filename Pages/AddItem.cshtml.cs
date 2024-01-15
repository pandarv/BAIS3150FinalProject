using System.Text.RegularExpressions;
using BAIS3150FinalProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150FinalProject.Pages
{
    public class AddItemModel : PageModel
    {
        [BindProperty] 
        public string Message { get; set; } = string.Empty;

        [BindProperty]
        public string ItemCode { get; set; } = string.Empty;

        [BindProperty]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        public string UnitPrice {  get; set; } = string.Empty;

        [BindProperty]
        public string StockQuantity {  get; set; } = string.Empty;

        [BindProperty]
        public string IsDeleted {  get; set; } = string.Empty;


        public void OnGet()
        {
        }

        public void OnPost()
        {
            Item AcceptedItem = new()
            {
                ItemCode = ItemCode,
                Description = Description,
                UnitPrice = decimal.Parse(UnitPrice),
                StockQuantity = int.Parse(StockQuantity),
                IsDeleted = bool.Parse(IsDeleted)
            };
            
            if (!Regex.IsMatch(ItemCode, "^[A-Z]\\d{5}$"))
            {
                ModelState.AddModelError("ItemCode", "Required - less than 6 characters - Pattern 'A99999'");
            }

            if (Description.Length == 0 || Description.Length > 255)
            {
                ModelState.AddModelError("Description", "Required - less than 256 characters.");
            }

            if (!Regex.IsMatch(UnitPrice, @"^\d+(\.\d{1,2})?$"))
            {
                ModelState.AddModelError("UnitPrice", "Required - Pattern '999.99'");
            }

            if (!Regex.IsMatch(StockQuantity, "^(0|[1-9]\\d*)$"))
            {
                ModelState.AddModelError("StockQuantity", "0 or above");
            }
            else
            {
                ABCPOS RequestDirector = new();
                bool Confirmation;

                Confirmation = RequestDirector.AddInventory(AcceptedItem);

                if (Confirmation)
                {
                    Message = "Successfully Item is Added";
                    
                    ItemCode = String.Empty;
                    Description = String.Empty;
                    UnitPrice = String.Empty;
                    StockQuantity = String.Empty;
                    IsDeleted = String.Empty;
                }
            }
        }
    }
}
