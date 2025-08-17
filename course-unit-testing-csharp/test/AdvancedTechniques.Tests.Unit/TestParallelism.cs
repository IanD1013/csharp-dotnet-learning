using Xunit;

// [assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass)]
// [assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: CollectionBehavior(MaxParallelThreads = 32)]