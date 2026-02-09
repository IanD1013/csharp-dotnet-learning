/*
 * The Factory Family:
 * 1. Simple Factory Pattern
 * 2. Factory Method Pattern: define an interface for creating an object, but let subclasses decide which class to instantiate. Factory Method lets a class defer instantiation to subclasses.
 * 3. Abstract Factory Pattern
 */

using FactoryMethod.FactoryMethod.Enemies;
using FactoryMethod.FactoryMethod.Levels;

// Using the Factory Method Pattern
Level level1 = LevelFactory.CreateLevel(levelNumber: 1);
level1.EncounterEnemy();

Level level2 = LevelFactory.CreateLevel(levelNumber: 2);
level2.EncounterEnemy();
