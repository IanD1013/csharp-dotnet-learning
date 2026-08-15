// Lesson "Tuples in C#": a tuple shape can be given a name with a using alias.
// A plain `using X = (...)` is visible only in the file that declares it; the global
// form below is visible across the whole assembly. Neither name survives into metadata.
global using GlobalEndpoint = (string host, int port);
