using System.Text.RegularExpressions;
using BAIS3150FinalProject.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAIS3150FinalProject.Pages
{
    public class UpdateCustomerModel : PageModel
    {
        public string Message { get; set; } = string.Empty;

        [BindProperty] public string CustomerID { get; set; } = string.Empty;

        [BindProperty] public string FirstName { get; set; } = string.Empty;

        [BindProperty] public string LastName { get; set; } = string.Empty;

        [BindProperty] public string Address { get; set; } = string.Empty;

        [BindProperty] public string PhoneNumber { get; set; } = string.Empty;

        [BindProperty] public string City { get; set; } = string.Empty;

        [BindProperty] public string Province { get; set; } = string.Empty;

        [BindProperty] public string PostalCode { get; set; } = string.Empty;

        [BindProperty] public bool IsDeleted { get; set; }
        
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
                        AcceptedCustomer = RequestDirector.FindAllCustomers(int.Parse(CustomerID));

                        if (AcceptedCustomer.CustomerID == 0)
                        {
                            Message = "Customer Not Found";
                        }
                        else
                        {
                            CustomerID = AcceptedCustomer.CustomerID.ToString();
                            FirstName = AcceptedCustomer.FirstName;
                            LastName = AcceptedCustomer.LastName;
                            Address = AcceptedCustomer.Address;
                            PhoneNumber = AcceptedCustomer.PhoneNumber;
                            City = AcceptedCustomer.City;
                            Province = AcceptedCustomer.Province;
                            PostalCode = AcceptedCustomer.PostalCode;
                            IsDeleted = AcceptedCustomer.IsDeleted;
                        }
                    }
                    break;
                case "Update Customer":
                    bool Confirmation = false;
                    AcceptedCustomer.CustomerID = int.Parse(CustomerID);
                    AcceptedCustomer.FirstName = FirstName;
                    AcceptedCustomer.LastName = LastName;
                    AcceptedCustomer.Address = Address;
                    AcceptedCustomer.PhoneNumber = PhoneNumber;
                    AcceptedCustomer.City = City;
                    AcceptedCustomer.Province = Province;
                    AcceptedCustomer.PostalCode = PostalCode;
                    AcceptedCustomer.IsDeleted = IsDeleted;
                    
                    if (!Regex.IsMatch(FirstName, "^[a-zA-Z]{1,50}$"))
                    {
                        ModelState.AddModelError("FirstName", "Required - less than 50 characters");
                    }
                    if (!Regex.IsMatch(LastName, "^[a-zA-Z]{1,50}$"))
                    {
                        ModelState.AddModelError("LastName", "Required - less than 50 characters");
                    }
                    if (!Regex.IsMatch(Address, "^[a-zA-Z0-9\\s.,#-]{1,255}$"))
                    {
                        ModelState.AddModelError("Address", "Required - less than 255 characters");
                    }
                    if (!Regex.IsMatch(PhoneNumber, "^[2-9]\\d{9}$"))
                    {
                        ModelState.AddModelError("PhoneNumber", "Required - length of 10 - first digit should be 2 and above");
                    }
                    if (!Regex.IsMatch(City, "^[a-zA-Z]+(\\s[a-zA-Z]+){0,49}$"))
                    {
                        ModelState.AddModelError("City", "Required - length of 50");
                    }
                    if (!Regex.IsMatch(Province, "^[a-zA-Z]+(\\s[a-zA-Z]+){0,49}$"))
                    {
                        ModelState.AddModelError("Province", "Required - length of 50");
                    }
                    if (!Regex.IsMatch(PostalCode, "^[A-Z]\\d[A-Z]\\d[A-Z]\\d$"))
                    {
                        ModelState.AddModelError("PostalCode", "Pattern - 'X9X9X9");
                    }
                    else
                    {
                        Confirmation = RequestDirector.ModifyCustomer(AcceptedCustomer);

                        if (Confirmation)
                        {
                            Message = "Successfully Updated";
                            
                            FirstName = String.Empty;
                            LastName = String.Empty;
                            Address = String.Empty;
                            PhoneNumber = String.Empty;
                            City = String.Empty;
                            Province = String.Empty;
                            PostalCode = String.Empty;
                            IsDeleted = false;
                        }
                    }
                    break;
                    
            }
        }
    }
}
