using BAIS3150FinalProject.TechnicalServices;

namespace BAIS3150FinalProject.Domain
{
    public class ABCPOS
    {
        public bool AddInventory(Item acceptedItem)
        {
            bool Confirmation;
            Items InventoryManager = new();

            Confirmation = InventoryManager.AddItem(acceptedItem);

            return Confirmation;
        }

        public bool ModifyInventory(Item acceptedItem)
        {
            bool Confirmation;
            Items InventoryManager = new();

            Confirmation = InventoryManager.UpdateItem(acceptedItem);

            return Confirmation;
        }

        public bool RemoveInventory(string itemCode)
        {
            bool Confirmation;
            Items InventoryManager = new();

            Confirmation = InventoryManager.DeleteItem(itemCode);

            return Confirmation;
        }

        public Item FindInventory(string itemCode)
        {
            Item ActiveItem;
            Items InventoryManager = new();

            ActiveItem = InventoryManager.GetItem(itemCode);

            return ActiveItem;
        }
        
        public Item FindActiveInventory(string itemCode)
        {
            Item ActiveItem;
            Items InventoryManager = new();

            ActiveItem = InventoryManager.GetActiveItem(itemCode);

            return ActiveItem;
        }

        public bool AddCustomer(Customer customer)
        {
            bool Confirmation;
            Customers CustomerManager = new();

            Confirmation = CustomerManager.AddCustomer(customer);

            return Confirmation;
        }

        public bool ModifyCustomer(Customer acceptedCustomer)
        {
            bool Confirmation;
            Customers CustomerManager = new();

            Confirmation = CustomerManager.UpdateCustomer(acceptedCustomer);

            return Confirmation;
        }

        public bool RemoveCustomer(int customerID)
        {
            bool Confimation = false;
            Customers CustomerManager = new();

            Confimation = CustomerManager.DeleteCustomer(customerID);

            return Confimation;
        }

        public Customer FindAllCustomers(int customerID)
        {
            Customer ActiveCustomer;
            Customers CustomerManager = new();

            ActiveCustomer = CustomerManager.GetAllCustomers(customerID);

            return ActiveCustomer;
        }
        public Customer FindActiveCustomer(int customerID)
        {
            Customer ActiveCustomer;
            Customers CustomerManager = new();

            ActiveCustomer = CustomerManager.GetActiveCustomer(customerID);

            return ActiveCustomer;
        }

        public Customer FindCustomerByPhoneNumber(string phoneNumber)
        {
            Customer ActiveCustomer;
            Customers CustomerManager = new();

            ActiveCustomer = CustomerManager.GetCustomerByPhoneNumber(phoneNumber);

            return ActiveCustomer;
        }

        public int ProcessSale(Sale ABCSale)
        {
            int SaleNumber;
            Sales SaleManager = new();

            SaleNumber = SaleManager.AddSale(ABCSale);

            return SaleNumber;
        }

        public List<Employee> FindEmployees()
        {
            List<Employee> EmployeeList= new();
            Employees EmployeeManager = new();

            EmployeeList = EmployeeManager.GetEmployees();

            return EmployeeList;
        }

        public Employee FindSingleEmployee(int employeeID)
        {
            Employee ActiveEmployee;
            Employees EmployeeManager = new();

            ActiveEmployee = EmployeeManager.GetEmployee(employeeID);

            return ActiveEmployee;
        }
    }
}
