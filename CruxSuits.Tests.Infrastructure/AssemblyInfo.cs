using Xunit;

// Empêche l’exécution parallèle des tests (SQLite in-memory n’aime pas le parallélisme partagé)
[assembly: CollectionBehavior(DisableTestParallelization = true)]