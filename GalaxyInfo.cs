using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab14__
{
    public class GalaxyInfo
    {
        private Random rand = new Random();
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
        protected string address;    //Адрес галактики
        public string Address        //Адрес галактики
        {
            get { return address; } //Свойство для чтения
            set                     //Свойство установки значения
            {
                Regex regex = new Regex("[А-Яа-яA-Za-z0-9]+");
                if (regex.IsMatch(value))
                    address = value;
                else
                    address = "No address";
            }
        }
        protected string type;    //Тип галактики
        public string Type        //Тип галактики
        {
            get { return type; } //Свойство для чтения
            set                  //Свойство установки значения
            {
                Regex regex = new Regex("[А-Яа-яA-Za-z0-9]+");
                if (regex.IsMatch(value))
                    type = value;
                else
                    type = "No type";
            }
        }
        public GalaxyInfo()     //Конструктор без параметров
        {
            Name = "No name";       //Имя галактики
            Address = "No address"; //Адрес галактики
            Type = "No type";       //Тип галактики
        }
        public GalaxyInfo(string name, string type, string address) //Конструктор с параметрами
        {
            Name = name;        //Имя галактики
            Address = address;  //Адрес галактики
            Type = type;        //Тип галактики
        }
        private static string[] names =                           //Названия галактик
        [ "Млечный путь", "Андромеда", "Треугольник", "Сомбреро",
            "Водоворот", "Антенна", "Центавра A" ];
        string[] types = new string[] { "Спиральная", "Эллиптическая", "Линзовидная", "Неправильная" };  //Типы галактик
        string[] addresses = new string[] { "Сектор 1", "Сектор 2", "Сектор 3", "Сектор 4" };            //Адреса галактик
        public void RandomInit() // Метод для формирования объектов класса с помощью ДСЧ
        {
            Name = names[rand.Next(names.Length)];
            Type = types[rand.Next(types.Length)];
            Address = addresses[rand.Next(addresses.Length)];
        }
    }
}
