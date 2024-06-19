using ClassLibrary1;
using Lab14__;
using System.Text.RegularExpressions;

namespace TestProjectLab14
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Galaxy_Constructor() //Конструктор галактики
        {
            Galaxy galaxy = new Galaxy();
            Assert.IsNotNull(galaxy.Name);
        }
        [TestMethod]
        public void Galaxy_NameSet()  //Установка имени галактики
        {
            Galaxy galaxy = new Galaxy();
            string Name = "Новая Галактика";
            galaxy.Name = Name;
            Assert.AreEqual(Name, galaxy.Name);
        }
        [TestMethod]
        public void Galaxy_NoNameSet()  //Установка имени галактики
        {
            Galaxy galaxy = new Galaxy();
            string Name = " ";
            galaxy.Name = Name;
            Assert.AreEqual("No name", galaxy.Name);
        }
        [TestMethod]
        public void Galaxy_Add()  //Добавление 
        {
            Galaxy galaxy = new Galaxy();
            CelestialBody body = new CelestialBody();
            galaxy.Add(body);
            Assert.IsTrue(galaxy.ContentsGalaxy.ContainsKey(body.Name));
            Assert.AreEqual(body, galaxy.ContentsGalaxy[body.Name]);
        }
        [TestMethod]
        public void Galaxy_Add2()  //Добавление в словарь повторяющегося элемента
        {
            Galaxy galaxy = new Galaxy();
            CelestialBody body = new CelestialBody();
            galaxy.Add(body);
            CelestialBody duplicateBody = new CelestialBody();
            galaxy.Add(duplicateBody);
            Assert.AreEqual(1, galaxy.ContentsGalaxy.Count);
        }
        [TestMethod]
        public void MakeGalax()  //Формирование галактики
        {
            Galaxy galaxy = new Galaxy();
            galaxy.MakeGalaxy();
            var celestialBodies = galaxy.ContentsGalaxy.Values;
            Assert.IsTrue(celestialBodies.Where(celbody => celbody is CelestialBody).Count() <= 20);
            Assert.IsTrue(celestialBodies.Where(celbody => celbody is Planet).Count() <= 10);
            Assert.IsTrue(celestialBodies.Where(celbody => celbody is GasGigant).Count() <= 5);
            Assert.IsTrue(celestialBodies.Where(celbody => celbody is Star).Count() <= 5);
        }
        [TestMethod]
        public void MaxTemperature()  //Максимальная температура звезды
        {
            var galaxy1 = new Galaxy();
            galaxy1.ContentsGalaxy.Add("Star1", new Star { Temperature = 5000 });
            galaxy1.ContentsGalaxy.Add("Planet1", new Planet());
            galaxy1.ContentsGalaxy.Add("Star2", new Star { Temperature = 6000 });
            var galaxy2 = new Galaxy();
            galaxy2.ContentsGalaxy.Add("Star3", new Star { Temperature = 4500 });
            var galaxy3 = new Galaxy();
            galaxy3.ContentsGalaxy.Add("Star4", new Star { Temperature = 7000 });
            galaxy3.ContentsGalaxy.Add("Star5", new Star { Temperature = 5500 });
            var galaxies = new List<Galaxy> { galaxy1, galaxy2, galaxy3 };
            var maxTemperature = Galaxy.MaxTemperature(galaxies);
            Assert.AreEqual(7000, maxTemperature);
        }

        [TestMethod]
        public void MinTemperature() //Минимальная температура звезды
        {
            var galaxy1 = new Galaxy();
            galaxy1.ContentsGalaxy.Add("Star1", new Star { Temperature = 5000 });
            galaxy1.ContentsGalaxy.Add("Planet1", new Planet());
            galaxy1.ContentsGalaxy.Add("Star2", new Star { Temperature = 6000 });
            var galaxy2 = new Galaxy();
            galaxy2.ContentsGalaxy.Add("Star3", new Star { Temperature = 3000 });
            var galaxy3 = new Galaxy();
            galaxy3.ContentsGalaxy.Add("Star4", new Star { Temperature = 7000 });
            galaxy3.ContentsGalaxy.Add("Star5", new Star { Temperature = 5500 });
            var galaxies = new List<Galaxy> { galaxy1, galaxy2, galaxy3 };
            var minTemperature = Galaxy.MinTemperature(galaxies);
            Assert.AreEqual(3000, minTemperature);
        }
        [TestMethod]
        public void Intersect()  //Пересечение
        {
            CelestialBody common1 = new CelestialBody("Common1", 1, 1, 1);
            CelestialBody common2 = new CelestialBody("Common2", 1, 1, 1);
            CelestialBody celbody1 = new CelestialBody("1", 1, 1, 1);
            CelestialBody celbody2 = new CelestialBody("2", 1, 1, 1);
            var galaxy1 = new Galaxy();  //Галактика 1
            galaxy1.Add(common1);
            galaxy1.Add(common2);
            galaxy1.Add(celbody1);
            var galaxy2 = new Galaxy();  //Галактика 2
            galaxy2.Add(common1);
            galaxy2.Add(common2);
            galaxy2.Add(celbody2);
            var intersect = Galaxy.Intersect(galaxy1, galaxy2);
            var CommonNames = new[] { "Common1", "Common2" };
            CollectionAssert.AreEquivalent(CommonNames, intersect.Select(cb => cb.Name).ToList());
        }
        [TestMethod]
        public void GroupByType()  //Группировка по типу
        {
            Galaxy galaxy1 = new Galaxy();
            Galaxy galaxy2 = new Galaxy();
            Star star1 = new Star("Star1", 1, 1, 1, 1);
            Star star2 = new Star("Star2", 1, 1, 1, 1);
            Planet planet = new Planet("Planet", 1, 1, 1, 1);
            GasGigant gasGiant = new GasGigant("GasGigant", 1, 1, 1, 1, true);
            galaxy1.Add(star1);
            galaxy1.Add(star2);
            galaxy1.Add(planet);
            galaxy2.Add(gasGiant);
            var galaxies = new List<Galaxy> { galaxy1, galaxy2 };
            var groupedResult = Galaxy.GroupByType(galaxies);
            var groupList = groupedResult.ToList();
            Assert.AreEqual(3, groupList.Count); //3 группы
        }
        [TestMethod]
        public void GroupByRadius()  //Группировка по радиусу
        {
            Galaxy galaxy1 = new Galaxy();
            Galaxy galaxy2 = new Galaxy();
            Star star1 = new Star("Star1", 1, 100, 1, 1);
            Star star2 = new Star("Star2", 1, 2000, 1, 1);
            Planet planet1 = new Planet("Planet1", 1, 3500, 1, 1);
            Planet planet2 = new Planet("Planet2", 1, 3600, 1, 1);
            GasGigant gasGiant = new GasGigant("GasGigant", 1, 6000, 1, 1, true);
            galaxy1.Add(star1);
            galaxy1.Add(planet1);
            galaxy1.Add(planet2);
            galaxy2.Add(star2);
            galaxy2.Add(gasGiant);
            var galaxies = new List<Galaxy> { galaxy1, galaxy2 };
            var groupedResult = Galaxy.GroupByRadius(galaxies);
            var groupList = groupedResult.ToList();
            Assert.AreEqual(4, groupList.Count);
        }
        [TestMethod]
        public void Volume() //Обьем небесного тела
        {
            // Подготовка данных
            var galaxy1 = new Galaxy
            {
                ContentsGalaxy = new Dictionary<string, CelestialBody>
                {
                    { "Earth", new CelestialBody { Name = "Earth", Radius = 6371 } }
                }
            };
            var galaxy2 = new Galaxy
            {
                ContentsGalaxy = new Dictionary<string, CelestialBody>
                {
                    { "Mars", new CelestialBody { Name = "Mars", Radius = 33 } }
                }
            };

            var galaxies = new List<Galaxy> { galaxy1, galaxy2 };
            var results = Galaxy.Volume(galaxies).ToList();
            Assert.AreEqual(2, results.Count);
        }
        [TestMethod]
        public void GalaxyInfo_DefaultConstructor()  //Конструктор без параметров 
        {
            var galaxyInfo = new GalaxyInfo();
            Assert.AreEqual("No name", galaxyInfo.Name);
            Assert.AreEqual("No address", galaxyInfo.Address);
            Assert.AreEqual("No type", galaxyInfo.Type);
        }
        [TestMethod]
        public void GalaxyInfo_Constructor()  //Конструктор с параметрами
        {
            string name = "Name";
            string address = "Address";
            string type = "Type";
            var galaxyInfo = new GalaxyInfo(name, type, address);
            Assert.AreEqual(name, galaxyInfo.Name);
            Assert.AreEqual(address, galaxyInfo.Address);
            Assert.AreEqual(type, galaxyInfo.Type);
        }
        [TestMethod]
        public void GalaxyInfo_Set()  //Сеттер
        {
            var galaxyInfo = new GalaxyInfo();
            galaxyInfo.Name = "";
            galaxyInfo.Type = "";
            galaxyInfo.Address = "";

            Assert.AreEqual("No name", galaxyInfo.Name);
            Assert.AreEqual("No address", galaxyInfo.Address);
            Assert.AreEqual("No type", galaxyInfo.Type);
        }
        [TestMethod]
        public void GalaxyInfo_RandomInit()  //Заполнение с помощью дсч
        {
            var galaxyInfo = new GalaxyInfo();
            galaxyInfo.RandomInit();
            Regex regex = new Regex("[А-Яа-яA-Za-z0-9]+");
            Assert.IsTrue(regex.IsMatch(galaxyInfo.Name));
            Assert.IsTrue(regex.IsMatch(galaxyInfo.Type));
            Assert.IsTrue(regex.IsMatch(galaxyInfo.Address));
        }
        [TestMethod]
        public void AveragePlanetWeight()
        {
            var collection = new List<CelestialBody> 
            {
                new Planet("Planet1",10,10,10,10),
                new Planet("Planet2",20,20,20,20),
                new Planet ("Planet3", 30, 30, 30, 30),
                new Star ("Star1",50,50,50,50),
                new GasGigant("GasGigant1",40,40,40,40,true)
            };
            double averageWeight = Program.AveragePlanetWeight(collection);
            double weight = 0;
            int count = 0;
            foreach (CelestialBody item in collection)
            {
                if (item is Planet)
                {
                    weight += item.Weight;
                    count++;
                }
            }
            Assert.AreEqual(weight / count, averageWeight);
        }
        [TestMethod]
        public void AveragePlanetWeight2()
        {
            var collection = new List<CelestialBody>
            {
                new Star ("Star1",50,50,50,50),
            };
            double averageWeight = Program.AveragePlanetWeight(collection);
            double weight = 0;
            int count = 0;
            foreach (CelestialBody item in collection)
            {
                if (item is Planet)
                {
                    weight += item.Weight;
                    count++;
                }
            }
            Assert.AreEqual(weight / count, averageWeight);
        }
        [TestMethod]
        public void SumSatellites()
        { 
            var collection = new List<CelestialBody>
            {
                new Planet("Planet1",10,10,10,10),
                new Planet("Planet2",20,20,20,20),
                new Planet ("Planet3", 30, 30, 30, 30),
                new Star ("Star1",50,50,50,50),
                new GasGigant("GasGigant1",40,40,40,40,true)
            };
            int Satellites = Program.SumSatellites(collection);
            int sumSatellites = 0;
            foreach (var item in collection)
            {
                var planet = item as Planet;
                if (planet != null)
                {
                    sumSatellites += planet.Satellites;
                }
            }
            Assert.AreEqual(sumSatellites, Satellites); 
        }
        [TestMethod]
        public void CountPlanets()
        {
            var collection = new List<CelestialBody>
            {
                new Planet("Planet1",10,10,10,10),
                new Planet("Planet2",20,20,20,20),
                new Planet ("Planet3", 30, 30, 30, 30),
                new Star ("Star1",50,50,50,50),
                new GasGigant("GasGigant1",40,40,40,40,true)
            };
            int planetCount = Program.CountPlanets(collection);
            int count = 0;
            foreach (var item in collection)
            {
                if (item is Planet)
                    count++;
            }
            Assert.AreEqual(count, planetCount); 
        }
        [TestMethod]
        public void CountStar()
        {
            var collection = new List<CelestialBody>
    {
        new Star { Name = "Star1" },
        new Star { Name = "Star2" },
        new Star { Name = "Star3" }
    };
            int starCount = Program.CountStars(collection);
            Assert.AreEqual(3, starCount); // Ожидаемое количество звезд: 3
        }
        [TestMethod]
        public void GroupById()
        {
            var collection = new List<CelestialBody>
    {
        new CelestialBody("1",1,1,1),
        new CelestialBody("2",1,1,1),
        new CelestialBody("3",1,1,2),
        new CelestialBody ("3",1,1,2)
    };
            var groupedResult = Program.GroupById(collection);
            var groupList = groupedResult.ToList();

            Assert.AreEqual(2, groupList.Count); //Ожидаемое количество групп: 2
        }
        [TestMethod]
        public void GroupByIdLinq()
        {
            var collection = new List<CelestialBody>
            {
                new CelestialBody("1",1,1,1),
                new CelestialBody("2",1,1,1),
                new CelestialBody("3",1,1,2),
                new CelestialBody ("3",1,1,2)
            };
            var groupedResult = Program.GroupByIdLinq(collection);
            var groupList = groupedResult.ToList();

            Assert.AreEqual(2, groupList.Count); //Ожидаемое количество групп: 2
        }
    }
}