using AsyncAwait;

// Console.WriteLine("Cooking Started");

// #region FirstExample
// var turkey = new Turkey();
// turkey.Cook();
// #endregion
//
//
// #region Callbacks
// turkey.Cook()
//     .ContinueWith(_ =>
//     {
//         var gravy = new Gravy();
//         gravy.Cook()
//             .ContinueWith(_ =>
//             {
//                 Console.WriteLine("Gravy Completed");
//                 Console.WriteLine("Ready to eat");
//             });
//         
//     });
// #endregion

// Console.ReadLine();

#region UsingAsync

Console.WriteLine("Cooking Started");
Console.WriteLine("Cooking Turkey");

var turkey = new Turkey();
await turkey.Cook(); // running on background thread

Console.WriteLine("Turkey Completed");
Console.WriteLine("Making Gravy");

var gravy = new Gravy();
await gravy.Cook();  // running on background thread

Console.WriteLine("Gravy Completed");
Console.WriteLine("Ready to Eat");

#endregion