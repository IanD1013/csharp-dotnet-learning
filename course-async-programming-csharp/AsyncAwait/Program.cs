using AsyncAwait;

Console.WriteLine("Cooking Started");

#region FirstExample
var turkey = new Turkey();
turkey.Cook();
#endregion


#region Callbacks
turkey.Cook()
    .ContinueWith(_ =>
    {
        var gravy = new Gravy();
        gravy.Cook()
            .ContinueWith(_ =>
            {
                Console.WriteLine("Gravy Completed");
                Console.WriteLine("Ready to eat");
            });
        
    });
#endregion

Console.ReadLine();