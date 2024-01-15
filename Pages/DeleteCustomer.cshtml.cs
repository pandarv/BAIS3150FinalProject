using System.Text.RegularExpressions;
using BAIS3150FinalProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150FinalProject.Pages
{
    public class DeleteCustomerModel : PageModel
    {
        public string Message { get; set; } = string.Empty;

        [BindProperty] public string CustomerID { get; set; } = string.Empty;
        [BindProperty] public string Submit { get; set; } = String.Empty;
        
        public void OnGet()
        {
        }

        public void OnPost()
        {
            ABCPOS RequestDirector = new();
            Customer AcceptedCustomer = new();

            switch (Submit)
            {
                case "Find Customer":
                    if (!Regex.IsMatch(CustomerID, @"^[1-9]\d{3,}$"))
                    {
                        ModelState.AddModelError("CustomerID", "CustomerID is Required and must be more than or equal to 1000");
                    }
                    else
                    {
                        
                        AcceptedCustomer = RequestDirector.FindActiveCustomer(int.Parse(CustomerID));

                        if (AcceptedCustomer.CustomerID == 0)
                        {
                            Message = "Customer Not Found";
                        }
                        else
                        {
                            Message = $"Found Customer {CustomerID}";
                            CustomerID = AcceptedCustomer.CustomerID.ToString();
                        }
                    }
                    break;
                case "Delete Customer":
                    bool Confirmation = false;
                    AcceptedCustomer.CustomerID = int.Parse(CustomerID);

                    Confirmation = RequestDirector.RemoveCustomer(AcceptedCustomer.CustomerID);

                    if (Confirmation)
                    {
                        Message = "Successfully Deleted";
                        
                        CustomerID = String.Empty;
                    }
                    break;
            }
        }
    }
}
