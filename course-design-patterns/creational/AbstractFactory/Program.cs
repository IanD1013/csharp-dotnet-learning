using AbstractFactory.CaveLevel;
using AbstractFactory.Common;
using AbstractFactory.HauntedHouseLevel;

void SetupEnvironment(LevelElementFactory levelFactory)
{
    levelFactory.SetupEnvironment();
}

SetupEnvironment(new CaveLevelElementFactory());
SetupEnvironment(new HauntedHouseElementFactory());