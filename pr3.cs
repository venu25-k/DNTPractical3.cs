using System;
using System.Collections.Generic;

// Custom Exception
class InvalidExpenseException : Exception
{
    public InvalidExpenseException(string message) : base(message)
    {
    }
}

// Base class
abstract class Expense
{
    public int ExpenseId { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }

    public Expense(int expenseId, string description, double amount, DateTime date)
    {
        ExpenseId = expenseId;
        Description = description;
        Amount = amount;
        Date = date;
    }

    public abstract void DisplayExpense();
}

// Food Expense
class FoodExpense : Expense
{
    public FoodExpense(int id, string description, double amount, DateTime date)
        : base(id, description, amount, date)
    {
    }

    public override void DisplayExpense()
    {
        Console.WriteLine(
            $"ID: {ExpenseId}, Category: Food, Description: {Description}, " +
            $"Amount: ₹{Amount}, Date: {Date.ToShortDateString()}");
    }
}

// Travel Expense
class TravelExpense : Expense
{
    public TravelExpense(int id, string description, double amount, DateTime date)
        : base(id, description, amount, date)
    {
    }

    public override void DisplayExpense()
    {
        Console.WriteLine(
            $"ID: {ExpenseId}, Category: Travel, Description: {Description}, " +
            $"Amount: ₹{Amount}, Date: {Date.ToShortDateString()}");
    }
}

// Expense Manager
class ExpenseManager
{
    private List<Expense> expenses = new List<Expense>();

    // Add Expense
    public void AddExpense(Expense expense)
    {
        if (expense.Amount <= 0)
        {
            throw new InvalidExpenseException(
                "Expense amount must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(expense.Description))
        {
            throw new InvalidExpenseException(
                "Expense description cannot be empty.");
        }

        expenses.Add(expense);
        Console.WriteLine("Expense added successfully!");
    }

    // Display all expenses
    public void DisplayAllExpenses()
    {
        if (expenses.Count == 0)
        {
            Console.WriteLine("No expenses found.");
            return;
        }

        Console.WriteLine("\n----- Expense List -----");

        foreach (Expense expense in expenses)
        {
            expense.DisplayExpense();
        }
    }

    // Calculate total expense
    public double CalculateTotal()
    {
        double total = 0;

        foreach (Expense expense in expenses)
        {
            total += expense.Amount;
        }

        return total;
    }
}

// Main class
class Program
{
    static void Main()
    {
        ExpenseManager manager = new ExpenseManager();

        while (true)
        {
            Console.WriteLine("\n===== Expense Management System =====");
            Console.WriteLine("1. Add Food Expense");
            Console.WriteLine("2. Add Travel Expense");
            Console.WriteLine("3. Display All Expenses");
            Console.WriteLine("4. Calculate Total Expense");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");

            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddFoodExpense(manager);
                        break;

                    case 2:
                        AddTravelExpense(manager);
                        break;

                    case 3:
                        manager.DisplayAllExpenses();
                        break;

                    case 4:
                        Console.WriteLine(
                            $"Total Expense: ₹{manager.CalculateTotal()}");
                        break;

                    case 5:
                        Console.WriteLine("Thank you!");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Error: Please enter a valid number.");
            }
            catch (InvalidExpenseException ex)
            {
                Console.WriteLine("Expense Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected Error: " + ex.Message);
            }
        }
    }

    // Add Food Expense
    static void AddFoodExpense(ExpenseManager manager)
    {
        Console.Write("Enter Expense ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Description: ");
        string description = Console.ReadLine();

        Console.Write("Enter Amount: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        FoodExpense expense = new FoodExpense(
            id,
            description,
            amount,
            DateTime.Now
        );

        manager.AddExpense(expense);
    }

    // Add Travel Expense
    static void AddTravelExpense(ExpenseManager manager)
    {
        Console.Write("Enter Expense ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter Description: ");
        string description = Console.ReadLine();

        Console.Write("Enter Amount: ");
        double amount = Convert.ToDouble(Console.ReadLine());

        TravelExpense expense = new TravelExpense(
            id,
            description,
            amount,
            DateTime.Now
        );

        manager.AddExpense(expense);
    }
}