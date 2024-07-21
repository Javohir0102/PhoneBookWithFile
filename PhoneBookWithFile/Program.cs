using PhoneBookWithFile.Services;
using System;

namespace PhoneBookWithFile
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuService menuService = new MenuService();
            menuService.ShowMenuService();
        }
    }
}
