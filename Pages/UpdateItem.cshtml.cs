using System.Text.RegularExpressions;
using BAIS3150FinalProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150FinalProject.Pages
{
    public class UpdateItemModel : PageModel
    {
        
        [BindProperty]
        public string Message { get; set; } = string.Empty;

        [BindProperty]
        public string ItemCode { get; set; } = string.Empty;

        [BindProperty]
        public string Description { get; set; } = string.Empty;

        [BindProperty]
        public string UnitPrice { get; set; } = string.Empty;

        [BindProperty]
        public string StockQuantity { get; set; } = string.Empty;

        [BindProperty] public bool IsDeleted { get; set; }
        
        [BindProperty] public string Submit { get; set; } = String.Empty;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            ABCPOS RequestDirector = new();
            Item AcceptedItem = new();

            switch (Submit)
            {
                case "Find Item":
                    if (!Regex.IsMatch(ItemCode, "^[A-Z]\\d{5}$"))
                    {
                        ModelState.AddModelError("ItemCode", "Required - less than 6 characters - Pattern 'A99999'");
                    }
                    else
                    {
                        AcceptedItem = RequestDirector.FindInventory(ItemCode);

                        if (AcceptedItem.ItemCode.Trim() == "")
                        {
                            Message = "Item Not Found";
                        }
                        else
                        {
                            ItemCode = AcceptedItem.ItemCode;
                            Description = AcceptedItem.Description;
                            UnitPrice = AcceptedItem.UnitPrice.ToString();
                            StockQuantity = AcceptedItem.StockQuantity.ToString();
                            IsDeleted = AcceptedItem.IsDeleted;
                        }
                    }
                    break;
                case "Update Item":
                    bool Confirmation;
                    AcceptedItem.ItemCode = ItemCode;
                    AcceptedItem.Description = Description;
                    AcceptedItem.UnitPrice = decimal.Parse(UnitPrice);
                    AcceptedItem.StockQuantity = int.Parse(StockQuantity);
                    AcceptedItem.IsDeleted = IsDeleted;
                    
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
                        

                        Confirmation = RequestDirector.ModifyInventory(AcceptedItem);

                        if (Confirmation)
                        {
                            Message = "Successfully Item is Updated";
                    
                            ItemCode = String.Empty;
                            Description = String.Empty;
                            UnitPrice = String.Empty;
                            StockQuantity = String.Empty;
                        }
                    }
                    break;
            }
        }
    }
}
