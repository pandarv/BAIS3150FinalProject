using System.Text.RegularExpressions;
using BAIS3150FinalProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150FinalProject.Pages
{
    public class DeleteItemModel : PageModel
    {
        [BindProperty]
        public string Message { get; set; } = string.Empty;

        [BindProperty]
        public string ItemCode { get; set; } = string.Empty;
        
        [BindProperty] 
        public string Submit { get; set; } = String.Empty;

        public bool IsDeleted { get; set; }
        

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
                        AcceptedItem = RequestDirector.FindActiveInventory(ItemCode);

                        if (AcceptedItem.ItemCode.Trim() == "")
                        {
                            Message = IsDeleted ? "" : "Item Not Found";
                        }
                        else
                        {
                            ItemCode = AcceptedItem.ItemCode;
                            IsDeleted = AcceptedItem.IsDeleted;

                            Message = $"{AcceptedItem.ItemCode} is ready to be deleted.";
                        }
                    }
                    break;
                case "Delete Item":
                    bool Confirmation = false;
                    AcceptedItem.ItemCode = ItemCode;

                    Confirmation = RequestDirector.RemoveInventory(AcceptedItem.ItemCode);

                    if (Confirmation)
                    {
                        Message = "Successfully Deleted";
                        
                        ItemCode = String.Empty;
                    }
                    break;
            }
        }
    }
}
