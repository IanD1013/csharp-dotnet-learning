// Core Proxy Types:
// Remote
// Virtual -> lazy loading
// Protection

// Other Proxy Types:
// Smart Reference Proxy
// Caching Proxy

using Proxy;

IImage image1 = new ProxyImage(fileName: "image1");
IImage image2 = new ProxyImage(fileName: "image2");

Console.WriteLine("Displaying image1");
image1.Display();

Console.WriteLine("Displaying image2");
image2.Display();

Console.WriteLine("Displaying image1 again");
image1.Display();