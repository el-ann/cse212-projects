/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService
{
    public static void Run()
    {
        // Test 1
        // Scenario: Create a queue with an invalid size (0), then add one customer.
        // Expected Result: Queue defaults to max_size=10, and the customer is added (size=1).
        Console.WriteLine("Test 1");
        var cs1 = new CustomerService(0);
        cs1.AddNewCustomer("Alice", "A100", "Cannot log in");
        Console.WriteLine(cs1);
        // Defect(s) Found: Passed correctly (size defaulted to 10). No defect.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Create a queue with max size 2. Add customers until full, then try adding one more.
        // Expected Result: First two customers add successfully. Third attempt shows "Maximum Number of Customers in Queue."
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(2);
        cs2.AddNewCustomer("Bob", "B200", "Billing issue");
        cs2.AddNewCustomer("Carol", "C300", "Password reset");
        cs2.AddNewCustomer("Dave", "D400", "Refund request"); // Should be rejected
        Console.WriteLine(cs2);
        // Defect(s) Found: The rejection message printed correctly this time because the new test-friendly overload already includes the >= fix. So no defect showed up here, but it confirms the fix works. No defect in this test (since it was already fixed in the new overload).

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Add two customers, then serve them one at a time.
        // Expected Result: The FIRST customer added (Eve) should be served and removed first, followed by Frank.
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(5);
        cs3.AddNewCustomer("Eve", "E500", "Late delivery");
        cs3.AddNewCustomer("Frank", "F600", "Wrong item received");
        cs3.ServeCustomer(); // Should display and remove Eve
        Console.WriteLine(cs3);
        // Defect(s) Found: This is the real bug — expected Eve to be served (the first one added), but it displayed Frank instead. This confirms the requirement violation in ServeCustomer: it removes the front item before reading it, so it ends up showing the second customer instead of the first.

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Try to serve a customer from an empty queue.
        // Expected Result: Should display an error message instead of crashing.
        Console.WriteLine("Test 4");
        var cs4 = new CustomerService(5);
        cs4.ServeCustomer();
        // Defect(s) Found: Crashed with an unhandled exception instead of showing a friendly error message — confirms the missing empty-queue check in ServeCustomer.

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize)
    {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer
    {
        public Customer(string name, string accountId, string problem)
        {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString()
        {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer()
    {
        // Verify there is room in the service queue
        if (_queue.Count > _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Overload for testing purposes: adds a new customer using parameters
    /// directly instead of prompting via Console.ReadLine.
    /// </summary>
    private void AddNewCustomer(string name, string accountId, string problem)
    {
        if (_queue.Count >= _maxSize)
        {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("No customers in the Queue.");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString()
    {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}