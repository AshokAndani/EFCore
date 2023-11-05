using CodeFirst;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Welcome to Code First Approach for EntityFramework Core");
var context = new OnlineStoreContext();

while (true)
{

    Console.WriteLine("1-> Add, 2-> Update, 3-> Delete, 4-> Display");
    switch (int.Parse(Console.ReadLine()))
    {
        case 1: await Add(); break;
        case 2: await Update(); break;
        case 3: await Delete(); break;
        case 4: await Display(); break;
        default: Console.WriteLine("Envalid Entry"); break;
    }
    await Task.Delay(4000);
}

async Task Update()
{
    Console.Write("Enter Id: ");
    int id = int.Parse(Console.ReadLine());
    //var customer = context.Customers.Where(x => x.Id == id).FirstOrDefault(); //  best for non- primary key scan.
    var customer = await context.Customers.FindAsync(id); // this is good in performance for PM key based scan.
    if (customer == null)
    {
        await Console.Out.WriteLineAsync("with enter id customer doesn't exist");
        return;
    }
    Console.Write("Enter Name: ");
    customer.Name = Console.ReadLine() ?? customer.Name;
    Console.Write("Enter Email: ");
    customer.Email = Console.ReadLine() ?? customer.Email;
    Console.WriteLine("Changed SuccessFully");
    context.Customers.Update(customer);
    await context.SaveChangesAsync();
    await Display(id);
}

async Task Add()
{
    var customer = new Customer();
    Console.Write("Enter Name: ");
    customer.Name = Console.ReadLine() ?? customer.Name;
    Console.Write("Enter Email: ");
    customer.Email = Console.ReadLine() ?? customer.Email;
    var details = await context.Customers.AddAsync(customer);
    await context.SaveChangesAsync();
    Console.WriteLine("Added SuccessFully.");
    await Display(details.Entity.Id);
}
async Task Delete()
{
    Console.Write("Enter Id: ");
    int id = int.Parse(Console.ReadLine());
    var query = context.Customers.Where(x => x.Id == id);
    await query.ExecuteDeleteAsync();
    // var q = query.ToQueryString(); to inspect query.
    await context.SaveChangesAsync();
    Console.WriteLine("Deleted SuccessFully");
}
async Task Display(int id = -1)
{
    Console.Clear();
    if (id == -1)
    {
        await context.Customers.ForEachAsync(c =>
         {
             string s = $"Id: {c.Id}\nName: {c.Name}\nEmail: {c.Email}\n-----------------------------";
             Console.WriteLine(s);
         });

        // another approach using ToListAsync
        //var customers = await context.Customers.ToListAsync();
        //customers.ForEach(x=> Console.WriteLine(x.Name));

    }
    else
    {
        var c = await context.Customers.FindAsync(id);
        Console.Clear();
        string s = $"Id: {c.Id}\nName: {c.Name}\nEmail: {c.Email}\n-----------------------------";
        Console.WriteLine(s);
    }
}
