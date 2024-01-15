using System.Text.RegularExpressions;
using BAIS3150FinalProject.Domain;
using BAIS3150FinalProject.TechnicalServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Newtonsoft.Json;

namespace BAIS3150FinalProject.Pages
{
    public class ProcessSaleModel : PageModel
    {
        [BindProperty] public string Message { get; set; } = String.Empty;

        public string CustomerMessage { get; set; } = string.Empty;

        public List<Employee> Employees = new();
        public int saleNumber { get; set; }


        [BindProperty]
        public string Submit { get; set; } = string.Empty;

        [BindProperty]
        public string EmployeeID { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;

        // Customer Information
        [BindProperty]
        public string CustomerID { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        [BindProperty] public string PhoneNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public bool IsCustomerDeleted { get; set; }

        // Item

        [BindProperty]
        public string ItemCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool isItemDeleted { get; set; }

        [BindProperty]
        public string SaleDate { get; set; } = string.Empty;
        [BindProperty]
        public string SubTotal { get; set; } = string.Empty;
        [BindProperty]
        public string GST { get; set; } = string.Empty;
        [BindProperty]
        public string SaleTotal { get; set; } = string.Empty;

        [BindProperty]
        public string ListSaleItem { get; set; } = string.Empty;


        public void OnGet()
        {
            ABCPOS RequestDirector = new();
            Employees = RequestDirector.FindEmployees();

            HttpContext.Session.SetString("EmployeeID", EmployeeID);
            // string SelectedEmployeeID = HttpContext.Session.GetString("EmployeeID")!;

            HttpContext.Session.SetString("CustomerID", CustomerID);
            // string SelectedCustomerID = HttpContext.Session.GetString("CustomerID")!;

            // if (string.IsNullOrEmpty(HttpContext.Session.GetString("CustomerID")))
            // {
            //     HttpContext.Session.SetString("CustomerID", CustomerID);
            // }
            //
            // if (string.IsNullOrEmpty(HttpContext.Session.GetString("EmployeeID")))
            // {
            //     HttpContext.Session.SetString("EmployeeID", EmployeeID);
            // }

        }

        public void OnPost()
        {

            ABCPOS RequestDirector = new();
            Customer AcceptedCustomer = new();
            Employee ActiveEmployee = new();
            Item AcceptedItem = new();

            switch (Submit)
            {
                case "Select Employee":

                    if (string.IsNullOrEmpty(EmployeeID))
                    {
                        ModelState.AddModelError("EmployeeID", "Please select an employee.");
                    }
                    else
                    {
                        ActiveEmployee = RequestDirector.FindSingleEmployee(int.Parse(EmployeeID));
                        if (ActiveEmployee == null || string.IsNullOrEmpty(ActiveEmployee.EmployeeName) || ActiveEmployee.EmployeeID == 0)
                        {
                            Message = "Employee Not Found";
                        }
                        else
                        {
                            EmployeeID = ActiveEmployee.EmployeeID.ToString();
                            EmployeeName = ActiveEmployee.EmployeeName;
                        }
                    }

                    break;

                case "Find Customer":

                    if (string.IsNullOrEmpty(EmployeeID))
                    {
                        ModelState.AddModelError("EmployeeID", "Please select an employee.");
                    }
                    else if (!Regex.IsMatch(PhoneNumber, "^[2-9]\\d{9}$"))
                    {
                        ModelState.AddModelError("PhoneNumber", "Required - length of 10 - first digit should be 2 and above");
                    }
                    else
                    {
                        ActiveEmployee = RequestDirector.FindSingleEmployee(int.Parse(EmployeeID));
                        AcceptedCustomer = RequestDirector.FindCustomerByPhoneNumber(PhoneNumber);

                        if (ActiveEmployee.EmployeeID == 0)
                        {
                            Message = "Employee Not Found";
                        }
                        else if (AcceptedCustomer.CustomerID == 0)
                        {
                            CustomerMessage = "Customer Not Found";
                        }
                        else
                        {

                            // Employee
                            EmployeeID = ActiveEmployee.EmployeeID.ToString();
                            EmployeeName = ActiveEmployee.EmployeeName;

                            // Customer
                            CustomerMessage = $"Found Customer";
                            CustomerID = AcceptedCustomer.CustomerID.ToString();
                            FirstName = AcceptedCustomer.FirstName;
                            LastName = AcceptedCustomer.LastName;
                            Address = AcceptedCustomer.Address;
                            PhoneNumber = AcceptedCustomer.PhoneNumber;
                            City = AcceptedCustomer.City;
                            Province = AcceptedCustomer.Province;
                            PostalCode = AcceptedCustomer.PostalCode;
                            IsCustomerDeleted = AcceptedCustomer.IsDeleted;

                        }
                    }

                    break;

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
                            Message = "Item Not Found";
                        }
                        else
                        {
                            ItemCode = AcceptedItem.ItemCode;
                            Description = AcceptedItem.Description;
                            UnitPrice = AcceptedItem.UnitPrice;
                            StockQuantity = AcceptedItem.StockQuantity;
                            isItemDeleted = AcceptedItem.IsDeleted;
                        }
                    }
                    break;

                case "Process Sale":

                    //List<SaleItem> SaleItemList = JsonSerializer.Deserialize<List<SaleItem>>(ListSaleItem)!;
                    List<SaleItem> SaleItemList = JsonConvert.DeserializeObject<List<SaleItem>>(ListSaleItem)!;



                    Sale NewSale = new()
                    {
                        CustomerID = int.Parse(CustomerID),
                        EmployeeID = int.Parse(EmployeeID),
                        SaleDate = DateTime.Parse(SaleDate),
                        SubTotal = decimal.Parse(SubTotal),
                        GST = decimal.Parse(GST),
                        SaleTotal = decimal.Parse(SaleTotal),
                        SaleItemList = SaleItemList,
                    };


                    saleNumber = RequestDirector.ProcessSale(NewSale);
                    break;
            }


        }

    }
}
