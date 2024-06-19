using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ClassLibrary1;

namespace Lab14__
{
   public class Galaxy
   {
       public string  Test { get; set; }
        protected string name;   //Название галактики
        public string Name       //Название галактики
        {
            get { return name; } //Свойство для чтения
            set                  //Свойство установки значения
            {
                Regex regex = new Regex("[А-Яа-яA-Za-z0-9]+");
                if (regex.IsMatch(value))
                    name = value;
                else
                    name = "No name";
            }
        }
        protected static Random rand = new Random();

        private static string[] Names = //Названия галактик
        [
            "Млечный путь", "Андромеда", "Треугольник", "Сомбреро",
            "Водоворот", "Антенна", "Центавра A"
        ];

        public Dictionary<string, CelestialBody> ContentsGalaxy { get; set; } // Словарь небесных тел

        public Galaxy() //Конструктор без параметорв
        {
            Name = Names[rand.Next(Names.Length)];
            ContentsGalaxy = new Dictionary<string, CelestialBody>();
        }
        public void Add(CelestialBody celbody) //Добавление в галактику уникальных обьектов
        {
            if (!ContentsGalaxy.ContainsKey(celbody.Name))
            {
                ContentsGalaxy[celbody.Name] = celbody;
            }
        }
        /// <summary>
        /// Заполнение галактики небесными телами
        /// </summary>
        public void MakeGalaxy() 
        {
            for (int i = 0; i < 5; i++) // Небесное тело
            {
                CelestialBody C = new CelestialBody();
                C.RandomInit();
                Add(C);
            }

            for (int i = 5; i < 10; i++) // Звезды
            {
                Star S = new Star();
                S.RandomInit();
                Add(S);
            }

            for (int i = 10; i < 15; i++) // Планеты
            {
                Planet P = new Planet();
                P.RandomInit();
                Add(P);
            }

            for (int i = 15; i < 20; i++) // Газовые гиганты
            {
                GasGigant G = new GasGigant();
                G.RandomInit();
                Add(G);
            }
        }
        public static IEnumerable<CelestialBody> StarsLinq(IEnumerable<Galaxy> galaxies)
        {
            return from galaxy in galaxies
                from body in galaxy.ContentsGalaxy.Values
                where body is Star
                select body;
        }
        public static IEnumerable<CelestialBody> Stars(IEnumerable<Galaxy> galaxies)
        {
            return galaxies
                .SelectMany(galaxy => galaxy.ContentsGalaxy.Values)
                .Where(celbody => celbody is Star)
                .Select(star => star);
        }
        /// <summary>
        /// Запрос - максимальная температура
        /// </summary>
        public static double MaxTemperature(IEnumerable<Galaxy> galaxies)  //LINQ-запрос, максимальная температура звезды
        {
            return (from galaxy in galaxies
                from body in galaxy.ContentsGalaxy.Values
                where body is Star
                select ((Star)body).Temperature).Max();
        }
        /// <summary>
        /// Запрос - минимальная температура
        /// </summary>
        public static double MinTemperature(IEnumerable<Galaxy> galaxies) //Методы расширения, минимальная температура звезды
        {
            return galaxies
                .SelectMany(galaxy => galaxy.ContentsGalaxy.Values)
                .Where(celbody => celbody is Star)
                .Select(star => ((Star)star).Temperature)
                .Min();
        }
        /// <summary>
        /// Пересечение двух галактик
        /// </summary>
        public static IEnumerable<CelestialBody> Intersect(Galaxy galaxy1, Galaxy galaxy2)  //Пересечение двух галактик, LINQ
        {
            return galaxy1.ContentsGalaxy.Values.Intersect(galaxy2.ContentsGalaxy.Values);
        }
        /// <summary>
        /// Группировка данных по типу
        /// </summary>
        public static IEnumerable<IGrouping<string, CelestialBody>> GroupByType(IEnumerable<Galaxy> galaxies)
        {
            return galaxies.SelectMany(galaxy => galaxy.ContentsGalaxy.Values)
                .GroupBy(celbody => celbody.GetType().Name);
        }
        /// <summary>
        /// Группировка по радиусу
        /// </summary>
        public static IEnumerable<IGrouping<string, CelestialBody>> GroupByRadius(IEnumerable<Galaxy> galaxies)  //Группировка по радиусу, LINQ
        {
            return from galaxy in galaxies
                from celbody in galaxy.ContentsGalaxy.Values
                orderby celbody.Radius                                             //Сортировка по радиусу
                group celbody by celbody.Radius < 1000 ? "Радиус меньше 1000" :
                    celbody.Radius >= 1000 && celbody.Radius < 3000 ? "Радиус от 1000 до 3000" :
                    celbody.Radius >= 3000 && celbody.Radius < 5000 ? "Радиус от 3000 до 5000" :
                    "Радиус больше 5000";
        }
        /// <summary>
        /// Вычисление обьема
        /// </summary>
        public static IEnumerable<dynamic> VolumeLinq(IEnumerable<Galaxy> galaxies)  //Вычисление обьема, LINQ
        {
            return from galaxy in galaxies
                from celbody in galaxy.ContentsGalaxy.Values
                let volume = (4 / 3.0) * Math.PI * Math.Pow(celbody.Radius, 3)  //Вычисление обьема
                select new { celbody.Name, volume };
        }
        public static IEnumerable<(string Name, double Volume)> Volume(IEnumerable<Galaxy> galaxies)
        {
            return galaxies
                .SelectMany(galaxy => galaxy.ContentsGalaxy.Values)
                .Select(celbody => (celbody.Name, Volume: (4 / 3.0) * Math.PI * Math.Pow(celbody.Radius, 3)));
        }
    }
}
